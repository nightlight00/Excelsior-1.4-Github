using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace excels.Items.Accessories.Random
{
    public class MimicToothNecklace : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Taking damage causes the necklace to bite back \nSeen as a good luck charm in some parts");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = 3;
            Item.width = Item.height = 28;
            Item.accessory = true;
            Item.value = 500;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<excelPlayer>().MimicNecklace = true;
            player.luck += 0.2f;
        }
    }
}
