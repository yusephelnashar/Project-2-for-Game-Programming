using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Rectangle : Entity {
    public static Texture2D RectangleTexture;
    public Vector2 Origin {
        get {
            if (RectangleTexture == null) return Vector2.Zero;
            return new Vector2(RectangleTexture.Width / 2f, RectangleTexture.Height / 2f);
        }
    }
    public float Rotation {
        get; private set;
    }
    public Rectangle(float x, float y, float rotation = 0.0f, float mass = 0.5f) : base(x, y) {
        this.Rotation = rotation;
        this.Mass = mass;
    }
    public int Width {
        get {
            try {
                return RectangleTexture.Width;
            } catch (Exception) {
                return 0;
            }
        }
    }
}