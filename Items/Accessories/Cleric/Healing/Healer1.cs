using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace excels.Items.Accessories.Cleric.Healing
{
    internal class Antitoxins : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Anti-Toxins");
            Tooltip.SetDefault("Gain immunity to poison \nHealing poisoned allies cures and heals them");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 20;
            Item.height = 28;
            Item.rare = 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.Poisoned] = true;
        //    player.GetModPlayer<excelPlayer>().healBonus += 1;
            player.GetModPlayer<excelPlayer>().antitoxinBottle = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BottledHoney)
                .AddIngredient(ItemID.JungleSpores, 5)
                .AddIngredient(ItemID.Stinger, 3)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }

    internal class NectarBottle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nectar Bottle");
            Tooltip.SetDefault("Buffing allies lasts an additional 3 seconds \nHealing allies additionally increases their life regeneration");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 20;
            Item.height = 28;
            Item.rare = 3;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<excelPlayer>().buffBonus += 3;
            player.GetModPlayer<excelPlayer>().antitoxinBottle = true;
        }
    }

    internal class ApothSatchel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Apothecary's Satchel");
            Tooltip.SetDefault("'Contains a plentitude of medcinial herbs' \nHealing gives an additional 1 health");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 30;
            Item.height = 34;
            Item.rare = 0;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<excelPlayer>().healBonus += 1;
        }
    }

    internal class MiniatureCross : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stained Glass Cross");
            Tooltip.SetDefault("While under a damaging effect, healing gives an additional 2 health \nIncreases max health by 20");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 30;
            Item.height = 34;
            Item.rare = 2;
        }

        public override void UpdateEquip(Player player)
        {
            // bonus healing is done after updatebadliferegen
            player.GetModPlayer<excelPlayer>().glassCross = true;
            player.statLifeMax2 += 20;
        }
    }
}
