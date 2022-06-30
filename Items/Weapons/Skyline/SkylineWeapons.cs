using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace excels.Items.Weapons.Skyline
{
    #region Staff
    public class SkylineStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plummage Staff");
            Tooltip.SetDefault("Tickles foes with deadly feathers");
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Magic;
            Item.damage = 16;
            Item.useTime = 7;
            Item.useAnimation = 21;
            Item.reuseDelay = 9;
            Item.knockBack = 2.3f;
            Item.noMelee = true;
            Item.height = Item.width = 46;
            Item.rare = 1;
            Item.mana = 8;
            Item.shoot = ModContent.ProjectileType<SkylineFeather>();
            Item.shootSpeed = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Items.Materials.SkylineBar>(), 6)
                .AddIngredient(ItemID.Feather, 6)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(6));
        }
    }

    public class SkylineFeather : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.HarpyFeather}";

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.HarpyFeather);
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 120;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 200;
        }

        public override void AI()
        {
            Projectile.alpha -= 10;
            if (Main.rand.NextBool(3))
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 15, newColor: new Color(0, 180, 230));
                d.noGravity = true;
                d.alpha = 150;
                d.scale = 0.9f;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (var i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 15, newColor: new Color(0, 180, 230));
                d.noGravity = true;
                d.alpha = 80;
                d.scale = 1.3f;
            }
        }
    }
    #endregion

    #region Bow
    public class SkylineBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drizzlebow");
            Tooltip.SetDefault("A soft downpour of arrows");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 15;
            Item.useTime = 9;
            Item.useAnimation = 18;
            Item.reuseDelay = 14;
            Item.autoReuse = true;
            Item.knockBack = 3.4f;
            Item.noMelee = true;
            Item.height = 36;
            Item.width = 16;
            Item.rare = 1;
            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = 10;
            Item.shootSpeed = 9;
            Item.useStyle = ItemUseStyleID.Shoot;
           // Item.UseSound = SoundID.Item43;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Items.Materials.SkylineBar>(), 6)
                .AddIngredient(ItemID.Feather, 2)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y - 200f)
            {
                ceilingLimit = player.Center.Y - 200f;
            }
            position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
            position.X = (player.Center.X + target.X) / 2 + Main.rand.NextFloat(-80, 80);
            position.Y -= 100;
            Vector2 heading = target - position;

            if (heading.Y < 0f)
            {
                heading.Y *= -1f;
            }

            if (heading.Y < 20f)
            {
                heading.Y = 20f;
            }

            heading.Normalize();
            heading *= velocity.Length();
            heading.Y += Main.rand.Next(-40, 41) * 0.02f;
            Projectile.NewProjectile(source, position, heading, type, damage, knockback, player.whoAmI);

            SoundEngine.PlaySound(SoundID.Item5, player.Center);
            for (var i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustDirect(position, 0, 0, 15, newColor: new Color(0, 180, 230));
                d.noGravity = true;
                d.alpha = 80;
                d.scale = 1.3f;
            }
            return false;
        }
    }
    #endregion

    #region Yoyo
    public class SkylineYoyo : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arial");
            ItemID.Sets.Yoyo[Item.type] = true;
            ItemID.Sets.GamepadExtraRange[Item.type] = 15;
            ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee;
            Item.damage = 17;
            Item.useTime = Item.useAnimation = 25;
            Item.knockBack = 1.8f;
            Item.noMelee = true;
            Item.height = 22;
            Item.width = 26;
            Item.rare = 1;
            Item.shoot = ModContent.ProjectileType<SkylineYoyoProj>();
            Item.shootSpeed = 9;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.channel = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Items.Materials.SkylineBar>(), 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }

    public class SkylineYoyoProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arial");
            // Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 7f;
            // Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 240f;
            // Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 18.5f;
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 14;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 0;
        }
    }
    #endregion

    #region Summon 

    public class SkylineCaneBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lil' Sentinal");
            Description.SetDefault("Danger from above");

            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = true; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // If the minions exist reset the buff time, otherwise remove the buff from the player
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SkylineCaneMinion>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }

    public class SkylineCane : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skyline Cane");
            Tooltip.SetDefault("Summons a lil' sentinal to fight for you");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; 
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Summon;
            Item.damage = 11;
            Item.useTime = Item.useAnimation = 25;
            Item.knockBack = 2.1f;
            Item.noMelee = true;
            Item.height = Item.width = 42;
            Item.rare = 1;
            Item.shoot = ModContent.ProjectileType<SkylineCaneMinion>();
            Item.buffType = ModContent.BuffType<SkylineCaneBuff>();
            Item.shootSpeed = 1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.mana = 10;
            Item.UseSound = SoundID.Item44;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Items.Materials.SkylineBar>(), 6)
                .AddIngredient(ItemID.Feather, 4)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);

            // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
            projectile.originalDamage = Item.damage;
            return false;
        }
    }

    public class SkylineCaneMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lil' Sentinal");
            Main.projFrames[Projectile.type] = 6;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // Projectile is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to Projectile Projectile, as it's resistant to all homing Projectiles.
        }

        public sealed override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.tileCollide = false;

            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
        }

        public override bool? CanCutTiles() => false;

        public override bool MinionContactDamage() => false;

        protected float viewDist = 450f;
        protected float maxSpeed = 3;
        protected float idleAmt = 0.3f;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // if owner alive and buff handling
            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<SkylineCaneBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<SkylineCaneBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            #region Check for Target
            Vector2 targetPos = Projectile.position;
            float targetDist = viewDist;
            bool target = false;
            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                // changed so that it uses checks if player has line of sight with npc, not the actual summon
                if (Collision.CanHitLine(player.position, player.width, player.height, npc.position, npc.width, npc.height))
                {
                    targetDist = Vector2.Distance(Projectile.Center, targetPos);
                    targetPos = npc.Center;
                    target = true;
                }
            }
            else
            {
                for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.CanBeChasedBy(this, false))
                    {
                        float distance = Vector2.Distance(npc.Center, Projectile.Center);
                        if ((distance < targetDist || !target) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                        {
                            targetDist = distance;
                            targetPos = npc.Center;
                            target = true;
                        }
                    }
                }
            }

            // idle position
            if (!target)
            {
                Vector2 direction = player.Center;
                Projectile.ai[1] = 3600f;
                Projectile.netUpdate = true;
                int num = 1;
                for (int k = 0; k < Projectile.whoAmI; k++)
                {
                    if (Main.projectile[k].active && Main.projectile[k].owner == Projectile.owner && Main.projectile[k].type == Projectile.type)
                    {
                        num++;
                    }
                }
                direction.X -= (float)((10 + num * 40) * player.direction);
                direction.Y -= 40f;

                targetPos = direction;
            }
            #endregion

            #region Visuals
            Projectile.rotation = Projectile.velocity.X * 0.05f;
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }
            if (Projectile.velocity.X > 0f)
            {
                Projectile.spriteDirection = Projectile.direction = -1;
            }
            else if (Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = Projectile.direction = 1;
            }
            #endregion

            // this handles different movement options
            if (target)
            {
                maxSpeed = 4;
                // only adjust speed if distant to target
                if (Vector2.Distance(targetPos, Projectile.Center) > 140)
                {
                    AdjustVelocity(targetPos, 1.2f);
                }
                // only shooting can occur if there is a target
                Projectile.ai[0]++;
                if (Main.rand.NextBool(5))
                {
                    Projectile.ai[0]++;
                }
                if (Projectile.ai[0] > 30 && Vector2.Distance(targetPos, Projectile.Center) < 300) // idk the actual range of the projectile just a guess
                {
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Vector2 shootVel = targetPos - Projectile.Center;
                        if (shootVel == Vector2.Zero)
                        {
                            shootVel = new Vector2(0f, 1f);
                        }
                        shootVel.Normalize();
                        shootVel *= 5;
                        int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<SkylineCaneProj>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                        Main.projectile[proj].netUpdate = true;
                        Projectile.netUpdate = true;
                        Projectile.ai[0] = 0;
                    }
                }
            }
            else
            {
                // increases max speed if far from player
                if (Vector2.Distance(targetPos, Projectile.Center) > 500) {
                    maxSpeed = 7;
                }
                else {
                    maxSpeed -= 0.05f;
                    if (maxSpeed < 1.4f)
                    {
                        maxSpeed = 2;
                    }
                }
                AdjustVelocity(targetPos, idleAmt);
                // semi prepared to attack, but not completely (just so feels more alert)
                Projectile.ai[0] = 12;
                if (++Projectile.ai[1] > 60)
                {
                    // add a little randomness to idle speed
                    idleAmt = Main.rand.NextFloat(0.25f, 0.36f);
                    Projectile.ai[1] = 0;
                }
            }
        }

        private void AdjustVelocity(Vector2 pos, float mult)
        {
            if (pos.X > Projectile.Center.X)
            {
                Projectile.velocity.X += 0.2f * mult;
                if (Projectile.velocity.X > maxSpeed) { Projectile.velocity.X = maxSpeed; }
            }
            else
            {
                Projectile.velocity.X -= 0.2f * mult;
                if (Projectile.velocity.X < -maxSpeed) { Projectile.velocity.X = -maxSpeed; }
            }
            if (pos.Y > Projectile.Center.Y - 40)
            {
                Projectile.velocity.Y += 0.1f * mult;
                if (Projectile.velocity.Y > maxSpeed) { Projectile.velocity.Y = maxSpeed; }
            }
            else
            {
                Projectile.velocity.Y -= 0.2f * mult;
                if (Projectile.velocity.Y < -maxSpeed) { Projectile.velocity.Y = -maxSpeed; }
            }
        }
    }
    
    public class SkylineCaneProj : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.HarpyFeather}";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mini Space Laser");
            ProjectileID.Sets.MinionShot[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.timeLeft = 69;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.extraUpdates = 5;
        }
        public override bool MinionContactDamage() => true;

        public override void AI()
        {
            Projectile.ai[0]++;
            // so it appears to spawn in front and not on top of the sentinal
            if (Projectile.ai[0] > 6)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 15, newColor: new Color(0, 180, 230));
                d.noGravity = true;
                d.velocity *= 0.25f;
                d.alpha = 150;
                d.scale = 0.9f;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (var i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 15, newColor: new Color(0, 180, 230));
                d.noGravity = true;
                d.alpha = 80;
                d.scale = 1.3f;
            }
        }
    }

    #endregion 
}
