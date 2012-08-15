﻿using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using Game.Text;

namespace Game.Screens
{
    public class DumbbellScreen : GameScreen
    {
        Sprite background;
        SpriteAnimation dumbbellAnimation;
        Texture2D dumbbellSprite, avatar, bubbleBox;
        SpriteBatch spriteBatch;
        ContentManager Content;
        int counter = 0;
        PlayScreen playScreen;
        int lifts = 0;
        Bar bar;

        public DumbbellScreen(PlayScreen playScreen)
        {
            this.playScreen = playScreen;
            bar = playScreen.bar;
        }

        public override void Initialize()
        {
            Content = ScreenManager.Game.Content;
            spriteBatch = ScreenManager.SpriteBatch;
            dumbbellSprite = Content.Load<Texture2D>("Sprites/dumbbell-sprite");
            dumbbellAnimation = new SpriteAnimation();

            dumbbellAnimation.Initialize(dumbbellSprite, new Vector2(600, 500), 200, 262, 12, 100, Color.White, 1f, true);    
            base.Initialize();
        }

        public override void LoadContent()
        {
            background = new Sprite(Content.Load<Texture2D>("Textures//Gym-Interior"), new Rectangle(0, 0, 1280, 720));
            avatar = Content.Load<Texture2D>("Textures/avatar");
            bubbleBox = Content.Load<Texture2D>("Textures/bubbleBoxDumb");
            base.LoadContent();
        }

        
        public override void Update(GameTime gameTime)
        {
            dumbbellAnimation.Update(gameTime);
            
            if (Constants.isDumbbell)
            {
                lifts++;
                Constants.ResetFlags();
            }           

            if (counter == 600)
            {
                this.Remove();
                for (int i = 0; i <= lifts-1; i++)
                {
                    playScreen.GetPlayer().Collided(5);
                }
                playScreen.UnfreezeScreen();
            }

            counter++;
            playScreen.bar.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            background.Draw(spriteBatch);
            SpriteFont font = Content.Load<SpriteFont>("Fontopo");            
            SpriteBatch sprite = spriteBatch;
            sprite.Begin();
            spriteBatch.DrawString(font, "Lifts: " + lifts, new Vector2(400, 10), Color.Red);
            dumbbellAnimation.Draw(spriteBatch);
            spriteBatch.Draw(avatar, new Rectangle(10, 400, avatar.Width * 2, avatar.Height * 2), Color.White);
            spriteBatch.Draw(bubbleBox, new Rectangle(avatar.Width, 380, bubbleBox.Width, bubbleBox.Height * 2), Color.White);
            sprite.End();
            playScreen.bar.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
