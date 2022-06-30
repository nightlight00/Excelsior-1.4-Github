using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace excels
{
    public class excelPlayer : ModPlayer
    {
        // buffs
        public int BerserkerCount = 0;
        public bool FlaskFrostfire = false;
        // debuffs
        public bool DebuffMycosis = false;
        // cleric bonuses
        public bool GlacialGuard = false;

        // accessories
        public bool MimicNecklace = false;
        public int SummonBanner = 0;
        public bool ShieldReflect = false;
        public bool BeetleShield = false;
        public bool FriendshipRegen = false;
        // expert
        public bool NiflheimAcc = false;
        public bool ChasmAcc = false;
        public bool StellarAcc = false;

        // armor sets
        public bool GlacialSet = false;
        int GlacialSetReset = 0;
        public bool AvianSet = false; // used in Banner : GlobalProjectile
        public bool FossilSet = false;
        int FossilSetReset = 0;
        public bool StellarSet = false;
        public float StellarDamageBonus = 1;
        public int StellarCritBonus = 0;
        public float StellarUseSpeed = 1;
        public bool WyvernSet = false;
        public bool HeartbeatSet = false;
        public bool HolyKnightSet = false;
        public int HolyKnightSetBonus = 0;
        public bool PriestSet = false;
        public bool FloralSet = false;

        // spirits
        public float SpiritDamageMult = 1;
        public float SpiritAttackSpeed = 1;

        // cleric
        public int healBonus = 0;
        public int buffBonus = 0;

        public int bloodCostMinus = 0;
        public float bloodCostMult = 1;
        public bool AnguishSoul = false;
        // cleric accessories
        public bool antitoxinBottle = false;
        public bool nectarBottle = false;
        public bool glassCross = false;


        public override void ResetEffects()
        {
            // buffs
            BerserkerCount = 0;
            FlaskFrostfire = false;
            // debuffs
            DebuffMycosis = false;
            // cleric bonuses
            GlacialGuard = false;

            // accessories
            MimicNecklace = false;
            SummonBanner = 0;
            ShieldReflect = false;
            BeetleShield = false;
            FriendshipRegen = false;
            // expert
            NiflheimAcc = false;
            ChasmAcc = false;
            StellarAcc = false;

            // armor sets
            GlacialSet = false;
            AvianSet = false;
            FossilSet = false;
            StellarSet = false;
            StellarDamageBonus = 1;
            StellarCritBonus = 0;
            StellarUseSpeed = 1;
            WyvernSet = false;
            HeartbeatSet = false;
            HolyKnightSet = false;
            PriestSet = false;
            FloralSet = false;

            // spirits
            SpiritDamageMult = 1;
            SpiritAttackSpeed = 1;

            // cleric
            healBonus = 0;
            buffBonus = 0;

            bloodCostMinus = 0;
            bloodCostMult = 1;
            AnguishSoul = false;
            // cleric accessories
            antitoxinBottle = false;
            nectarBottle = false;
            glassCross = false;
        }

        public override void PostUpdateRunSpeeds()
        {
            if (GlacialGuard)
            {
                Player.moveSpeed *= 0.66f;
                Player.runAcceleration *= 0.66f;
                Player.maxRunSpeed *= 0.66f;
                Player.accRunSpeed *= 0.66f;
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (DebuffMycosis)
            {
                int dot = 8;
                if (Main.hardMode) {
                    dot += 6;
                    if (NPC.downedPlantBoss) { dot += 10; }
                }
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegen -= dot;
            }
            if (ChasmAcc)
            {
                // since its a negative, need to cancel it out
                Player.lifeRegen += (int)(Player.lifeRegen * -0.2f);
            }
            if (glassCross && Player.lifeRegen < 0)
            {
                Player.GetModPlayer<excelPlayer>().healBonus += 2;
            }
        }

        public override void UpdateLifeRegen()
        {
            if (FriendshipRegen)
            {
                for (var i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.minionSlots > 0)
                    {
                        Player.lifeRegen += 1;
                    }
                }
            }
            if (AnguishSoul && Player.lifeRegen > 0)
            {
                Player.lifeRegen /= 2;
            }
        }

        public override void PreUpdate()
        {
            // using this for variable timers
            if (GlacialSet)
            {
                GlacialSetReset--;
            }
            if (FossilSet)
            {
                FossilSetReset--;
            }
            if (HolyKnightSet)
            {
                if (HolyKnightSetBonus >= 35)
                {
                    Player.statLife += 3;
                    if (Player.statLife > Player.statLifeMax2)
                    {
                        Player.statLife = Player.statLifeMax2;
                    }
                    Player.HealEffect(3);
                    HolyKnightSetBonus -= 35;
                }
            }
        }

        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.DamageType == DamageClass.Ranged && FossilSetReset < 0)
            {
                Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(8)), ModContent.ProjectileType<Items.Weapons.Fossil.FossilChunkR>(), 30, 1, Player.whoAmI);
                FossilSetReset = 23;
            }
            return true;
        }

        public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            if (NiflheimAcc || (FlaskFrostfire && item.DamageType == DamageClass.Melee))
            {   
                Dust d = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 92);
                d.scale = item.scale;
                d.noGravity = true;
                d.noLight = true;
            }
        }

        public override void OnHitPvp(Item item, Player target, int damage, bool crit)
        {
            if (MimicNecklace)
            {
                SoundEngine.PlaySound(SoundID.NPCHit26, Player.Center);
                FakeHitPVP(target, 15, target.getRect());
            }
            if (NiflheimAcc)
            {
                target.AddBuff(BuffID.Frostburn, damage * 40);
            }
            if (FlaskFrostfire && item.DamageType == DamageClass.Melee)
            {
                if (Main.rand.NextBool(3))
                {
                    target.AddBuff(BuffID.Frostburn, 300);
                }
                else
                {
                    target.AddBuff(BuffID.Frostburn, 180);
                }
            }
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (GlacialSet)
            {
                GlacialSetBonus();
            }
            // if returning a hit to an npc, a some basic checks need to be done first
            if (npc.lifeMax > 5 && damage > 1) // prevents from working with spell enemies and if attack was dodged
            {
                if (MimicNecklace)
                {
                    SoundEngine.PlaySound(SoundID.NPCHit26, Player.Center);
                    FakeHitNPC(npc, 35, npc.getRect());
                }
                if (ShieldReflect)
                {
                    FakeHitNPC(npc, (int)(npc.damage * 1.3f), npc.getRect());
                }
            }
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (GlacialSet)
            {
                GlacialSetBonus();
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (NiflheimAcc)
            {
                target.AddBuff(BuffID.Frostburn, damage * 40);
            }
            if (FlaskFrostfire && item.DamageType == DamageClass.Melee)
            {
                if (Main.rand.NextBool(3))
                {
                    target.AddBuff(BuffID.Frostburn, 300);
                }
                else
                {
                    target.AddBuff(BuffID.Frostburn, 180);
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            Item item = Player.HeldItem;
            if (NiflheimAcc) // && (Main.rand.Next(70) < (item.useAnimation + (item.mana * 2))))
            {
                int dmg = damage * (Math.Abs(60 - item.useTime) / 5);
                target.AddBuff(BuffID.Frostburn, dmg * 60);
            }
            if (FlaskFrostfire && proj.DamageType == DamageClass.Melee)
            {
                if (Main.rand.NextBool(3))
                {
                    target.AddBuff(BuffID.Frostburn, 300);
                }
                else
                {
                    target.AddBuff(BuffID.Frostburn, 180);
                }
            }
            if (AvianSet && (proj.DamageType == DamageClass.Summon || proj.minionSlots > 0) && proj.type != ModContent.ProjectileType<Items.Armor.Avian.AvianSkyFeather>())
            {
                for (var i = 0; i < 3; i++) {
                    if (Main.rand.NextBool((int)((5 - proj.minionSlots) * (i * 1.7f + 1))))
                    {
                        Vector2 pos = target.position + new Vector2(Main.rand.Next(-120, 121), -400);
                        Vector2 vel = (target.Center - pos).SafeNormalize(Vector2.Zero) * 11;

                        Projectile.NewProjectile(proj.GetSource_FromThis(), pos, vel, ModContent.ProjectileType<Items.Armor.Avian.AvianSkyFeather>(),
                            14, 2, Main.player[proj.owner].whoAmI);
                    }
                }
            }
        }

        #region Hit Effects
        public void GlacialSetBonus()
        {
            if (GlacialSetReset < 0)
            {
                // creates six ice shards
                for (var i = 0; i < 6; i++) {
                    Projectile p = Projectile.NewProjectileDirect(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<Items.Armor.Glacial.GlacialShard>(), 20, 1, Player.whoAmI);
                    p.ai[0] = (3.6f / 3.5f) * (i + 1);
                }

                GlacialSetReset = 300;
            }
        }

        public void FakeHitNPC(NPC target, int damage, Rectangle position)
        {
            if (target.immortal)
            {
                return;
            }
            target.life -= damage;
            target.HitEffect();
            SoundEngine.PlaySound(target.HitSound.Value, target.Center);
            CombatText.NewText(position, Color.Orange, damage);
            if (target.life <= 0)
            {
                target.checkDead();
            }
        }

        public void FakeHitPVP(Player target, int damage, Rectangle position)
        {
            target.statLife -= damage;
            CombatText.NewText(position, Color.Orange, damage);
            if (target.statLife <= 0)
            {
                target.KillMe(PlayerDeathReason.ByCustomReason($"{target.name} was biten"), 40, 0, true);
            }
        }
        #endregion
    }

    public class excelItem : GlobalItem
    {
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            // This method shows adding items to Fishrons boss bag. 
            // Typically you'll also want to also add an item to the non-expert boss drops, that code can be found in ExampleGlobalNPC.NPCLoot. Use this and that to add drops to bosses.
            if (context == "bossBag" && arg == ItemID.QueenBeeBossBag && Main.rand.NextBool(3))
            {
                player.QuickSpawnItem(player.GetSource_DropAsItem(), ModContent.ItemType<Items.Accessories.Cleric.Healing.NectarBottle>());
            }
            if (context == "bossBag" && arg == ItemID.WallOfFleshBossBag && Main.rand.NextBool(3))
            {
                player.QuickSpawnItem(player.GetSource_DropAsItem(), ModContent.ItemType<Items.Accessories.Cleric.Damage.ClericEmblem>());
            }

            if (context == "crate")
            {
                switch (arg)
                {
                    case ItemID.WoodenCrate:
                    case ItemID.WoodenCrateHard:
                        if (Main.rand.NextBool(3))
                        {
                            switch (Main.rand.Next(2))
                            {
                                case 0:
                                    player.QuickSpawnItem(player.GetSource_DropAsItem(), ModContent.ItemType<Items.Accessories.Cleric.Healing.ApothSatchel>());
                                    break;

                                case 1:
                                    player.QuickSpawnItem(player.GetSource_DropAsItem(), ModContent.ItemType<Items.Accessories.Banner.RegenBanner>());
                                    break;
                            }
                        }
                        if (Main.rand.NextBool(4))
                        {
                            switch (Main.rand.Next(2))
                            {
                                case 0:
                                    player.QuickSpawnItem(player.GetSource_DropAsItem(), ModContent.ItemType<Items.Misc.Herbs.Gladiolus>(), Main.rand.Next(1, 3));
                                    break;

                                case 1:
                                    player.QuickSpawnItem(player.GetSource_DropAsItem(), ModContent.ItemType<Items.Misc.Herbs.GladiolusSeeds>(), Main.rand.Next(2, 5));
                                    break;
                            }
                        }
                        break;

                    case ItemID.GoldenCrate:
                    case ItemID.GoldenCrateHard:
                        if (Main.rand.NextBool(3))
                        {
                            player.QuickSpawnItem(player.GetSource_DropAsItem(), ModContent.ItemType<Items.Potions.Potions.SweetPotion>(), Main.rand.Next(1, 4));
                        }
                        break;

                    case ItemID.FrozenCrateHard:
                        if (Main.rand.NextBool(3))
                        {
                            switch (Main.rand.Next(2))
                            {
                                case 0:
                                    player.QuickSpawnItem(player.GetSource_DropAsItem(), ModContent.ItemType<Items.Materials.GlacialBar>(), Main.rand.Next(1, 4));
                                    break;

                                case 1:
                                    player.QuickSpawnItem(player.GetSource_DropAsItem(), ModContent.ItemType<Items.Materials.GlacialOre>(), Main.rand.Next(2, 7));
                                    break;
                            }
                        }
                        break;

                    case ItemID.FloatingIslandFishingCrate:
                    case ItemID.FloatingIslandFishingCrateHard:
                        if (Main.rand.NextBool(3))
                        {
                            switch (Main.rand.Next(2))
                            {
                                case 0:
                                    player.QuickSpawnItem(player.GetSource_DropAsItem(), ModContent.ItemType<Items.Materials.SkylineBar>(), Main.rand.Next(1, 4));
                                    break;

                                case 1:
                                    player.QuickSpawnItem(player.GetSource_DropAsItem(), ModContent.ItemType<Items.Materials.SkylineOre>(), Main.rand.Next(2, 7));
                                    break;
                            }
                        }
                        break;

                    case ItemID.DungeonFishingCrate:
                    case ItemID.DungeonFishingCrateHard:
                        if (Main.rand.NextBool(4))
                        {
                            player.QuickSpawnItem(player.GetSource_DropAsItem(), ModContent.ItemType<Items.Materials.ShatteredHeartbeat>(), Main.rand.Next(1, 3));
                        }
                        break;
                }
            }
        }
    }
}
