using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FGame3D
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public Random rnd;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BasicEffect effect;
        Player player;
        Vector3 cameraUpVector = Vector3.UnitZ;
        GameMap map;

        public Game1()
        {
            rnd = new Random();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            player = new Player(new Vector3(4, 4, 20), 0, 0);
            effect = new BasicEffect(graphics.GraphicsDevice);
            textures.Add("dirt", Content.Load<Texture2D>("blocks/dirt"));
            textures.Add("sand", Content.Load<Texture2D>("blocks/sand"));
            map = new GameMap(this);

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected override void UnloadContent()
        {

        }
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            player.Update(Keyboard.GetState(), map);
            effect.View = Matrix.CreateLookAt(player.position * Block.size, (player.position + player.sight) * Block.size, cameraUpVector);
            float aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
            float fieldOfView = MathHelper.PiOver4;
            float nearClipPlane = 1;
            float farClipPlane = 300;
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
        }
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            map.Draw(effect, graphics, player);
            base.Draw(gameTime);
        }
    }
}
