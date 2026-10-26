using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using The_Game;

public class Ball : Entity {
    public static Texture2D BallTexture;
    public override Texture2D texture {
        get {
            return BallTexture;
        }
    }
    public override Vector2 Origin {
        get {
            if (BallTexture is null) return Vector2.Zero;
            return new Vector2(BallTexture.Width / 2f, BallTexture.Height / 2f);
        }
    }
    public override Vector2 Scale {
        get; protected set;
    }
    private float _rotationRad;
    public override float Rotation {
        get {
            return _rotationRad * (180f / MathF.PI);
        } set {
            _rotationRad = value * (MathF.PI / 180f);
        }
    }
    public float Radius {
        get {
            if (BallTexture is null) return 0f;

            int TexWidth = BallTexture.Width;
            int TexHeight = BallTexture.Height;

            try {
                if (TexWidth != TexHeight) {
                    throw new Exception("Image isn't equilateral.");
                }
                
                return TexWidth / 2f * Scale.X;
            } catch (Exception) {
                return 0f;
            }
        }
    }
    private Vector2 _velocity;
    public Vector2 Velocity {
        get {
            return _velocity;
        } set {
            if (!(value is Vector2 v2)) return;
            _velocity = value;
        }
    }
    private Vector2 _acceleration;
    public Ball(float x, float y, float rotation = 0.0f, float mass = 0.5f) : base(x, y) {
        PrevPosition = Position;
        Rotation = rotation;

        _velocity = Vector2.Zero;
        _acceleration = Vector2.Zero;
        Scale = Vector2.Multiply(Vector2.One, 0.5f);
        Mass = mass;
    }

    public Ball(float rotation = 0.0f, float mass = 0.5f) : base(DEFAULT_X, DEFAULT_Y) {
        Rotation = rotation;

        _velocity = Vector2.Zero;
        _acceleration = Vector2.Zero;
        Scale = Vector2.Multiply(Vector2.One, 0.25f);
        Mass = mass;
    }

    public override void Update(GameTime gameTime) {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
        
        _acceleration = Vector2.Zero;

        Vector2 gravityForce = new Vector2(0.0f, Game1.GRAVITY * Mass);
        Vector2 dragForce = -Game1.DRAG * _velocity;

        Vector2 netForce = gravityForce + dragForce;
        _acceleration += netForce / Mass;

        _velocity += _acceleration * dt;

        x += _velocity.X;
        y += _velocity.Y;

        // Angular velocity implementation (JUST HAS TO ADD ROTATION TO COUNT FOR GRADE NOT EVEN VELOCITY)
        // IMPLEMENT NEBULAE FOR THE PARALLAX ITS COOL
    }

    public override void Draw(SpriteBatch spriteBatch) {
        if (BallTexture is null) return;
        spriteBatch.Draw(BallTexture, Position, null, Color.White, _rotationRad, Origin, Scale, SpriteEffects.None, 0f);
    }

    public static void ResolveElasticCollision(Ball b1, Ball b2) {
        Vector2 collisionLine = b1.Position - b2.Position;
        collisionLine.Normalize();

        float angle = MathF.Acos(Vector2.Dot(collisionLine, Vector2.UnitX));

        Vector2 u0 = Vector2.Transform(b1._velocity, Matrix.CreateRotationZ(angle));
        Vector2 u1 = Vector2.Transform(b2._velocity, Matrix.CreateRotationZ(angle));

        float m0 = b1.Mass;
        float m1 = b2.Mass;

        Vector2 v0;
        v0.X = ((m0 - m1) / (m0 + m1)) * u0.X + ((2 * m1) / (m0 + m1)) * u1.X;

        Vector2 v1;
        v1.X = ((2 * m0) / (m0 + m1)) * u0.X + ((m1 - m0) / (m0 + m1)) * u1.X;

        v0.Y = u0.Y;
        v1.Y = u1.Y;

        b1._velocity = Vector2.Transform(v0, Matrix.CreateRotationZ(-angle));
        b2._velocity = Vector2.Transform(v1, Matrix.CreateRotationZ(-angle));
    }
}