using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Drawing;

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
        Texture2D paddle;
        Vector2[] WallPosition = new Vector2[3];
        Texture2D[] wall = new Texture2D[3];
        Vector2 BallPosition;
        Texture2D ball;

        SpriteFont font;

        int score = 0;
        int xCoord = 2;
        int yCoord = 2;

        bool directionX = true;//true-> prawo...false->lewo
        bool directionY = true;//true->gora...false->dol
        bool gameStatus = true;//true->gra aktywna....false->przerwanie gry
        


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
            PaddlePosition = new Vector2(this.GraphicsDevice.Viewport.Width / 2 - 80, this.GraphicsDevice.Viewport.Height * 0.835f);
            
            paddle = new Texture2D(this.GraphicsDevice, 160, 40);
            Color[] color = new Color[160 * 40];
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

        protected void DrawWall(int tab_index, float pos_x, float pos_y, int tex_width, int tex_height, Color tex_color)
        {
            WallPosition[tab_index] = new Vector2(pos_x, pos_y);
            wall[tab_index] = new Texture2D(GraphicsDevice, tex_width, tex_height);
            Color[] color = new Color[tex_width * tex_height];
            for (int i = 0; i < color.Length - 1; i++) color[i] = tex_color;
            wall[tab_index].SetData<Color>(color);
        }
        protected void DrawBall()
        {
            /*(PaddlePosition.X + PaddlePosition.X / 2), (PaddlePosition.Y + paddle.Height)*/
            //BallPosition = new Vector2(/*GraphicsDevice.Viewport.Width/3,GraphicsDevice.Viewport.Height/3*/40,100);
            BallPosition = new Vector2(350, 100);
            ball = new Texture2D(GraphicsDevice, 20, 20);
            Color[] color = new Color[20 * 20];
            for (int i = 0; i < color.Length - 1; i++) color[i] = Color.Orange;
            ball.SetData<Color>(color);
        }
        protected void BallControll()
        {
            if (gameStatus)
            {
                BallPosition.X += 2;
                BallPosition.Y += 2;
            }
        }
        protected void CollisionDetector(int index)
        {
            //wewnetrzna strona

            if (BallPosition.X <= WallPosition[0].X + wall[0].Width &&
               BallPosition.Y <= WallPosition[0].Y + wall[0].Height &&
               BallPosition.X >= WallPosition[0].X)
            {
                score++;
            }

            //zewnetrzna stona

            if (BallPosition.Y <= WallPosition[0].Y + wall[0].Height &&
                BallPosition.X + ball.Width >= WallPosition[0].X &&
                BallPosition.Y + ball.Height >= WallPosition[1].Y &&
                BallPosition.X <= WallPosition[2].X + wall[2].Width &&
                BallPosition.Y <= WallPosition[2].Y + wall[2].Height) ;

        }
        protected void SetWalls()
        {
            DrawWall(0, 120, 40, 40, 160);
            DrawWall(1, 120, 40, 560, 40);
            DrawWall(2, 640, 40, 40, 160);
        }
        protected void Collision(int index)
        {
            if(BallPosition.X + ball.Width >= WallPosition[index].X && BallPosition.X <= WallPosition[index].X + wall[index].Width &&
               BallPosition.Y <= WallPosition[index].Y + wall[index].Height && BallPosition.Y + ball.Height >= WallPosition[index].Y)
            {
                if (index == 0 | index == 2)
                {
                    directionX = !directionX;
                    score++;
                }
                else if (index == 1)
                {
                    directionY = !directionY;
                    score++;
                }
            }
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            DrawBall();
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
            font = Content.Load<SpriteFont>("Score");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && (PaddlePosition.X) != GraphicsDevice.Viewport.X) PaddlePosition.X -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && (PaddlePosition.X + paddle.Width) != GraphicsDevice.Viewport.Width) PaddlePosition.X += 5;

            if(gameStatus)
            {
                if (directionX)
                {
                    BallPosition.X += xCoord;
                }
                else
                {
                    BallPosition.X -= xCoord;
                }
                if(directionY)
                {
                    BallPosition.Y += yCoord;
                }
                else
                {
                    BallPosition.Y -= yCoord;
                }
            }

            if (BallPosition.X <= GraphicsDevice.Viewport.X) //lewa sciana
            {
                directionX = !directionX;
                score -= 2;
            }
            if (BallPosition.Y <= GraphicsDevice.Viewport.Y) //gorna
            {
                directionY = !directionY;
                score -= 2;
            }
            if (BallPosition.X + ball.Width >= GraphicsDevice.Viewport.Width) //prawa
            {
                directionX = !directionX;
                score -= 2;
            }
            if (BallPosition.Y + ball.Height >= GraphicsDevice.Viewport.Height) //dol
            {
                directionY = !directionY;
                score -= 2;
            }
            if (BallPosition.X >= PaddlePosition.X - ball.Width / 2 &&
               BallPosition.X <= PaddlePosition.X + paddle.Width &&
               BallPosition.Y + ball.Height >= PaddlePosition.Y)
            {
                if (yCoord > 0) directionY = !directionY;
            }
            Collision(0);Collision(1);Collision(2);
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
            spriteBatch.Draw(ball, BallPosition);
            spriteBatch.DrawString(font, "Score: "+score.ToString(), new Vector2(25, 50), Color.Black);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
