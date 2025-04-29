using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson18_Platformer;

public class Player
{
    private const int _Speed = 150;
    private const int _JumpForce = -100;
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
    public Vector2 Velocity { get => _velocity; }
    private Rectangle _gameBoundingBox;
    private Vector2 _dimensions;
    internal Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)_position.X, (int)_position.Y, (int)_dimensions.X, (int)_dimensions.Y);
        }
    }

    

    private bool _facingRight;
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
        _dimensions = new Vector2(33, 34);
        _facingRight = true;
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

        _velocity.Y += Platformer._Gravity * (float) gameTime.ElapsedGameTime.TotalSeconds;
        _position += _velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
        
        //are we moving up or down faster than gravity?
        if(Math.Abs(_velocity.Y) > Platformer._Gravity * (float) gameTime.ElapsedGameTime.TotalSeconds)
        {
            _state = State.Jumping;
            _animationPlayer.Play(_jumpSequence);
        }
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
                    SpriteEffects effects = SpriteEffects.None;
                    if(!_facingRight)
                    {
                        effects = SpriteEffects.FlipHorizontally;
                    }
                    _animationPlayer.Draw(spriteBatch, _position, effects);
                    break;
            }
    }
    internal void MoveHorizontally(float direction)
    {
        _velocity.X = direction * _Speed;
        if(direction > 0)
        {
            _facingRight = true;
        }
        else if (direction < 0)
        {
            _facingRight = false;
        }

        if(_state != State.Jumping)
        {
            _animationPlayer.Play(_walkSequence);
            _state = State.Walking;
        }
    }
    internal void MoveVertically(float direction)
    {
        _velocity.Y = direction * _Speed;
    }
    internal void Stop()
    {
        _velocity.X = 0;
        if(_state == State.Walking)
        {
            _state = State.Idle;
            _animationPlayer.Play(_idleSequence);
        }
    }
    internal void Land(Rectangle whatILandedOn)
    {
        if(_state == State.Jumping)
        {   
            //add an extra pixel to make up for what StandOn is about to do
            _position.Y = whatILandedOn.Top - _dimensions.Y + 1;
            _velocity.Y = 0;
            _state = State.Walking;
        }
    }
    internal void StandOn(GameTime gameTime)
    {
        _velocity.Y -= Platformer._Gravity * (float) gameTime.ElapsedGameTime.TotalSeconds;
    }
    internal void Jump()
    {
        if(_state != State.Jumping)
        {
            _velocity.Y = _JumpForce;
        }
    }
}