using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Creative;

namespace excels.Items.Accessories.Shield
{
    [AutoloadEquip(EquipType.Shield)]
    internal class BeetleShield : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beetle Husk Shield");
            Tooltip.SetDefault("Grants immunity to knockback \nGrants immunity to fire blocks " +
                "\nReflects some damage back onto enemies \nDecreases damage taken by 10%");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.defense = 6;
            Item.rare = 8;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ObsidianShield)
                .AddIngredient(ItemID.SoulofMight, 4)
                .AddIngredient(ItemID.BeetleHusk, 6)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void UpdateEquip(Player player)
        {
            player.noKnockback = true;
            player.fireWalk = true;
            player.endurance += 0.1f;
            player.GetModPlayer<excelPlayer>().ShieldReflect = true;

            player.hasRaisableShield = true;

            player.GetModPlayer<excelPlayer>().BeetleShield = true;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<AccessoryDrawHelper>()] == 0)
            {
                Projectile p = Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, new Vector2(0, -3),
                    ModContent.ProjectileType<AccessoryDrawHelper>(), 0, 0, player.whoAmI);
                p.ai[0] = 1;
            }
        }
    }
}
