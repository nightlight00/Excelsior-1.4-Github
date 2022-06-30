using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace excels.Buffs.ClericBonus
{
    public abstract class ClericBonusBuff : ModBuff
    {
        public string BuffName;
        public string BuffDesc;

        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Names();
            DisplayName.SetDefault(BuffName);
            Description.SetDefault(BuffDesc);
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = true; // Add this so the nurse doesn't remove the buff when healing
        }

        public virtual void Names()
        {

        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            rare = ModContent.RarityType<Items.ClericBuffRarity>();
        }
    }

    internal class GlacialGuardBuff : ClericBonusBuff
    {
        public override void Names()
        {
            BuffName = "Glacial Guard";
            BuffDesc = "Increases defense by 15 but decreases movement abilities";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 15;

            player.GetModPlayer<excelPlayer>().GlacialGuard = true;
        }
    }

    internal class HolyGuardBuff : ClericBonusBuff
    {
        public override void Names()
        {
            BuffName = "Holy Guard";
            BuffDesc = "You're being protected by the holy light";
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 10;
        }
    }

    internal class EnergyUnleash : ClericBonusBuff
    {
        public override void Names()
        {
            BuffName = "Energy Unleash";
            BuffDesc = "Natural energy is flowing through at intense rates";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 3;
            Dust d = Dust.NewDustDirect(player.position, player.width, player.height, 220);
            d.velocity = new Vector2(player.velocity.X * -0.65f, -Main.rand.NextFloat(3, 5));
            d.noGravity = true;
        }
    }

    internal class FloralBeauty : ClericBonusBuff
    {
        public override void Names()
        {
            BuffName = "Floral Beauty";
            BuffDesc = "Increased life regeneration";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 5;
        }
    }

    internal class PriestessBlessingRadiance : ClericBonusBuff
    {
        public override void Names()
        {
            BuffName = "Priestess' Blessing : Radiance";
            BuffDesc = "Increases healing potency by 1";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<excelPlayer>().healBonus += 1;
        }
    }

    internal class PriestessBlessingNecrotic : ClericBonusBuff
    {
        public override void Names()
        {
            BuffName = "Priestess' Blessing : Necrotic";
            BuffDesc = "Necrotic blood cost reduced by 15%";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<excelPlayer>().bloodCostMult -= 0.15f;
        }
    }
}
