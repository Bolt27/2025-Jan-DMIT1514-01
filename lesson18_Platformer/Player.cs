using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson18_Platformer;

public class Player
{
    private enum State
    {
        Idle, Walking, Jumping
    }
    private State _state;
    private CelAnimationSequence _idleSequence;
    private CelAnimationSequence _jumpSequence;
    private CelAnimationSequence _walkSequence;
    private CelAnimationPlayer _animationPlayer;

    private Vector2 _position;
    private Vector2 _velocity;
    private Rectangle _gameBoundingBox;
    private Vector2 _dimensions;
    internal Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)_position.X, (int)_position.Y, (int)_dimensions.X, (int)_dimensions.Y);
        }
    }
    public Player(Vector2 position, Rectangle gameBoundingBox)
    {
        _position = position;
        _gameBoundingBox = gameBoundingBox;
        _animationPlayer = new CelAnimationPlayer();
    }
    internal void Initialize()
    {
        _state = State.Idle;
        _animationPlayer.Play(_idleSequence);
        _dimensions = new Vector2(30, 46);
    }
    internal void LoadContent(ContentManager Content)
    {
        _idleSequence = new CelAnimationSequence(Content.Load<Texture2D>("Idle"), 30, 1 / 8f);
        _walkSequence = new CelAnimationSequence(Content.Load<Texture2D>("Walk"), 35, 1 / 8f);
        _jumpSequence = new CelAnimationSequence(Content.Load<Texture2D>("JumpOne"), 30, 1 / 8f);
    }
    internal void Update(GameTime gameTime)
    {
        _animationPlayer.Update(gameTime);
        switch (_state)
        {
            case State.Jumping:
                break;
            case State.Idle:
                break;
            case State.Walking:
                break;
        }
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        switch (_state)
            {
                case State.Jumping:
                case State.Idle:
                case State.Walking:
                    _animationPlayer.Draw(spriteBatch, _position, SpriteEffects.None);
                    break;
            }
    }
}