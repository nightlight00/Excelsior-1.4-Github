using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace excels.Items.Weapons.Guns1
{
    #region Purple Haze
    class PurpleHaze : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Uses gel for ammo");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Flamethrower);

            Item.damage = 20;
            Item.rare = ItemRarityID.Pink;
            Item.shootSpeed = 15f;
            Item.shoot = ModContent.ProjectileType<Inferno>();
            Item.height = 16;
            Item.width = 68;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (player.itemAnimation < player.itemAnimationMax - 4)
            {
                return false;
            }
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
        
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Musket)
                .AddIngredient(ItemID.Ichor, 8)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    class Inferno : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Flames}";

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 30;
            Projectile.width = Projectile.height = 16;
            Projectile.alpha = 255;
            Projectile.penetrate = 4;
            Projectile.velocity *= 2f;
            Projectile.ignoreWater = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1; // 1 hit per npc max
            Projectile.localNPCHitCooldown = 15;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 1200);
        }
        public override void AI()
        {
            if (Projectile.timeLeft < 27)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust dst = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 75, Projectile.velocity.X * 0.6f, Projectile.velocity.Y * 0.6f, 0, default(Color), 2.5f);
                    dst.noGravity = true;
                    dst.rotation += 0.2f;
                    dst.fadeIn = Main.rand.NextFloat(2.9f, 3.5f);
                    dst.scale = dst.fadeIn * 0.7f;
                }
                if (Main.rand.NextBool())
                {
                    Dust dst2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 75, 0, 0, 0, default(Color), 1.25f);
                    dst2.rotation += 0.2f;
                  //  dst2.velocity.X += Projectile.velocity.X / 5;
                }

            }
        }
    }
    #endregion
    public class VelvetMaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Mean Greens");
            Tooltip.SetDefault("33% chance to not consume ammo");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 26;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 22;
            Item.useTime = Item.useAnimation = 19;
            Item.reuseDelay = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3.4f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 11f;
            Item.useAmmo = AmmoID.Bullet;

            Item.UseSound = SoundID.Item36;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //SoundEngine.PlaySound(SoundID.Item36);
            for (int i = 1; i < 3; i++)
            {
                Projectile.NewProjectile(source, position, velocity.RotatedBy(MathHelper.ToRadians(4*i)) * (1 - (0.18f * i)), type, damage, knockback, player.whoAmI);
                Projectile.NewProjectile(source, position, velocity.RotatedBy(MathHelper.ToRadians(4*-i))*(1-(0.18f*i)), type, damage, knockback, player.whoAmI);
            }
            Projectile.NewProjectile(source, position, velocity*0.95f, ProjectileID.IchorBullet, damage, knockback, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TheUndertaker)
                .AddIngredient(ItemID.Ichor, 8)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }

    }

    public class MeanGreens : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mean Greens");
            Tooltip.SetDefault("33% chance to not consume ammo" +
                             "\nConverts ammunition into chlorophyte");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 21;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 22;
            Item.useTime = Item.useAnimation = 34;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = 10000;
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 15f;
            Item.useAmmo = AmmoID.Bullet;

            Item.UseSound = SoundID.Item36;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= .33f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, -2);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ProjectileID.ChlorophyteBullet;
            for (int i = 0; i < 4; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(24));
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ChlorophyteBar, 14)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    public class TommyGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tommy Gun");
            Tooltip.SetDefault("33% chance to not consume ammo" +
                             "\n'My boy got a tommy gun!'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 21;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 22;
            Item.useTime = Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2.5f;
            Item.value = 10000;
            Item.rare = 5;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 18f;
            Item.useAmmo = AmmoID.Bullet;

            Item.UseSound = SoundID.Item41;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= .33f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 newSpeed = velocity * Main.rand.NextFloat(0.85f, 1.15f);
            newSpeed = newSpeed.RotatedByRandom(MathHelper.ToRadians(Main.rand.Next(11)));
            Projectile.NewProjectile(source, position, newSpeed, type, damage, knockback, player.whoAmI);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-16, 0);
        }
    }

    public class SnowballCannonEX : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flurry Hand-Cannon");
            Tooltip.SetDefault("50% chance to not consume ammo" +
                             "\n'Now you too can control a snowstorm from the comforts of your home!'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 23;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 22;
            Item.useTime = Item.useAnimation = 7;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3f;
            Item.value = 10000;
            Item.rare = 6;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.SnowBallFriendly;
            Item.shootSpeed = 13f;
            Item.useAmmo = AmmoID.Snowball;

            Item.UseSound = SoundID.Item11;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= .50f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (var i = 0; i < 1 + Main.rand.Next(3); i++)
            {
                Vector2 newSpeed = velocity * Main.rand.NextFloat(0.85f - (0.05f * i), 1.15f + (0.025f * i));
                newSpeed = newSpeed.RotatedByRandom(MathHelper.ToRadians(Main.rand.Next(19 + (i * 3))));
                float scale = Main.rand.NextFloat(0.7f - (0.05f * i), 1.2f + (0.05f * i));
                Projectile p = Projectile.NewProjectileDirect(source, position, newSpeed, type, (int)(damage * scale), knockback * scale, player.whoAmI);
                p.scale = scale;
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, -4);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SnowBlock, 40);
            recipe.AddIngredient(ItemID.SoulofNight, 6);
            recipe.AddIngredient(ItemID.SoulofLight, 6);
            recipe.AddTile(TileID.CrystalBall);
            recipe.ReplaceResult(ItemID.SnowGlobe);
            recipe.Register();
        }
    }
    /*
    public class GrenadePistol : ModItem
    {
        public override string Texture => $"Terraria/Images/Item_{ItemID.Revolver}";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Gives all ammunition explosive properties \n'The demolitionist's backup when bombs are not enough'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 17;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = Item.useAnimation = 37;
            Item.useAmmo = AmmoID.Bullet;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.height = 22;
            Item.width = 46;
            Item.knockBack = 1f;
            Item.rare = 1;
            Item.value = 100000;
            Item.shoot = 10;
            Item.shootSpeed = 6.4f;
            Item.scale = 0.9f;
            Item.UseSound = SoundID.Item41;
            Item.noMelee = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4f, 0f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ProjectileID.ExplosiveBullet, damage, knockback, player.whoAmI, 0);
            return false;
        }
    }
    */
}
