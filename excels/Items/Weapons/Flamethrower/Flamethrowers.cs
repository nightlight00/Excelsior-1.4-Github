using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

namespace excels.Items.Weapons.Flamethrower
{
    internal class Hellslinger : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Uses gel for ammo");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 16;
            Item.knockBack = 0.1f;
            Item.noMelee = true;
            Item.useTime = 7;
            Item.useAnimation = 21;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item34;
            Item.rare = ItemRarityID.LightRed;

            Item.height = 16;
            Item.width = 46;
            Item.shootSpeed = 5;
            Item.useAmmo = ItemID.Gel;
            Item.shoot = ModContent.ProjectileType<HellFlames>();

            Item.useTurn = false;
            Item.autoReuse = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(4, 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 16)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (player.itemAnimation < 18)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class HellFlames : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Flames}";

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Flames);
            AIType = ProjectileID.Flames;

            Projectile.timeLeft = (int)(Projectile.timeLeft * 0.8f);
            Projectile.penetrate = 2;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 900);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 900);
        }
    }
}
