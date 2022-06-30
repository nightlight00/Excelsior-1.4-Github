using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace excels.Items.Materials
{
    #region Skyline
    public class SkylineBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("An incredibly light metal");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = 1;
            Item.maxStack = 999;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.createTile = ModContent.TileType<Tiles.OresBars.ExcelBarTiles>();
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.placeStyle = 0;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SkylineOre>(), 3)
                .AddTile(TileID.Furnaces)
                .Register();
        }
    }

    public class SkylineOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skyline Pebble");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = 1;
            Item.maxStack = 999; 
            
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.createTile = ModContent.TileType<Tiles.OresBars.SkylineOreTile>();
            Item.autoReuse = true;
            Item.useTurn = true;
        }
    }
    #endregion

    #region Glacial
    public class GlacialBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = 1;
            Item.maxStack = 999;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.createTile = ModContent.TileType<Tiles.OresBars.ExcelBarTiles>();
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.placeStyle = 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<GlacialOre>(), 3)
                .AddTile(TileID.Furnaces)
                .Register();
        }
    }

    public class GlacialOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = 1;
            Item.maxStack = 999;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.createTile = ModContent.TileType<Tiles.OresBars.GlacialOreTile>();
            Item.autoReuse = true;
            Item.useTurn = true;
            //Item.place
        }
    }
    #endregion

    #region Granite
    public class EnergizedGranite : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A chunk of granite that emits a strange energy");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = 1;
            Item.maxStack = 999;
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            Lighting.AddLight(Item.position, 1.14f * 0.3f, 2.36f * 0.3f, 2.55f * 0.3f);
        }
    }
    #endregion

    #region Fossil
    public class AncientFossil : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Prehistoric bones from underneath the grainy sands");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = 6;
            Item.maxStack = 999;
        }

    }
    #endregion

    #region Stellar Plating
    public class StellarPlating : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Taken from an interstellar machine");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 24;
            Item.rare = ModContent.RarityType<StellarRarity>();
            Item.maxStack = 999;
        }
    }
    #endregion

    #region Wyvern Scale
    public class WyvernScale : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The scales of a mystical flying creature");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = 5;
            Item.maxStack = 999;
        }

    }
    #endregion

    #region Mystic Crystal
    public class MysticCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A purified evil condensed into a glittery gemstone");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = 1;
            Item.maxStack = 999;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DemoniteOre, 3)
                .AddIngredient(ItemID.PurificationPowder, 5)
                .AddIngredient(ItemID.FallenStar)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                    .AddIngredient(ItemID.CrimtaneOre, 3)
                    .AddIngredient(ItemID.PurificationPowder, 5)
                    .AddIngredient(ItemID.FallenStar)
                    .AddTile(TileID.Anvils)
                    .Register();
        }
    }
    #endregion

    #region Shattered Heartbeat
    public class ShatteredHeartbeat : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("It's still beating");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = 2;
            Item.maxStack = 999;
        }
        public override void AddRecipes()
        {
            CreateRecipe(10)
                .AddIngredient(ItemID.LifeCrystal)
                .AddIngredient(ItemID.Bone, 25)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    #endregion
}
