using System;
using Microsoft.Xna.Framework;

public static class CollisionHelper {
    public static bool PointCollision(Vector2 p1, Vector2 p2) {
        return p1.X == p2.X && p1.Y == p2.Y;
    }

    public static bool CircleOnCircleCollision(Ball b1, Ball b2) {
        float sumRadius = b1.Radius + b2.Radius;
        float radiusSquared = MathF.Pow(sumRadius, 2f);

        Vector2 b1Pos = b1.Position;
        Vector2 b2Pos = b2.Position;
        
        return Math.Pow(b2Pos.X - b1Pos.X, 2) + Math.Pow(b2Pos.Y - b1Pos.Y, 2) <= radiusSquared;
    }

    // public static bool RectangleOnRectangleCollision(Rectangle r1, Rectangle r2) {
    //     Vector2 r1Pos = r1.Position;
    //     Vector2 r2Pos = r2.Position;
        
    //     bool case1 = r1Pos.X + r1.Width < r2Pos.X;
    //     bool case2 = r1Pos.X > r2.X + r2.Width;
    //     bool case3 = r1Pos.Y + r1.Height < r2.Y;
    //     bool case4 = r1Pos.Y > r2.Y + r2.Height;

    //     return !(case1 || case2 || case3 || case4);
    // }

    public static bool PointOnCircleCollision(Vector2 p1, Ball b1) {
        Vector2 b1Pos = b1.Position;

        return Math.Pow(b1.Radius, 2f) >= Math.Pow(b1Pos.X - p1.X, 2f) + Math.Pow(b1Pos.Y - p1.Y, 2f);
    }

    // public static bool PointOnRectangleCollision(Vector2 p1, Rectangle r1) {
    //     r1Pos = r1.Position;

    //     return p1.X >= r1Pos.X && p1.X <= r1Pos.X + r1.Width && p1.Y >= r1Pos.Y && p1.Y <= r1Pos.Y + r1.Height;
    // }

    // public static bool CircleOnRectangleCollision(Ball b1, Rectangle r1) {
    //     Vector2 b1Pos = b1.Position;
    //     Vector2 r1Pos = r1.Position;


    // }

    // public static bool PerPixelCollision(object s1, object s2) {
    //     switch (s1, s2) {
    //         case (Ball b1, Ball b2):
    //             Texture2D tex = Ball.BallTexture;

    //             Color[] texData = new Color[tex.Width * tex.Height];
    //             tex.GetData(texData);

    //             foreach (Color color in texData) {
    //                 int alpha = color.A;
                    
    //             }
    //     }
    // }
}