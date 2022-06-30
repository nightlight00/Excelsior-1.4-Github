using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;
using Terraria.IO;
using Terraria.Initializers;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace excels
{
	public class excelWorld : ModSystem
	{
		public static bool downedNiflheim = false;
		public static bool downedChasm = false;
		public static bool downedStarship = false;

		public override void OnWorldLoad()
		{
			downedNiflheim = false;
			downedChasm = false;
			downedStarship = false;
		}

		public override void OnWorldUnload()
		{
			downedNiflheim = false;
			downedChasm = false;
			downedStarship = false;
		}


		public override void SaveWorldData(TagCompound tag)
		{
			if (downedNiflheim)
			{
				tag["downedNiflheim"] = true;
			}
			if (downedChasm)
			{
				tag["downedChasm"] = true;
			}
			if (downedStarship)
			{
				tag["downedStarship"] = true;
			}
		}

		public override void LoadWorldData(TagCompound tag)
		{
			downedNiflheim = tag.ContainsKey("downedNiflheim");
			downedChasm = tag.ContainsKey("downedChasm");
			downedStarship = tag.ContainsKey("downedStarship");
		}

		public override void NetSend(BinaryWriter writer)
		{
			// Order of operations is important and has to match that of NetReceive
			var flags = new BitsByte();
			flags[0] = downedNiflheim;
			flags[1] = downedChasm;
			flags[2] = downedStarship;
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			// Order of operations is important and has to match that of NetSend
			BitsByte flags = reader.ReadByte();
			downedNiflheim = flags[0];
			downedChasm = flags[1];
			downedStarship = flags[2];
		}

		public override void PostWorldGen()
		{
			for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
			{
				Chest chest = Main.chest[chestIndex];
				// If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. 
				// this looks for the 0 frame, which would be a normal wooden chest
				if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers && Main.tile[chest.x, chest.y].TileFrameX == 0 * 36)
				{
					for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
					{
						if (chest.item[inventoryIndex].type == ItemID.None)
						{
							if (Main.rand.NextBool(3))
							{
								switch (Main.rand.Next(2))
								{
									default: // 0
										chest.item[inventoryIndex].SetDefaults(ModContent.ItemType<Items.Accessories.Banner.RegenBanner>());
										break;

									case 1:
										chest.item[inventoryIndex].SetDefaults(ModContent.ItemType<Items.Accessories.Cleric.Healing.ApothSatchel>());
										break;
								}
							}
							break;
						}
					}
				}

				// locked gold chest
				if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers && Main.tile[chest.x, chest.y].TileFrameX == 2 * 36)
				{
					for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
					{
						if (chest.item[inventoryIndex].type == ItemID.None)
						{ 
							if (Main.rand.NextBool(4))
                            {
								chest.item[inventoryIndex].SetDefaults(ModContent.ItemType<Items.Weapons.Necrotic1.Dungeon.TideClamp>());
							}
							break;
						}
					}
				}
			}
		}

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Random Gems"));

			if (ShiniesIndex != -1)
			{
				tasks.Insert(ShiniesIndex + 1, new SkylineOrePass("Skyline Ores", 237.4298f));
				tasks.Insert(ShiniesIndex + 1, new GladiolusPass("Gladiolus", 237.4298f));
			}

		}

		public override void PostAddRecipes()
		{
			for (var i = 0; i < Recipe.maxRecipes; i++)
			{
				Recipe recipe = Main.recipe[i];
				if (recipe.TryGetResult(ItemID.NightsEdge, out Item _))
				{
					recipe.RemoveTile(TileID.DemonAltar);
					recipe.AddTile(ModContent.TileType<Tiles.Misc.StarlightAnvilTile>());
				}
			}
		}

		int GladiolusTimer = 900; // 15 seconds
		public override void PostUpdateEverything()
		{
			GladiolusTimer--;

			// random herb time
			if (GladiolusTimer <= 0)
			{
				bool GladiolusPlaced = false;

				// attempts to spawn gladiolus 50 times before giving up
				for (var i = 0; i < 300; i++)
				{
					if (!GladiolusPlaced)
					{
						int x = WorldGen.genRand.Next(0, Main.maxTilesX);
						int y = WorldGen.genRand.Next(0, (int)(Main.maxTilesY * 0.25f));
						if (Main.tile[x, y].TileType == TileID.Grass || Main.tile[x, y].TileType == TileID.GolfGrass)
						{
							if (!Main.tile[x, y - 1].HasTile)
							{
								WorldGen.PlaceTile(x, y - 1, ModContent.TileType<Tiles.Herbs.GladiolusTile>());
								GladiolusPlaced = true;
							}
						}
					}
				}

				GladiolusTimer = Main.rand.Next(900, 1500);
			}
		}
	}

	public class GladiolusPass : GenPass
    {
		public GladiolusPass(string name, float loadWeight) : base(name, loadWeight)
		{
		}

		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
			// has 400 tries to create 50 herbs, growth stage is randomized
			int GladCount = 0;
			for (var i = 0; i < 400; i++)
			{
				if (GladCount < 50)
				{
					int x = WorldGen.genRand.Next(0, Main.maxTilesX);
					int y = WorldGen.genRand.Next(0, (int)(Main.maxTilesY * 0.25f));
					if (Main.tile[x, y].TileType == TileID.Grass || Main.tile[x, y].TileType == TileID.GolfGrass)
					{
						if (!Main.tile[x, y - 1].HasTile)
						{
							WorldGen.PlaceTile(x, y - 1, ModContent.TileType<Tiles.Herbs.GladiolusTile>(), false, false, -1, Main.rand.Next(3));
							GladCount++;
						}
					}
				}
			}
		}
    }

	public class SkylineOrePass : GenPass
    {
		public SkylineOrePass(string name, float loadWeight) : base(name, loadWeight)
		{
		}

		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
		{
			progress.Message = "Skyline Stars are filling the outer skies";

			// "6E-05" is "scientific notation". It simply means 0.00006 but in some ways is easier to read.
			for (int t = 0; t < (int)((Main.maxTilesX * Main.maxTilesY) * 3E-05); t++)
			{

				int x = WorldGen.genRand.Next((int)(Main.maxTilesX * 0.015f), (int)(Main.maxTilesX * 0.985f));
				if (x < Main.maxTilesX * 0.4f || x > Main.maxTilesX * 0.6f)
				{
					int y = WorldGen.genRand.Next(20, (int)(Main.maxTilesY * 0.1f));
					PlaceSkyline(x, y);
					//WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<Tiles.OresBars.GlacialOreTile>()w);
				}
			}
		}

		private readonly int[,] _skylineoreshape = {
			{0,0,0,0,1,0,0,0,0},
			{0,1,2,2,1,2,2,1,0},
			{0,2,1,2,1,2,1,2,0},
			{0,2,2,1,1,1,2,2,0},
			{1,1,1,1,2,1,1,1,1},
			{0,2,2,1,1,1,2,2,0},
			{0,2,1,2,1,2,1,2,0},
			{0,1,2,2,1,2,2,1,0},
			{0,0,0,0,1,0,0,0,0},
		};
		private readonly int[,] _skylineoreslopeshape = {
			{0,0,0,0,0,0,0,0,0},
			{0,0,1,2,0,1,2,0,0},
			{0,4,0,0,0,0,0,3,0},
			{0,2,0,0,0,0,0,1,0},
			{0,0,0,0,0,0,0,0,0},
			{0,4,0,0,0,0,0,3,0},
			{0,2,0,0,0,0,0,1,0},
			{0,0,3,4,0,3,4,0,0},
			{0,0,0,0,0,0,0,0,0},
		};
		private readonly int[,] _skylineorewallshape = {
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,1,1,1,1,1,0,0},
			{0,0,1,1,1,1,1,0,0},
			{0,0,1,1,1,1,1,0,0},
			{0,0,1,1,1,1,1,0,0},
			{0,0,1,1,1,1,1,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
		};

		public bool PlaceSkyline(int i, int j)
		{
			for (int y = 0; y < _skylineoreshape.GetLength(0); y++)
			{
				for (int x = 0; x < _skylineoreshape.GetLength(1); x++)
				{
					int k = i - 4 + x;
					int l = j - 4 + y;

					int tileType = 0;
					SlopeType slopeType = 0;
					switch (_skylineoreshape[y, x])
					{
						case 1:
							tileType = ModContent.TileType<Tiles.OresBars.SkylineOreTile>();
							break;
						case 3:
							tileType = TileID.Diamond;
							break;
						case 2:
							tileType = TileID.Cloud;
							break;
					}
					switch (_skylineoreslopeshape[y, x])
					{
						case 1:
							slopeType = SlopeType.SlopeDownLeft; 
							break;
						case 2:
							slopeType = SlopeType.SlopeDownRight;
							break;
						case 3:
							slopeType = SlopeType.SlopeUpLeft;
							break;
						case 4:
							slopeType = SlopeType.SlopeUpRight;
							break;
					}
					switch (_skylineorewallshape[y, x])
					{
						case 1:
							WorldGen.PlaceWall(k, l, WallID.Cloud);
							break;
					}
					if (tileType != 0)
					{
						WorldGen.PlaceTile(k, l, tileType);
						Tile tile = Framing.GetTileSafely(k, l);
						tile.Slope = slopeType;
					}
					//Main.tile[k, l].TileType = tileType;
					//Main.tile[k, l].ResetToType(TileID.PlantDetritus); 
					//WorldGen.PlaceTile(k * 16, l * 16, tileType);
					//WorldGen.Place1x1(k, l, tileType);
				}
			}
			return true;
		}
	}
}
