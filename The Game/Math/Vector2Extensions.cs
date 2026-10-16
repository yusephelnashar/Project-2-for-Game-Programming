using System;
using Microsoft.Xna.Framework;

public static class Vector2Extensions {
    public static float CrossProduct(Vector2 v1, Vector2 v2) {
        return v1.X * v2.Y - v1.Y * v2.X;
    }

    public static float Cross(this Vector2 a, Vector2 other) {
        return CrossProduct(a, other);
    }
}