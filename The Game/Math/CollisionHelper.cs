using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

    public static bool RectangleOnRectangleCollision(Rectangle r1, Rectangle r2) {
        Vector2 r1Pos = r1.Position;
        Vector2 r2Pos = r2.Position;
        
        bool case1 = r1Pos.X + r1.Width < r2Pos.X;
        bool case2 = r1Pos.X > r2Pos.X + r2.Width;
        bool case3 = r1Pos.Y + r1.Height < r2Pos.Y;
        bool case4 = r1Pos.Y > r2Pos.Y + r2.Height;

        return !(case1 || case2 || case3 || case4);
    }

    public static bool PointOnCircleCollision(Vector2 p1, Ball b1) {
        Vector2 b1Pos = b1.Position;

        return Math.Pow(b1.Radius, 2f) >= Math.Pow(b1Pos.X - p1.X, 2f) + Math.Pow(b1Pos.Y - p1.Y, 2f);
    }

    public static bool PixelPerfectCollision<T1, T2>(T1 s1, T2 s2) where T1 : Entity where T2 : Entity {
        var transform1 = Matrix.CreateScale(scale1.X, scale1.Y, 1f) * Matrix.CreateRotationZ(rotation1);
        var transform2 = Matrix.CreateScale(scale2.X, scale2.Y, 1f) * Matrix.CreateRotationZ(rotation2);

        var inverse1 = Matrix.Invert(transform1);
        var inverse2 = Matrix.Invert(transform2);

        Vector2[] corners1 =
        {
            Vector2.Transform(new Vector2(-origin1.X, -origin1.Y), transform1) + position1,
            Vector2.Transform(new Vector2(texture1.Width - origin1.X, -origin1.Y), transform1) + position1,
            Vector2.Transform(new Vector2(-origin1.X, texture1.Height - origin1.Y), transform1) + position1,
            Vector2.Transform(new Vector2(texture1.Width - origin1.X, texture1.Height - origin1.Y), transform1) + position1
        };

        Vector2[] corners2 =
        {
            Vector2.Transform(new Vector2(-origin2.X, -origin2.Y), transform2) + position2,
            Vector2.Transform(new Vector2(texture2.Width - origin2.X, -origin2.Y), transform2) + position2,
            Vector2.Transform(new Vector2(-origin2.X, texture2.Height - origin2.Y), transform2) + position2,
            Vector2.Transform(new Vector2(texture2.Width - origin2.X, texture2.Height - origin2.Y), transform2) + position2
        };

        Vector2 min1 = Vector2.Min(Vector2.Min(corners1[0], corners1[1]),
                                Vector2.Min(corners1[2], corners1[3]));
        Vector2 max1 = Vector2.Max(Vector2.Max(corners1[0], corners1[1]),
                                Vector2.Max(corners1[2], corners1[3]));
        Vector2 min2 = Vector2.Min(Vector2.Min(corners2[0], corners2[1]),
                                Vector2.Min(corners2[2], corners2[3]));
        Vector2 max2 = Vector2.Max(Vector2.Max(corners2[0], corners2[1]),
                                Vector2.Max(corners2[2], corners2[3]));

        int left = (int)MathF.Floor(MathF.Max(min1.X, min2.X));
        int top = (int)MathF.Floor(MathF.Max(min1.Y, min2.Y));
        int right = (int)MathF.Ceiling(MathF.Min(max1.X, max2.X));
        int bottom = (int)MathF.Ceiling(MathF.Min(max1.Y, max2.Y));

        if (left >= right || top >= bottom)
            return false;

        var pixels1 = new Color[texture1.Width * texture1.Height];
        var pixels2 = new Color[texture2.Width * texture2.Height];
        texture1.GetData(pixels1);
        texture2.GetData(pixels2);

        for (int y = top; y < bottom; y++)
        {
            for (int x = left; x < right; x++)
            {
                Vector2 worldPoint = new Vector2(x + 0.5f, y + 0.5f);

                Vector2 point1 = Vector2.Transform(worldPoint - position1, inverse1) + origin1;
                Vector2 point2 = Vector2.Transform(worldPoint - position2, inverse2) + origin2;

                int x1 = (int)MathF.Floor(point1.X);
                int y1 = (int)MathF.Floor(point1.Y);
                int x2 = (int)MathF.Floor(point2.X);
                int y2 = (int)MathF.Floor(point2.Y);

                if (x1 >= 0 && x1 < texture1.Width && y1 >= 0 && y1 < texture1.Height &&
                    x2 >= 0 && x2 < texture2.Width && y2 >= 0 && y2 < texture2.Height)
                {
                    Color pixel1 = pixels1[y1 * texture1.Width + x1];
                    Color pixel2 = pixels2[y2 * texture2.Width + x2];

                    if (pixel1.A > 0 && pixel2.A > 0)
                        Console.WriteLine($"{r1.Position.X}, {r1.Position.Y} = {r1.PrevPosition.X}, {r1.PrevPosition.Y}");

                        r1.x = r1.PrevPosition.X;
                        r1.y = r1.PrevPosition.Y;

                        r2.x = r2.PrevPosition.X;
                        r2.y = r2.PrevPosition.Y;

                        return true;
                }
            }
        }

        return false;
    }

    // public static bool CircleOnRectangleCollision(Ball b1, Rectangle r1) {
    //     Vector2 b1Pos = b1.Position;
    //     Vector2 r1Pos = r1.Position;
        
    //     float closestX = Math.Clamp(b1Pos.X, r1Pos.X, r1Pos.X + r1.Width);
    //     float closestY = Math.Clamp(b1Pos.Y, r1Pos.Y, )
    // }
}