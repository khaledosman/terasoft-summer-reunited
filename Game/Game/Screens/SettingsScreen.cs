﻿using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game.Text;

namespace Game.Screens
{
    class SettingsScreen : GameScreen
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Button button;
        private HandCursor hand;
        private GraphicsDevice graphics;
        private int screenWidth, counter=0, screenHeight;
        private ContentManager content;
        private string message;
        private Vector2 avatarPosition;
        private Texture2D background, leftArrow, rightArrow;
        private Texture2D[] avatars;
        public Texture2D currentAvatar { get; set; }

        public override void Initialize()
        {
            showAvatar = true;
            button = new Button();
            hand = new HandCursor();
            hand.Initialize(ScreenManager.Kinect);
            button.Initialize("Buttons/OK", this.ScreenManager.Kinect, new Vector2(1050, 10));
            button.Clicked += new Button.ClickedEventHandler(button_Clicked);
            base.Initialize();
        }
        void button_Clicked(object sender, System.EventArgs a)
        {
            this.Remove();
            ScreenManager.AddScreen(new PlayScreen());
        }
        public override void LoadContent()
        {
            content = ScreenManager.Game.Content;
            graphics = ScreenManager.GraphicsDevice;
            spriteBatch = ScreenManager.SpriteBatch;
            screenHeight = graphics.Viewport.Height;
            screenWidth = graphics.Viewport.Width;
            background = content.Load<Texture2D>("Textures\\backgroundsettings");
            leftArrow = content.Load<Texture2D>("Textures\\leftArrow");
            rightArrow= content.Load<Texture2D>("Textures\\rightArrow");
            font = content.Load<SpriteFont>("SpriteFont1");
            hand.LoadContent(content);
            button.LoadContent(content);
            avatars = new Texture2D[4];
            avatars[0] = content.Load<Texture2D>("Textures\\avatar-dead");
            avatars[1] = content.Load<Texture2D>("Textures\\avatar-red");
            avatars[2] = content.Load<Texture2D>("Textures\\avatar-green");
            avatars[3] = content.Load<Texture2D>("Textures\\avatar-white");
            avatarPosition = new Vector2((screenWidth /1.7f), (screenHeight / 1.5f));
            currentAvatar = avatars[0];
            base.LoadContent();
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(leftArrow, new Vector2(screenWidth/2f -20, screenHeight/2f), Color.White);
            spriteBatch.Draw(rightArrow, new Vector2(screenWidth /2f -20 + currentAvatar.Width, screenHeight / 2f), Color.White);
            spriteBatch.Draw(currentAvatar, avatarPosition, null, Color.White, 0,
                    new Vector2(currentAvatar.Width, currentAvatar.Height), 1f, SpriteEffects.None, 0);
            hand.Draw(spriteBatch);
            button.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if((hand.position.X>(screenWidth/2 -200)) && (hand.position.X<(screenWidth/2 +200)))
            if (Constants.isSwappingHand)
            {
                counter++;
                if(counter== avatars.Length)
                counter=0;
                currentAvatar = avatars[counter];
                Constants.ResetFlags();
            }
            button.Update(gameTime);
            hand.Update(gameTime);
            base.Update(gameTime);
        }

    }

}