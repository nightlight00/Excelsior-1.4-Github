using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace excels
{
    public class excelNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;


        public bool DebuffMycosis = false;
        public int MarkedTimer = 0;

        public int SpellCurse = 0;

        public override void SetDefaults(NPC npc)
        {
            /* slimes,
            if (npc.aiStyle == 1 || npc.type == NPCID.MeteorHead || npc.type == NPCID.Snatcher || npc.type == NPCID.AngryTrapper || npc.type == NPCID.GraniteGolem || npc.type == NPCID.GraniteFlyer || npc.type == NPCID.Dandelion || npc.type == NPCID.MartianDrone || npc.type == NPCID.MartianProbe || npc.type == NPCID.MartianSaucer || npc.type == NPCID.MartianWalker || npc.type == NPCID.Mimic || npc.type == NPCID.BigMimicCorruption || npc.type == NPCID.BigMimicCrimson || npc.type == NPCID.BigMimicHallow || npc.type == NPCID.BigMimicJungle || npc.type == NPCID.IceMimic || npc.type == NPCID.PresentMimic || npc.type == NPCID.Golem || npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight || npc.type == NPCID.GolemHead || npc.type == NPCID.RockGolem || npc.type == NPCID.IceGolem || npc.type == NPCID.Plantera || npc.type == NPCID.KingSlime || npc.type == NPCID.QueenSlimeBoss || npc.type == NPCID.CrimsonAxe || npc.type == NPCID.CursedHammer || npc.type == NPCID.EnchantedSword || npc.type == NPCID.DeadlySphere || npc.type == NPCID.DungeonSpirit || npc.type == NPCID.Everscream || npc.type == NPCID.MourningWood || npc.type == NPCID.PirateShipCannon || npc.type == NPCID.IceElemental || npc.type == NPCID.ManEater || npc.type == NPCID.Pixie || npc.type == NPCID.Pumpking || npc.type == NPCID.SantaNK1 || npc.type == NPCID.MartianTurret || npc.type == NPCID.Reaper || npc.type == NPCID.Wraith || npc.type == NPCID.PossessedArmor)
            { 
                 NPCID.Sets.DebuffImmunitySets.Add(npc.type, new NPCDebuffImmunityData
                    {
                    SpecificallyImmuneTo = new int[] {
                        ModContent.BuffType<Buffs.Debuffs.FragileBones>(),
                    }
                });
            }
            */
        }

        public override void ResetEffects(NPC npc)
        {
            DebuffMycosis = false;
        }

        public override bool PreAI(NPC npc)
        {
            MarkedTimer--;
            SpellCurse--;
            if (MarkedTimer > 0)
            {
                for (var i = 0; i < 3; i++)
                {
                    for (var d = 0; d < 4; d++)
                    {
                        Vector2 vel = Vector2.One.RotatedBy(MathHelper.ToRadians(90 * d));
                        Dust dst = Dust.NewDustDirect(npc.Center + vel * (i * 4), 0, 0, 27);
                        dst.noGravity = true;
                        dst.scale = 1.5f - (i * 0.35f);
                        dst.velocity = vel * 8;
                    }
                }
            }
            return base.PreAI(npc);
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.AngryNimbus)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapons.MageGun.StormCaller>(), 17));
            }
            if (npc.type == NPCID.GoblinSummoner)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Accessories.Banner.ShadowflameBanner>(), 3));
            }
            if (npc.type == NPCID.GraniteGolem || npc.type == NPCID.GraniteFlyer)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Materials.EnergizedGranite>(), 5, 2, 3));
            }
            if (npc.type == NPCID.GoblinShark)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapons.Blood.ScarletScythe>(), 4));
            }
            if (npc.type == NPCID.SandShark || npc.type == NPCID.SandsharkHallow || npc.type == NPCID.SandsharkCorrupt || npc.type == NPCID.SandsharkCrimson)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Items.Materials.AncientFossil>(), 6, 4));
            }
            if (npc.type == NPCID.SnowBalla || npc.type == NPCID.SnowmanGangsta || npc.type == NPCID.MisterStabby)
            {
                if (npc.type != NPCID.MisterStabby)
                {
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Vanity.MafiosoHat>(), 50));
                }
                if (npc.type == NPCID.SnowmanGangsta)
                {
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapons.Guns1.TommyGun>(), 40));
                }
                npcLoot.Add(ItemDropRule.Food(ModContent.ItemType<Items.Food.Carrot>(), 75));
            }
            if (npc.type == NPCID.Drippler)
            {
                npcLoot.Add(ItemDropRule.OneFromOptionsWithNumerator(90, 3, ModContent.ItemType<Items.Weapons.Blood.DripplingStick>()));
            }
            if (npc.type == NPCID.WyvernHead)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Materials.WyvernScale>(), 1, 2, 4));
            }
            if (npc.type == NPCID.Nurse)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.WeaponHeal.Generic.Syringe>(), 1, 40, 60));
            }

            // Boss drops
            if (npc.type == NPCID.QueenBee)
            {
                LeadingConditionRule leadingConditionRule = new LeadingConditionRule(new Conditions.NotExpert());
                leadingConditionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Accessories.Cleric.Healing.NectarBottle>(), 3));
                npcLoot.Add(leadingConditionRule);
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                LeadingConditionRule leadingConditionRule = new LeadingConditionRule(new Conditions.NotExpert());
                leadingConditionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Accessories.Cleric.Damage.ClericEmblem>(), 3));
                npcLoot.Add(leadingConditionRule);
            }
        
        }

        public override void HitEffect(NPC npc, int hitDirection, double damage)
        {
            if (npc.type == NPCID.MisterStabby && npc.life <= 0)
            {
                if (Main.rand.NextBool(6) || (Main.expertMode && Main.rand.NextBool(3)))
                {
                    NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<NPCs.Snow.StabbyJr>());
                }
            }
            if (npc.type == NPCID.EyeofCthulhu && Main.moonPhase == 0 && npc.life <= 0)
            {
                Item.NewItem(npc.GetSource_FromThis(), npc.getRect(), ModContent.ItemType<Items.Weapons.Midnight.LunarReflection>());
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (DebuffMycosis)
            {
                int dot = 8;
                if (Main.hardMode) { 
                    dot += 8; 
                    if (NPC.downedPlantBoss) { dot += 12; }
                }
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= dot;
                if (damage < (dot / 4))
                {
                    damage = (dot / 4);
                }
                //npc.color = new Color(1.13f, 1.46f, 2.45f);
            }
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            if (npc.type == NPCID.SporeBat || npc.type == NPCID.SporeSkeleton || npc.type == NPCID.FungiSpore)
            {
                // 1/3 chance to inflict mycosis for 6-9 seconds
                if (Main.rand.NextBool(3))
                {
                    target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Mycosis>(), (6 + Main.rand.Next(4)) * 60);
                }
            }
            if (npc.type == NPCID.FungiBulb || npc.type == NPCID.MushiLadybug|| npc.type == NPCID.AnomuraFungus)
            {
                // guaranteed chance to inflict mycosis for 7-11 seconds
                target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Mycosis>(), (7 + Main.rand.Next(5)) * 60);
            }
            if (npc.type == NPCID.FungoFish || npc.type == NPCID.GiantFungiBulb)
            {
                // guaranteed chance to inflict mycosis for 14-20 seconds
                target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Mycosis>(), (14 + Main.rand.Next(7)) * 60);
            }
            if (target.ZoneGlowshroom && Main.rand.NextBool(13))
            {
                int scale = 1;
                if (Main.hardMode) { 
                    scale++; 
                    if (NPC.downedPlantBoss) { scale++; }
                }
                int dot = (int)(6 + (npc.damage / (5 - scale)) * (60 * (0.8f + (scale * 0.1f))));
                target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Mycosis>(), dot);
            }
          
        }
        
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        { 
            if (NPC.downedPlantBoss && type == NPCID.WitchDoctor)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Accessories.Banner.VenomBanner>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 30);
                nextSlot++;
            }
        }

        /*
        public override void GetChat(NPC npc, ref string chat)
        {
            if (NPC.downedBoss1 && npc.type == NPCID.Demolitionist && NPC.CountNPCS(NPCID.ArmsDealer) == -1 && Main.rand.NextBool(5))
            {
                // currently disabled sine doesnt work properly
                // after eoc is defeated and arm dealer present, demo has 1/5 chance of talking about his gun
                switch (Main.rand.Next(3))
                {
                    case 1: chat = "You thought " + Main.npc[NPC.FindFirstNPC(NPCID.PartyGirl)].GivenName + " had an impressive gun?  Think again!"; break;
                    case 2: chat = "This bad boy has some extreme firepower!  There's none quite like it!"; break;
                    default: chat = Main.npc[NPC.FindFirstNPC(NPCID.PartyGirl)].GivenName + " isn't the only one here who has a firearm.  And he's not the only on who'll sell it either."; break;
                }
            }
        }
        */
    }
}
