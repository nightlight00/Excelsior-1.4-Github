using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace excels.Tiles.Misc
{
    #region Skyline Brick
    internal class SkylineBrickTile : ModTile
    {
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;

			AddMapEntry(new Color(197, 246, 245));

			DustType = ModContent.DustType<Dusts.SkylineDust>();
			ItemDrop = ModContent.ItemType<Items.TileItems.SkylineBrick>();
			HitSound = SoundID.Tink;
			//sound = 1;
		}
	}

	internal class SkylineBrickWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			Main.wallHouse[Type] = true;

			DustType = DustID.Marble;
			ItemDrop = ModContent.ItemType<Items.TileItems.SkylineBrickWallItem>();

			AddMapEntry(new Color(197, 246, 245));
		}
	}
	#endregion

	#region Glacial Brick
	internal class GlacialBrickTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;

			AddMapEntry(new Color(48, 184, 246));

			DustType = ModContent.DustType<Dusts.GlacialDust>();
			ItemDrop = ModContent.ItemType<Items.TileItems.GlacialBrick>();
			HitSound = SoundID.Tink;
			//SoundStyle = 1;
		}

		public override bool HasWalkDust() => true;

        public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color)
        {
			dustType = DustID.Snow;
        }
    }
    #endregion

    #region Checker
    internal class CheckerTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;

			AddMapEntry(new Color(223, 241, 252));

			DustType = DustID.Marble;
			ItemDrop = ModContent.ItemType<Items.TileItems.CheckerItem>();
			HitSound = SoundID.Tink;
			//SoundStyle = 1;
		}
	}

	internal class CheckerWall : ModWall
    {
		public override void SetStaticDefaults()
		{
			Main.wallHouse[Type] = true;

			DustType = DustID.Marble;
			ItemDrop = ModContent.ItemType<Items.TileItems.CheckerWallItem>();

			AddMapEntry(new Color(143, 158, 168));
		}
	}
	#endregion
	
	public class StarlightAnvilTile : ModTile
    {
		public override void SetStaticDefaults()
		{
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			Main.tileFrameImportant[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileID.Sets.IgnoredByNpcStepUp[Type] = true;

			AdjTiles = new int[] { TileID.Anvils };

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
			TileObjectData.addTile(Type);

			//ItemDrop = ModContent.ItemType<Items.Furniture.Anvils.StarlightAnvil>();
			HitSound = SoundID.Tink;
			//SoundStyle = 1;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Starlight Anvil");
			AddMapEntry(new Color(85, 53, 163), name);
			
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 32, ModContent.ItemType<Items.Furniture.Anvils.StarlightAnvil>(), 1);
		}
	}
}
