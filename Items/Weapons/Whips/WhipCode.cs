using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent;

namespace excels.Items.Weapons.Whips
{
    /*
    #region Obsidian
    internal class ObsidianLash : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SummonerWeaponThatScalesWithAttackSpeed[Type] = true; // This makes the item be affected by the user's melee speed.
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BoneWhip);
            Item.useTime = Item.useAnimation = 40;
            Item.knockBack = 6;
            Item.damage = 36;
            Item.shoot = ModContent.ProjectileType<ObsidianLashP>();
        }
    }

    public class ObsidianLashP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BoneWhip);
            AIType = ProjectileID.BoneWhip;
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.IsAWhip[Type] = true; // This makes the projectile use whip collision detection and allows flasks to be applied to it.
        }

        private float Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
        }

        // This method draws a line between all points of the whip, in case there's empty space between the sprites.
        private void DrawLine(SpriteBatch spriteBatch, List<Vector2> list)
        {
            Texture2D texture = TextureAssets.FishingLine.Value;

            Rectangle frame = texture.Frame();
            Vector2 origin = new Vector2(frame.Width / 2, 2);

            Vector2 pos = list[0];
            for (int i = 0; i < list.Count - 1; i++)
            {
                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2;
                Color color = Lighting.GetColor(element.ToTileCoordinates(), Color.White);
                Vector2 scale = new Vector2(1f, (diff.Length() + 2) / frame.Height);

                spriteBatch.Draw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0f);

                pos += diff;
            }
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spriteBatch;

            List<Vector2> list = new List<Vector2>();
            Projectile.FillWhipControlPoints(Projectile, list);

            DrawLine(spriteBatch, list);

            // If you don't want to use custom code, you can instead call one of vanilla's DrawWhip methods. However, keep in mind that you must adhere to how they draw if you do.
            // Main.DrawWhip_WhipBland(Projectile, list);	SpriteEffects flip = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            SpriteEffects flip = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 pos = list[0];

            // The frame and origin values in this loop were chosen to suit this projectile's sprite, but won't necessarily work for your own. Feel free to change them if they don't!
            for (int i = 0; i < list.Count - 1; i++)
            {
                Rectangle frame = new Rectangle(0, 0, 10, 26);
                Vector2 origin = new Vector2(5, 8);
                float scale = 1;

                // These statements determine what part of the spritesheet to draw for the current segment, and can also be changed.
                if (i == list.Count - 2)
                {
                    frame.Y = 74;
                    frame.Height = 18;

                    // To make it look more impactful, this scales the tip of the whip up when fully extended, and down when curled up.
                    Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out int _, out float _);
                    float t = Timer / timeToFlyOut;
                    scale = MathHelper.Lerp(0.5f, 1.5f, Utils.GetLerpValue(0.1f, 0.7f, t, true) * Utils.GetLerpValue(0.9f, 0.7f, t, true));
                }
                else if (i > 10)
                {
                    frame.Y = 58;
                    frame.Height = 16;
                }
                else if (i > 5)
                {
                    frame.Y = 42;
                    frame.Height = 16;
                }
                else if (i > 0)
                {
                    frame.Y = 26;
                    frame.Height = 16;
                }

                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2; // This projectile's sprite faces down, so PiOver2 is again used to correct rotation.
                Color color = Lighting.GetColor(element.ToTileCoordinates());

                spriteBatch.Draw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, flip, 0);

                pos += diff;
            }
            return false;
        }
    }
    #endregion
    */

}

