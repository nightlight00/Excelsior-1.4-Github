using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace excels
{
    internal class ClericClass : DamageClass
    {
		public override void SetStaticDefaults()
		{
			// Make weapons with this damage type have a tooltip of 'X example damage'.
			ClassName.SetDefault("clerical damage");
		}

		public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
		{
			if (damageClass == DamageClass.Generic)
				return StatInheritanceData.Full;


			return new StatInheritanceData(
				damageInheritance: 0f,
				critChanceInheritance: 0f,
				attackSpeedInheritance: 0f,
				armorPenInheritance: 0f,
				knockbackInheritance: 0f
			);
		}

		public override bool UseStandardCritCalcs => true;

	}

	public class ClericClassPlayer : ModPlayer
    {
		public static ClericClassPlayer ModPlayer(Player player)
		{
			return player.GetModPlayer<ClericClassPlayer>();
		}

		// radiant is mispelled lol
		public int clericRadiantAdd;
		public float clericRadiantMult = 1f;
		public int clericNecroticAdd;
		public float clericNecroticMult = 1f;

		public float clericKnockback;
		public int clericCrit;

		public override void ResetEffects()
		{
			ResetVariables();
		}

		public override void UpdateDead()
		{
			ResetVariables();
		}

		private void ResetVariables()
		{
			clericRadiantAdd = 0;
			clericRadiantMult = 1f;
			clericNecroticAdd = 0;
			clericNecroticMult = 1f;

			clericKnockback = 0f;
			clericCrit = 0;
		}
	}

	public abstract class ClericDamageItem : ModItem
	{
		//public override bool i => true;
		public int clericBloodCost = 0;
		public bool clericEvil = false;
		public int healAmount = 0;
		public float clericManaReduce = 0;

		int clericBloodCostTrue = 0;

		// Custom items should override this to set their defaults
		public virtual void SafeSetDefaults()
		{
		}

		// By making the override sealed, we prevent derived classes from further overriding the method and enforcing the use of SafeSetDefaults()
		// We do this to ensure that the vanilla damage types are always set to false, which makes the custom damage type work
		public sealed override void SetDefaults()
		{
			Item.DamageType = ModContent.GetInstance<ClericClass>();
			SafeSetDefaults();
			//Item.DamageType = ModContent.GetInstance<ClericClass>();
		}
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
			// already gains these bonuses
		//	damage *= player.GetDamage(DamageClass.Generic).Multiplicative;
			if (!clericEvil)
			{
				// radient damage, plus 25% necrotic damage
				damage += ClericClassPlayer.ModPlayer(player).clericRadiantAdd;
				damage *= ClericClassPlayer.ModPlayer(player).clericRadiantMult + ((ClericClassPlayer.ModPlayer(player).clericNecroticMult - 1) * 0.25f);
			}
			else
			{
				// same as above but in reverse
				damage += ClericClassPlayer.ModPlayer(player).clericNecroticAdd;
				damage *= ClericClassPlayer.ModPlayer(player).clericNecroticMult + ((ClericClassPlayer.ModPlayer(player).clericRadiantMult - 1) * 0.25f);

				clericBloodCostTrue = (int)((clericBloodCost - player.GetModPlayer<excelPlayer>().bloodCostMinus) * player.GetModPlayer<excelPlayer>().bloodCostMult);
				if (clericBloodCostTrue <= 0)
                {
					clericBloodCostTrue = 1;
                }
			}
		}


        public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
        {
			// Adds knockback bonuses
			knockback += ClericClassPlayer.ModPlayer(player).clericKnockback;
		}

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
		   crit += ClericClassPlayer.ModPlayer(player).clericCrit;
		}

		// Because we want the damage tooltip to show our custom damage, we need to modify it
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var modPlayer = ClericClassPlayer.ModPlayer(Main.player[Item.whoAmI]);
			ExtraTooltipModifications(tooltips);

			// cool colour switching

		//	string s = $"{MathHelper.Lerp(Max.R, Min.R, timer).ToString("X2") + MathHelper.Lerp(Max.G, Min.G, timer).ToString("X2") + MathHelper.Lerp(Max.B, Min.B, timer).ToString("X2")}";
			string ClassText = $"[c/9b5ed4:~Cleric Class~]\n";
			// 9b5ed4

			//tooltips.Add(new TooltipLine(Mod, "Damage", "This weapon benefits from necrotic bonuses"));
			if (clericEvil)
            {
				float dmg = (float)Math.Round((modPlayer.clericNecroticMult - 1), 2) * 100;
				float dmgOp = (float)Math.Round((modPlayer.clericRadiantMult - 1) * 0.25f, 2) * 100;

				foreach (TooltipLine line2 in tooltips)
				{
					if (line2.Mod == "Terraria") { if (line2.Name == "Damage")
						{
							// switches damage tooltip
							int wDmg = (int)Main.player[Item.whoAmI].GetWeaponDamage(Item, true);
							wDmg += ClericClassPlayer.ModPlayer(Main.player[Item.whoAmI]).clericNecroticAdd;
							wDmg = (int)(wDmg * (ClericClassPlayer.ModPlayer(Main.player[Item.whoAmI]).clericNecroticMult + ((ClericClassPlayer.ModPlayer(Main.player[Item.whoAmI]).clericRadiantMult - 1) * 0.25f)));
							line2.Text = $"{ClassText}{wDmg} necrotic damage ({dmg}% necrotic + {dmgOp}% radiant)";
						} 
						else if (line2.Name == "UseMana" && (clericBloodCost > 0 || Item.mana > 0))
                        {
							string cost = "drops";
							if (clericBloodCostTrue <= 1)
							{
								cost = "drop";
								clericBloodCostTrue = 1;
							}
							if (clericBloodCostTrue > 0 && Item.mana > 0)
                            {
								line2.Text = $"Drains {clericBloodCostTrue} blood {cost} and {Item.mana} mana";
							}
							else if (clericBloodCostTrue > 0)
                            {
								line2.Text = $"Drains {clericBloodCostTrue} blood {cost}";
							}
							else if (Item.mana > 0)
                            {
								line2.Text = $"Drains {Item.mana} mana";
							}
							//line2.Text = $"Drains {clericBloodCostTrue} blood {cost} and {Item.mana} mana";
                        }
					}
				}
				//tooltips.Add(new TooltipLine(Mod, "ClericDamageType", "This weapon benefits from necrotic bonuses"));
			}
            else
            {
				float dmg = (float)Math.Round((modPlayer.clericRadiantMult - 1), 2) * 100;
				float dmgOp = (float)Math.Round((modPlayer.clericNecroticMult - 1) * 0.25f, 2) * 100;
				foreach (TooltipLine line2 in tooltips)
				{
					if (line2.Mod == "Terraria" && line2.Name == "Damage")
					{
						int wDmg = (int)(Main.player[Item.whoAmI].GetWeaponDamage(Item, true) * ExtraDamage());
						wDmg += ClericClassPlayer.ModPlayer(Main.player[Item.whoAmI]).clericRadiantAdd;
						wDmg = (int)(wDmg * (ClericClassPlayer.ModPlayer(Main.player[Item.whoAmI]).clericRadiantMult + ((ClericClassPlayer.ModPlayer(Main.player[Item.whoAmI]).clericNecroticMult - 1) * 0.25f)));
						line2.Text = $"{ClassText}{wDmg} radiant damage ({dmg}% radiant + {dmgOp}% necrotic)";
					}
					else if (line2.Name == "UseMana" && healAmount > 0)
					{
						string l = "";
						if (Item.damage <= 0)
                        {
							l = ClassText;
						}
						int healExtra = Main.player[Item.whoAmI].GetModPlayer<excelPlayer>().healBonus;
						line2.Text = $"{l}Uses {Item.mana} mana \nRestores {healAmount+healExtra} health";
					}
				}
				//tooltips.Add(new TooltipLine(Mod, "CerlicHealAMount", $"Restores {healAmount} health points"));
				//tooltips.Add(new TooltipLine(Mod, "ClericDamageType", "This weapon benefits from radiant bonuses"));
			}

			
		}

		public virtual void ExtraTooltipModifications(List<TooltipLine> tooltips)
        {

        }
		public virtual float ExtraDamage()
        {
			return 1;
        }


		public override bool CanUseItem(Player player)
		{
			if (clericBloodCostTrue > 0)
            {
				if ((player.statLife > clericBloodCostTrue) && (player.statMana > Item.mana))
                {
					CombatText.NewText(player.getRect(), Color.Red, clericBloodCostTrue);
					player.statLife -= clericBloodCostTrue;

					int bTime = (Item.damage * (40 - (Item.damage / 10))) - (player.lifeRegen * 5);
					player.AddBuff(ModContent.BuffType<Buffs.ClericCld.AnguishedSoul>(), bTime);
					return true;
                }
				else
                {
					return false;
                }
            }
			return true;
		}
	}
	
	public abstract class ClericHolyWeap : ClericDamageItem
	{
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

        public override bool CanUseItem(Player player)
        {
			if (player.altFunctionUse == 2)
            {
				if (player.HasBuff(ModContent.BuffType<Buffs.ClericCld.BlessingCooldown>()))
				{
					return false;
                }
				return true;
            }
			return true;
        }
    }
}
