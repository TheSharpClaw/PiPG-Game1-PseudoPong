using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 PaddlePosition;
        Vector2[] WallPosition = new Vector2[3];
        Texture2D paddle;
        Texture2D[] wall = new Texture2D[3];

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        /// 
        protected void SetPaddle()
        {
            PaddlePosition = new Vector2(this.GraphicsDevice.Viewport.Width / 4, this.GraphicsDevice.Viewport.Height * 0.8f);
            paddle = new Texture2D(this.GraphicsDevice, 100, 10);
            Color[] color = new Color[100 * 10];
            for (int i = 0; i < color.Length - 1; i++)
            {
                color[i] = Color.Red;
            }
            paddle.SetData<Color>(color);
        }
        protected void DrawWall(int tab_index,float pos_x, float pos_y, int tex_width, int tex_height)
        {
            WallPosition[tab_index] = new Vector2(pos_x, pos_y);
            wall[tab_index] = new Texture2D(GraphicsDevice, tex_width, tex_height);
            Color[] color = new Color[tex_width * tex_height];
            for (int i = 0; i < color.Length - 1; i++) color[i] = Color.Black;
            wall[tab_index].SetData<Color>(color);
        }
        protected void DrawWall(int tab_index, float pos_x, float pos_y, int tex_width, int tex_height,Color tex_color)
        {
            WallPosition[tab_index] = new Vector2(pos_x, pos_y);
            wall[tab_index] = new Texture2D(GraphicsDevice, tex_width, tex_height);
            Color[] color = new Color[tex_width * tex_height];
            for (int i = 0; i < color.Length - 1; i++) color[i] = tex_color;
            wall[tab_index].SetData<Color>(color);
        }
        protected void SetWalls()
        {
            DrawWall(0, 100, 100, 35, 150);
            DrawWall(1, 135, 100, 550, 35);
            DrawWall(2, 650, 100, 35, 150);
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            SetPaddle();
            SetWalls();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //if(GamePad.GetState(PlayerIndex.One).Buttons.LeftStick == ButtonState.Pressed)
            if(Keyboard.GetState().IsKeyDown(Keys.Left) && (PaddlePosition.X) != GraphicsDevice.Viewport.X) PaddlePosition.X -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && (PaddlePosition.X + paddle.Width) != GraphicsDevice.Viewport.Width) PaddlePosition.X += 5;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(paddle, PaddlePosition);
            for (int i = 0; i < 3; i++) spriteBatch.Draw(wall[i], WallPosition[i]);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
