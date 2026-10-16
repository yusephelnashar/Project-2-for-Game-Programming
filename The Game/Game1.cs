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

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = WIN_WIDTH;
        _graphics.PreferredBackBufferHeight = WIN_HEIGHT;
        
        TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0);

        _balls.Add(new Ball(x: 100, y: WIN_HEIGHT / 5, mass: 10f));
        _balls.Add(new Ball(x: 700, y: WIN_HEIGHT / 5, mass: 5f));

        _balls[0].Velocity = new Vector2(10f, 0f);
        _balls[1].Velocity = new Vector2(-10f, 0f);

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
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        foreach (Ball ball in _balls) {
            ball.Update(gameTime);
        }

        for (int i = 0; i < _balls.Count; i++) {
            for (int j = i + 1; j < _balls.Count; j++) {
                if (CollisionHelper.CircleOnCircleCollision(_balls[i], _balls[j])) {
                    Console.WriteLine("COLLIDING");
                    Ball.ResolveElasticCollision(_balls[i], _balls[j]);
                }
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();

        foreach (Ball ball in _balls) {
            ball.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
