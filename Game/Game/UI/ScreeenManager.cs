﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Kinect;
using Game.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Game.UI
{
    public class ScreenManager : DrawableGameComponent
    {
        private List<GameScreen> screens = new List<GameScreen>();
        private List<GameScreen> screensToUpdate = new List<GameScreen>();
        private SpriteBatch spriteBatch;
        public Kinect.Kinect Kinect;

        /// <summary>
        /// Returns the sprite batch object.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        /// <summary>
        /// Creates a new instance of ScreenManager.
        /// </summary>
        public ScreenManager(Microsoft.Xna.Framework.Game game, Kinect.Kinect kinect)
            : base(game)
        {
            this.Kinect = kinect;
            if (Kinect.trackedSkeleton != null)
            {
                Constants.posY = Kinect.trackedSkeleton.Joints[JointType.HipCenter].Position.Y;
                Constants.posZ = Kinect.trackedSkeleton.Joints[JointType.ElbowRight].Position.Z;
                Constants.elbowPosY = Kinect.trackedSkeleton.Joints[JointType.ElbowRight].Position.Y;
                Constants.diffHandElbow = Kinect.trackedSkeleton.Joints[JointType.HandRight].Position.Z - Kinect.trackedSkeleton.Joints[JointType.ElbowRight].Position.Z;
                Constants.HipPosX = Kinect.trackedSkeleton.Joints[JointType.HipCenter].Position.X;
            }
        
            base.Initialize();

        }

        /// <summary>
        /// Loads the content of the screens managed by the screenManager.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (GameScreen screen in screens)
                screen.LoadContent();
        }

        /// <summary>
        /// Unloads the content of the screens managed by the screenManager.
        /// </summary>
        protected override void UnloadContent()
        {
            foreach (GameScreen screen in screens)
                screen.UnloadContent();
        }
        /// <summary>
        /// Updates the screens managed by the screenManager.
        /// </summary>
        /// <param name="gameTime">Represents the time of the game.</param>
        public override void Update(GameTime gameTime)
        {
            screensToUpdate.Clear();
            foreach (GameScreen screen in screens)
            {
                if (!screen.IsFrozen)
                    screensToUpdate.Add(screen);
            }

            if (screensToUpdate.Count == 0)
            {
                GameScreen screen = screens.FindLast(delegate(GameScreen g) { return g.IsFrozen; });
                screen.UnfreezeScreen();
                screen.screenPaused = false;
                screensToUpdate.Add(screen);
            }
            else
            {
                while (screensToUpdate.Count > 0)
                {
                    GameScreen screen = screensToUpdate[screensToUpdate.Count - 1];

                    screensToUpdate.RemoveAt(screensToUpdate.Count - 1);
                    screen.Update(gameTime);
                }
            }
        }
        /// <summary>
        /// Updates the screens managed by the screenManager.
        /// </summary>
        /// <param name="gameTime">Represents the time of the game.</param>
        public override void Draw(GameTime gameTime)
        {
            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(gameTime);
            }
        }
        /// <summary>
        /// Adds a screen to the list of screens managed by the screenManager.
        /// </summary>
        /// <param name="screen">Represents the screen that should be managed by the screenManager.</param>
        public void AddScreen(GameScreen screen)
        {
            screen.ScreenManager = this;
            screen.Initialize();
            screen.LoadContent();
            screens.Add(screen);
        }

        /// <summary>
        /// Removes a screen from the list of screens managed by the screenManager.
        /// </summary>
        /// <param name="screen">Represents the screen that should be removed from the list
        /// of screens managed by the screenManager.</param>
        public void RemoveScreen(GameScreen screen)
        {
            screen.UnloadContent();
            screens.Remove(screen);
            screensToUpdate.Remove(screen);
        }

    }
}