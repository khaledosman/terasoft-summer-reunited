﻿using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Game.Screens
{
    public class InstructionsScreen : GameScreen
    {
        private string Text;
        private Vector2 textPosition;
        private Rectangle textBox;
        private SpriteFont spriteFont;
        private Texture2D backgroundImage;
        Button OkButton;

        public InstructionsScreen()
        {
            textBox = new Rectangle();
            OkButton = new Button();
        }

        public void Initialize(string text)
        {
            Text = text;
            textPosition = new Vector2(130, 145);
            textBox = new Rectangle((int)textPosition.X, (int)textPosition.Y, 1020, 455);

            base.Initialize();
        }

        public void LoadContent()
        {
            ContentManager Content = ScreenManager.Game.Content;
            //spriteFont = Content.Load<SpriteFont>("instructionsFont");
            backgroundImage = Content.Load<Texture2D>("Textures/instructionsScreen");
            Text = WrapText(spriteFont, Text, textBox.Width);

            base.LoadContent();
        }

        public void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = this.ScreenManager.SpriteBatch;

            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.Transparent);
            //spriteBatch.DrawString(spriteFont, Text, textPosition, Color.White);

            base.Draw(gameTime);
        }

        public string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');

            StringBuilder sb = new StringBuilder();

            float lineWidth = 0f;

            float spaceWidth = spriteFont.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }
    }
}
