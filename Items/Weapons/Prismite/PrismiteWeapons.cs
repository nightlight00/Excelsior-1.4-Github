using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
namespace excels.Items.Weapons.Prismite
{
    #region Bow
    internal class PrismiteBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires fragile crystal arrows");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 32;
            Item.knockBack = 2;
            Item.useTime = Item.useAnimation = 25;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAmmo = AmmoID.Arrow;
            Item.rare = 5;
            Item.shoot = 10;
            Item.shootSpeed = 9.75f;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;
            Item.scale = 1.15f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, 0f);
        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 crystal = velocity.RotatedByRandom(MathHelper.ToRadians(12));
            Projectile.NewProjectile(source, position, crystal, ModContent.ProjectileType<PrismiteArrow>(), damage, 5, player.whoAmI);
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PearlwoodBow)
                .AddIngredient(ItemID.CrystalShard, 8)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddIngredient(ItemID.UnicornHorn)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    
    public class PrismiteArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.alpha = 100;
        }

        public override void AI()
        {
            int dType = 70;
            if (Main.rand.NextBool(3)) dType = 68;
            Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dType);
            d.noGravity = true;
            d.noLight = true;
            d.scale = Main.rand.NextFloat(0.95f, 1.2f);
            d.velocity *= 0.7f;
            //Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
            for (var i = 0; i < 2; i++)
            {
                Vector2 crystalSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(30)) * Main.rand.NextFloat(0.6f, 1.6f);
                Projectile p = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, crystalSpeed, ProjectileID.CrystalShard, Projectile.damage / 2, 0, Main.player[Projectile.owner].whoAmI);
                p.penetrate = 3;
                p.usesLocalNPCImmunity = true;
                p.localNPCHitCooldown = 20; 
            }
            for (var i = 0; i < 12; i++)
            {
                int dType = 70;
                if (Main.rand.NextBool(3)) dType = 68;
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dType);
                d.noGravity = true;
                d.noLight = true;
                d.scale = Main.rand.NextFloat(1.1f, 1.5f);
                d.velocity *= 1.3f;
            }

        }
    }
    #endregion

    #region Sword
    public class PrismiteSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shatters on hit");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 42;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.height = Item.width = 40;
            Item.knockBack = 1.5f;
            Item.rare = 5;
            Item.value = 5000;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override bool? UseItem(Player player)
        {
            Item.noMelee = false;
            Item.noUseGraphic = false;
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PearlwoodSword)
                .AddIngredient(ItemID.CrystalShard, 10)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddIngredient(ItemID.UnicornHorn)
                .AddTile(TileID.Anvils)
                .Register();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int dstType = 68;
            if (Main.rand.NextBool()) dstType = 70;
            Dust d = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, dstType);
            d.noGravity = true;
            d.scale = Main.rand.NextFloat(0.9f, 1.2f);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            SoundEngine.PlaySound(SoundID.Item27, player.Center);
            for (var i = 0; i < 3; i++)
            {
                Vector2 shootVel = target.Center - player.Center;
                if (shootVel == Vector2.Zero)
                {
                    shootVel = new Vector2(0f, 1f);
                }
                shootVel.Normalize();
                shootVel *= Main.rand.NextFloat(14, 18);
                shootVel = shootVel.RotatedByRandom(MathHelper.ToRadians(25));
                Projectile.NewProjectile(player.GetSource_FromThis(),
                    player.Center, shootVel, ModContent.ProjectileType<PrismiteShard>(), damage / 2, knockBack / 2, player.whoAmI);
            }
            Item.noUseGraphic = true;
           // Item.noMelee = true;
        }
    }

    public class PrismiteShard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.CrystalShard);
            AIType = ProjectileID.CrystalShard;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 3;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }

        int dstType = 68; 

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                Projectile.frame = Main.rand.Next(4);
                Projectile.ai[0]++;
                if (Main.rand.NextBool()) dstType = 70;
            }
            Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, dstType);
            d.noGravity = true;
            d.scale = Main.rand.NextFloat(0.9f, 1.2f) * Projectile.scale;
            d.velocity *= 0;
        }
    }
    #endregion
}
