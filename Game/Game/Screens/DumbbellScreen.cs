﻿using Game.Text;
using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Screens
{
    public class DumbbellScreen : GameScreen
    {
        private SpriteAnimation dumbbellAnimation;
        private Texture2D dumbbellSprite, avatar, bubbleBox, background;
        private SpriteBatch spriteBatch;
        private ContentManager Content;
        private int counter = 0;
        private PlayScreen playScreen;
        private Bar bar;
        private SpriteFont font;

        public DumbbellScreen(PlayScreen playScreen)
        {
            this.playScreen = playScreen;
            bar = playScreen.bar;
        }

        public override void Initialize()
        {
            Content = ScreenManager.Game.Content;
            spriteBatch = ScreenManager.SpriteBatch;           
            dumbbellAnimation = new SpriteAnimation();
            Constants.ResetDumbbellsAndRun();

            enablePause = true;
            base.Initialize();
        }

        public override void LoadContent()
        {
            dumbbellSprite = Content.Load<Texture2D>("Sprites/dumbbell-sprite");
            background = Content.Load<Texture2D>("Textures//Gym-Interior");
            avatar = Content.Load<Texture2D>("Textures/avatar");
            bubbleBox = Content.Load<Texture2D>("Textures/bubbleBoxDumb");
            font = Content.Load<SpriteFont>("Fontopo");
            dumbbellAnimation.Initialize(dumbbellSprite, new Vector2(600, 500), 200, 262, 12, 100, Color.White, 1f, true); 
            base.LoadContent();
        }

        
        public override void Update(GameTime gameTime)
        {
            dumbbellAnimation.Update(gameTime);
            if (Constants.isDumbbell)
            {
                playScreen.GetPlayer().Collided(Constants.dumbbellEffect);
                Constants.ResetFlags();
            }
            if (counter == 600)
            {
                this.Remove();
                playScreen.player.ReInitializeRunAnimation();
                playScreen.UnfreezeScreen();
            }

            counter++;
            playScreen.bar.Update(gameTime);
            playScreen.bar.SetCurrentValue(playScreen.player.Immunity);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 720),Color.White);
            spriteBatch.DrawString(font, "Lifts: "+Constants.numberOfDumbbells, new Vector2(400, 10), Color.Red);
            dumbbellAnimation.Draw(spriteBatch);
            #region Tamer Avatar +bubble box draw
            spriteBatch.Draw(avatar, new Rectangle(10, 400, avatar.Width * 2, avatar.Height * 2), Color.White);
            spriteBatch.Draw(bubbleBox, new Rectangle(avatar.Width, 380, bubbleBox.Width, bubbleBox.Height * 2), Color.White);
            #endregion
            playScreen.bar.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
