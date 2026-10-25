using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using The_Game;

public class Rectangle : Entity {
    public static Texture2D RectangleTexture;
    public Vector2 Origin {
        get {
            if (RectangleTexture == null) return Vector2.Zero;
            return new Vector2(RectangleTexture.Width / 2f, RectangleTexture.Height / 2f);
        }
    }
    private float _rotationRad;
    public float Rotation {
        get {
            return _rotationRad * (180f / MathF.PI);
        } set {
            _rotationRad = value * (MathF.PI / 180f);
        }
    }
    public float Width {
        get {
            try {
                return RectangleTexture.Width * Scale.X;
            } catch (Exception) {
                return 0;
            }
        }
    }

    public float Height {
        get {
            try {
                return RectangleTexture.Height * Scale.Y;
            } catch (Exception) {
                return 0;
            }
        }
    }

    public Vector2 _velocity;
    public Vector2 Velocity {
        get {
            return _velocity;
        } set {
            if (!(value is Vector2 v2)) return;
            _velocity = value;
        }
    }
    public Vector2 Scale {
        get; private set;
    }
    private Vector2 _acceleration;
    public Rectangle(float x, float y, float rotation = 0.0f, float mass = 0.5f) : base(x, y) {
        Rotation = rotation;

        _velocity = Vector2.Zero;
        _acceleration = Vector2.Zero;
        Scale = Vector2.Multiply(Vector2.One, 0.5f);
        this.Mass = mass;
    }

    public Rectangle(float rotation = 0.0f, float mass = 0.5f) : base(DEFAULT_X, DEFAULT_Y) {
        Rotation = rotation;

        _velocity = Vector2.Zero;
        _acceleration = Vector2.Zero;
        Scale = Vector2.Multiply(Vector2.One, 0.5f);
        this.Mass = mass;
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
    }

    public override void Draw(SpriteBatch spriteBatch) {
        if (RectangleTexture is null) return;
        spriteBatch.Draw(RectangleTexture, Position, null, Color.White, _rotationRad, Origin, Scale, SpriteEffects.None, 0f);
    }

    public static void ResolveElasticCollision(Rectangle r1, Rectangle r2) {
        Vector2 collisionLine = r1.Position - r2.Position;
        collisionLine.Normalize();

        float angle = MathF.Acos(Vector2.Dot(collisionLine, Vector2.UnitX));

        Vector2 u0 = Vector2.Transform(r1._velocity, Matrix.CreateRotationZ(angle));
        Vector2 u1 = Vector2.Transform(r2._velocity, Matrix.CreateRotationZ(angle));

        float m0 = r1.Mass;
        float m1 = r2.Mass;

        Vector2 v0;
        v0.X = ((m0 - m1) / (m0 + m1)) * u0.X + ((2 * m1) / (m0 + m1)) * u1.X;

        Vector2 v1;
        v1.X = ((2 * m0) / (m0 + m1)) * u0.X + ((m1 - m0) / (m0 + m1)) * u1.X;

        v0.Y = u0.Y;
        v1.Y = u1.Y;

        r1._velocity = Vector2.Transform(v0, Matrix.CreateRotationZ(-angle));
        r2._velocity = Vector2.Transform(v1, Matrix.CreateRotationZ(-angle));
    }
}