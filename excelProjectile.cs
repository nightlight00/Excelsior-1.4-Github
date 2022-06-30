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
    internal class excelProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public int HealStrength = -1;
        public float HealMult = 1;

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.type == ProjectileID.Shroomerang)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Mycosis>(), 150);
            }
            if (Main.player[projectile.owner].GetModPlayer<excelPlayer>().NiflheimAcc)
            {
                target.AddBuff(BuffID.Frostburn, damage * 40);
            }
        }


        public override void OnHitPvp(Projectile projectile, Player target, int damage, bool crit)
        {
            if (projectile.type == ProjectileID.Shroomerang)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Mycosis>(), 150);
            }
            if (Main.player[projectile.owner].GetModPlayer<excelPlayer>().NiflheimAcc)
            {
                target.AddBuff(BuffID.Frostburn, damage * 40);
            }
        }


        public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
        {
            //if (projectile.type == ProjectileID.
        }

        public override void AI(Projectile projectile)
        {
            if (Main.player[projectile.owner].GetModPlayer<excelPlayer>().NiflheimAcc)
            {
                if (Main.rand.NextBool(3))
                {
                    Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 92);
                    d.scale = projectile.scale;
                    d.velocity = projectile.velocity * Main.rand.NextFloat(0.2f, 0.4f);
                    d.noLight = true;
                    d.noGravity = true;
                }
            }
        }
    }
}
