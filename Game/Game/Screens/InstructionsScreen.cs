﻿using System.Text;
using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Screens
{
    public class InstructionsScreen : GameScreen
    {
        private string Text, TextToDraw;
        private Vector2 textPosition;
        private Rectangle textBox;
        private SpriteFont spriteFont;
        private Texture2D backgroundImage;
        private Button OkButton;
        private HandCursor Hand;
        public bool WrapTexter { get; set; }
        public InstructionsScreen()
        {
            textBox = new Rectangle();
            OkButton = new Button();
            Hand = new HandCursor();
            WrapTexter = false;
        }

        public override void Initialize()
        {
            textPosition = new Vector2(75, 145);
            textBox = new Rectangle((int)textPosition.X, (int)textPosition.Y, 1020, 455);
            OkButton.Initialize("Buttons/OK", this.ScreenManager.Kinect, new Vector2(1050, 10));
            OkButton.Clicked += new Button.ClickedEventHandler(OkButton_Clicked);

            Hand.Initialize(ScreenManager.Kinect);
            base.Initialize();
        }

        void OkButton_Clicked(object sender, System.EventArgs a)
        {
            this.Remove();
            ScreenManager.AddScreen(new MainScreen());
        }

        public void SetText(string text)
        {
            Text = text;
        }

        public override void LoadContent()
        {
            ContentManager Content = ScreenManager.Game.Content;
            spriteFont = Content.Load<SpriteFont>("Fontopo");
            backgroundImage = Content.Load<Texture2D>("Textures/instructionsScreen");
            spriteFont.LineSpacing = 21;
            if(WrapTexter)
                TextToDraw = WrapText(spriteFont, Text, 1130);
            else
            TextToDraw = Text;
            OkButton.LoadContent(Content);
            Hand.LoadContent(Content);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            OkButton.Update(gameTime);
            Hand.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = this.ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);
            spriteBatch.DrawString(spriteFont, TextToDraw, textPosition, Color.White);
            OkButton.Draw(spriteBatch);
            Hand.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        
        public string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');

            StringBuilder builder = new StringBuilder();

            float lineWidth = 0f;

            float spaceWidth = spriteFont.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    builder.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    builder.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return builder.ToString();
        }
    }
}
