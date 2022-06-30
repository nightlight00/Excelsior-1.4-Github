using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace excels.Items.Weapons.Blood
{
    public class ScarletScythe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Releases life-draining slashes");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 58;
            Item.useTime = Item.useAnimation = 43;
            Item.DamageType = DamageClass.Melee;
            Item.width = 70;
            Item.height = 72;
            Item.rare = 4;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.knockBack = 5;
            Item.shoot = ModContent.ProjectileType<ScarletSlash>();
            Item.shootSpeed = 9;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                Dust d = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 5);
                d.scale = Main.rand.NextFloat(1.1f, 1.4f);
                d.noGravity = true;
            }
        }
    }

    public class ScarletSlash : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.alpha = 100;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.width = Projectile.height = 30;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 18;
            Projectile.timeLeft = 72;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.lifeMax > 5 && !target.friendly)
            {
                Projectile.damage = (int)(Projectile.damage * 0.85f);
                if (target.type != NPCID.TargetDummy)
                {
                    Main.player[Projectile.owner].AddBuff(BuffID.SoulDrain, 240, true);
                }
                for (var i = 0; i < 7; i++)
                {
                    Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 182, Projectile.velocity.X, Projectile.velocity.Y);
                    d.scale = Main.rand.NextFloat(0.8f, 1f);
                    d.alpha = 130;
                    d.noGravity = true;
                }
            }
        }

        float rot = 16;
        public override void AI()
        {
            Projectile.velocity *= 0.97f;
            Projectile.alpha += 1;
            Projectile.rotation += MathHelper.ToRadians(rot);
            rot += 0.02f;
            if (Main.rand.NextBool(4))
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 182);
                d.scale = Main.rand.NextFloat(0.4f, 0.6f);
                d.alpha = 200;
                d.noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (var i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 182);
                d.scale = Main.rand.NextFloat(0.9f, 1.1f);
                d.alpha = 100;
                d.noGravity = true;
            }
        }
    }
}
