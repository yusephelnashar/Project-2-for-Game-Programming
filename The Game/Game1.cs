using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace The_Game;

public class Game1 : Game
{
    public const float DRAG = 1.2f;
    public const float GRAVITY = 9.81f;
    private const int WIN_WIDTH = 800;
    private const int WIN_HEIGHT = 800;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<Ball> _balls = new List<Ball>();
    private List<Rectangle> _rectangles = new List<Rectangle>();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = WIN_WIDTH;
        _graphics.PreferredBackBufferHeight = WIN_HEIGHT;
        
        TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0);

        // _balls.Add(new Ball(x: 100, y: WIN_HEIGHT / 5, mass: 10f));
        // _balls.Add(new Ball(x: 700, y: WIN_HEIGHT / 5, mass: 5f));

        // _balls[0].Velocity = new Vector2(10f, 0f);
        // _balls[1].Velocity = new Vector2(-10f, 0f);

        _rectangles.Add(new Rectangle(100, WIN_HEIGHT / 5, mass: 10f));
        _rectangles.Add(new Rectangle(700, WIN_HEIGHT / 5, mass: 5f));

        _rectangles[0].Velocity = new Vector2(10f, 0f);
        _rectangles[0].Rotation = 45f;
        _rectangles[1].Velocity = new Vector2(-10f, 0f);
        _rectangles[1].Rotation = 90f;
        // _rectangles[1].Rotation = 45f;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        Ball.BallTexture = Content.Load<Texture2D>("Circle");
        Rectangle.RectangleTexture = Content.Load<Texture2D>("Rectangle");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        // foreach (Ball ball in _balls) {
        //     ball.Update(gameTime);
        // }

        // for (int i = 0; i < _balls.Count; i++) {
        //     for (int j = i + 1; j < _balls.Count; j++) {
        //         if (CollisionHelper.CircleOnCircleCollision(_balls[i], _balls[j])) {
        //             Console.WriteLine("COLLIDING");
        //             Ball.ResolveElasticCollision(_balls[i], _balls[j]);
        //         }
        //     }
        // }

        foreach (Rectangle rectangle in _rectangles) {
            rectangle.Update(gameTime);
        }

        for (int i = 0; i < _rectangles.Count; i++) {
            for (int j = i + 1; j < _rectangles.Count; j++) {
                if (CollisionHelper.PixelPerfectCollision(Rectangle.RectangleTexture, _rectangles[i].Position, _rectangles[i].Origin, _rectangles[i].Rotation,
                _rectangles[i].Scale, Rectangle.RectangleTexture, _rectangles[j].Position, _rectangles[j].Origin, _rectangles[j].Rotation, _rectangles[j].Scale, 
                _rectangles[i], _rectangles[j])) {
                    Console.WriteLine("COLLIDING");
                    Rectangle.ResolveElasticCollision(_rectangles[i], _rectangles[j]);
                }

                // if (CollisionHelper.RectangleOnRectangleCollision(_rectangles[i], _rectangles[j])) {
                //     Console.WriteLine("COLLIDING");
                //     Rectangle.ResolveElasticCollision(_rectangles[i], _rectangles[j]);
                // }
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();

        foreach (Rectangle rectangle in _rectangles) {
            rectangle.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
