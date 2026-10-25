using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Entity {
    protected const float DEFAULT_X = 0.0f;
    protected const float DEFAULT_Y = 0.0f;
    public Vector2 PrevPosition {
        get; protected set;
    }
    private float _x;
    public float x {
        get {
            return _x;
        } set {
            var tempPosition = PrevPosition;

            tempPosition.X = _x;
            PrevPosition = tempPosition;

            _x = value;
        }
    }
    private float _y;
    public float y {
        get {
            return _y;
        } set {
            var tempPosition = PrevPosition;

            tempPosition.Y = _y;
            PrevPosition = tempPosition;
            
            _y = value;
        }
    }
    public float Mass {
        get; protected set;
    }
    public Vector2 Position { 
        get {
            return new Vector2(x, y);
        }
    }

    public Entity(float x, float y) {
        this.x = x;
        this.y = y;
    }

    public Entity() {
        x = DEFAULT_X;
        y = DEFAULT_Y;
    }

    public virtual void Update(GameTime gameTime) {
        
    }

    public virtual void Draw(SpriteBatch spriteBatch) {
        
    }
}