using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;


namespace excels.Buffs.MeleeBuffs
{

	public class BerserkerRage : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Berserker's Rage"); // Buff display name
			Description.SetDefault("Improved melee abilities");
			Main.buffNoSave[Type] = true; // Causes this buff not to persist when exiting and rejoining the world
		}

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
        }
    }
}
