using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;


namespace excels.Dusts
{
    internal class EnergyDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 1.1f;
        //    dust.scale = 1.3f;
            dust.noGravity = true;
            dust.alpha = 100;
        }
        
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return Color.White;
        }

        public override bool Update(Dust dust)
        {
            if (!dust.noGravity)
            {
                return true;
                dust.velocity.Y += 0.02f;
                dust.velocity.X *= 0.97f;
            }
            else
            {
                dust.velocity *= 0.987f;
            }

            if (dust.velocity.X < 0.1f || dust.velocity.X > -0.1f)
            {
                dust.rotation += MathHelper.ToRadians(dust.velocity.X * 2);
            }

            if (!dust.noLight)
            {
                Lighting.AddLight(dust.position, 1.14f * dust.scale * 0.16f, 2.36f * dust.scale * 0.16f, 2.55f * dust.scale * 0.16f);
            }

            dust.scale -= 0.05f;
            if (dust.scale < 0.3f)
            {
                dust.active = false;
            }

            return false;
        }
    }

    internal class ChasmHeadDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.scale = Main.rand.NextFloat(1, 1.3f);
        }

        public override bool Update(Dust dust)
        {
            return true;
        }
    }

    internal class ChasmBodyDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.scale = Main.rand.NextFloat(1, 1.3f);
        }

        public override bool Update(Dust dust)
        {
            return true;
        }
    }

    internal class GlacialDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.scale = Main.rand.NextFloat(1, 1.2f);
        }

        public override bool Update(Dust dust)
        {
            return true;
        }
    }

    internal class SkylineDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.scale = Main.rand.NextFloat(1, 1.2f);
        }

        public override bool Update(Dust dust)
        {
            return true;
        }
    }

    internal class FossilBoneDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.scale = Main.rand.NextFloat(1.15f, 1.35f);
        }

        public override bool Update(Dust dust)
        {
            return true;
        }
    }
}
