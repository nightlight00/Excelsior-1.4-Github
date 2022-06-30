using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Enums;


namespace excels.Items.Weapons.Granite
{
    #region Energy Sword
    internal class EnergySword : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;

        public override void SetDefaults()
        {
            Item.width = Item.height = 32;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 18;
            Item.useTime = Item.useAnimation = 13;
            Item.UseSound = SoundID.Item15;
            Item.knockBack = 1.3f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = 1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.scale = 1.1f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust d = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.EnergyDust>());
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Materials.EnergizedGranite>(), 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    #endregion

    #region Energy Pickaxe
    internal class EnergyPickaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Can mine meteorite");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override Color? GetAlpha(Color lightColor) => Color.White;

        public override void SetDefaults()
        {
            Item.height = Item.width = 40;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 7;
            Item.useTime = Item.useAnimation = 8;
            Item.UseSound = SoundID.Item15;
            Item.knockBack = 0.9f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = 1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.pick = 50;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust d = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.EnergyDust>());
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Materials.EnergizedGranite>(), 12)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    #endregion

    #region Seeking Discharge
    internal class EnergyStorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seeking Discharge");
            Tooltip.SetDefault("Conjures unstable charges of energy");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Magic;
            Item.damage = 15;
            Item.useTime = Item.useAnimation = 17;
            Item.UseSound = SoundID.Item21;
            Item.knockBack = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = 1;
            Item.mana = 7;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<EnergyDischarge>();
            Item.shootSpeed = 8.3f;
            Item.noMelee = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Materials.EnergizedGranite>(), 8)
                .AddIngredient(ItemID.Book)
                .AddTile(TileID.Bookcases)
                .Register();
        }
    }

    internal class EnergyDischarge : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 20;
            Projectile.alpha = 100;
            Projectile.timeLeft = 200;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.ai[0] < 2)
            {
                Explosion();
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.ai[0] < 2)
            {
                Explosion();
            }
        }

        private void Explosion()
        { 
            Projectile.alpha = 255;
            Projectile.velocity *= 0;
            Projectile.timeLeft = 2;
            Projectile.knockBack *= 1.45f;
            Projectile.tileCollide = false;
            Projectile.ai[0] = 2;
            Projectile.position = Projectile.Center;
            Projectile.width = Projectile.height = 60;
            Projectile.Center = Projectile.position;
            for (var i = 0; i < 32; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.EnergyDust>(), Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-4, 4));
                d.scale = Main.rand.NextFloat(1) / 4 + 0.9f;
                d.velocity = new Vector2(Main.rand.NextFloat(-2.4f, 2.4f), Main.rand.NextFloat(-0.25f, -3));
                d.noGravity = true;
            }
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.EnergyDust>());
            d.velocity = -Projectile.velocity / 3;
            d.scale = 1.4f;

            if (Projectile.ai[0] == 0)
            {
                Vector2 targetPos = Vector2.Zero;
                float targetDist = 300;
                bool target = false;
                for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.CanBeChasedBy(this, false))
                    {
                        float distance = Vector2.Distance(npc.Center, Projectile.Center);
                        if ((distance < targetDist) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                        {
                            targetDist = distance;
                            targetPos = npc.Center;
                            target = true;
                        }
                    }
                }
                if (target)
                {
                    Vector2 move = targetPos - Projectile.Center;
                    AdjustMagnitude(ref move);
                    Projectile.velocity = (20 * Projectile.velocity + move); // / 5f;
                    AdjustMagnitude(ref Projectile.velocity);
                    Projectile.rotation = Projectile.velocity.ToRotation();
                }
            }
        }

        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 6f)
            {
                vector *= 8.3f / magnitude;
            }
        }
    }
    #endregion

    #region Granite Surge
    
    internal class GraniteSurge : ClericDamageItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Explodes into restorative granite energy on enemy impact");
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SafeSetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = ModContent.GetInstance<ClericClass>();
            Item.width = Item.height = 70;
            Item.useTime = Item.useAnimation = 27;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3;
            Item.value = 10000;
            Item.rare = 1;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<EnergySurge>();
            Item.shootSpeed = 8;
            Item.noMelee = true;

            clericEvil = true;
            clericBloodCost = 4;
            Item.mana = 3;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Materials.EnergizedGranite>(), 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }

    public class EnergySurge : clericProj
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Fireball}";

        public override void SafeSetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 200;
            Projectile.friendly = true;
            Projectile.alpha = 255;

            clericEvil = true;

        }

        public override void AI()
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<Dusts.EnergyDust>());
            d.noGravity = true;
            d.scale *= Main.rand.NextFloat(1.3f, 1.45f);
            d.velocity *= 0;

            if (Main.rand.NextBool(3))
            {
                Dust d2 = Dust.NewDustDirect(Projectile.Center, 0, 0, ModContent.DustType<Dusts.EnergyDust>());
                d.scale *= 0.87f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (var i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_None(), Projectile.Center, new Vector2(Main.rand.NextFloat(-3, 3), -5 - Main.rand.NextFloat(2)), ModContent.ProjectileType<EnergySpark>(), 1, 1, Main.player[Projectile.owner].whoAmI);
            }
        }
    }

    public class EnergySpark : clericHealProj
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Fireball}";

        public override void SafeSetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 200;
            Projectile.friendly = true;
            Projectile.alpha = 255;

            clericEvil = true;
            canHealOwner = true;
            healPenetrate = 1;
        }

        public override void AI()
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<Dusts.EnergyDust>());
            d.noGravity = true;
            d.scale *= 1.12f;
            d.velocity *= 0;

            Projectile.velocity.Y += 0.2f;
            HealDistance(Main.LocalPlayer, Main.player[Projectile.owner], 28, 2);
        }
    }
    
    #endregion
    
}
