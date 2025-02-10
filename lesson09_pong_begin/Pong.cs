using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson09_pong_begin;

public class Pong : Game
{
    const float scale = 2f;
    private const int _WindowWidth = 250 * (int)scale, _WindowHeight = 150 * (int)scale, _BallWidthAndHeight = 7;
    private const int _PLayAreaEdgeLineWidth = 4;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _backgroundTexture, _ballTexture;

    private Rectangle _playAreaBoundingBox;
    //to keep track of the state of the ball
    private Vector2 _ballDimensions, _ballPosition, _ballDirection;
    private float _ballSpeed;

    public Pong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        _ballPosition.X = 50;
        _ballPosition.Y = 65;

        _ballSpeed = 30f;
        _ballDirection = new Vector2(-1, -1);

        _ballDimensions = new Vector2(_BallWidthAndHeight, _BallWidthAndHeight);
        _playAreaBoundingBox = new Rectangle(0, 0, _WindowWidth, _WindowHeight);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ballTexture = Content.Load<Texture2D>("Ball");
    }

    protected override void Update(GameTime gameTime)
    {
        // Move the ball according to its speed and direction
        _ballPosition += _ballDirection * _ballSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;

        if(_ballPosition.X <= _playAreaBoundingBox.Left || (_ballPosition.X + (_ballDimensions.X * scale)) >= _playAreaBoundingBox.Right)
        {
            _ballDirection.X *= -1;
        }

        if (_ballPosition.Y <= (_playAreaBoundingBox.Top + _PLayAreaEdgeLineWidth) 
        || (_ballPosition.Y + (_ballDimensions.Y * scale)) >= _playAreaBoundingBox.Bottom - _PLayAreaEdgeLineWidth)
        {
            _ballDirection.Y *= -1;
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_backgroundTexture, Vector2.Zero, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        _spriteBatch.Draw(_ballTexture, _ballPosition, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
