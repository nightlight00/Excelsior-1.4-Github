using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace excels.Buffs.ClericCld
{
    internal class BlessingCooldown : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Prayer Exhaut");
			Description.SetDefault("Holy weapon prayers need to replenish their energy");
			Main.buffNoTimeDisplay[Type] = false;
			Main.buffNoSave[Type] = false;
			Main.debuff[Type] = true; // Add this so the nurse doesn't remove the buff when healing
		}
	}

	internal class AnguishedSoul : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anguished Soul");
			Description.SetDefault("Cost of performing the dark arts \nAll positive life regeneration halved");
			Main.buffNoTimeDisplay[Type] = false;
			Main.buffNoSave[Type] = false;
			Main.debuff[Type] = true; // Add this so the nurse doesn't remove the buff when healing
		}

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
			rare = ItemRarityID.Red;
        }

        public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<excelPlayer>().AnguishSoul = true;
		}
	}
}
