using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson10_scale_and_paddle;

public class Pong : Game
{
    private const int _Scale = 2;

    private const int _WindowWidth = 250 * _Scale, _WindowHeight = 150 * _Scale;
    private const int _BallWidthAndHeight = 7 * _Scale;

    private const int _PaddleWidth = 2 * _Scale, _PaddleHeight = 18 * _Scale;
    private const int _PlayAreaEdgeLineWidth = 4 * _Scale;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _backgroundTexture;

    private Rectangle _playAreaBoundingBox;
    //to keep track of the state of the ball
    private Texture2D _ballTexture;
    private Vector2 _ballDimensions, _ballPosition, _ballDirection;
    private float _ballSpeed;

    private Texture2D _paddleTexture;
    private Vector2 _paddleDimensions, _paddlePosition, _paddleDirection;
    private float _paddleSpeed;

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

        _playAreaBoundingBox = new Rectangle(0, 0, _WindowWidth, _WindowHeight);

        _ballPosition = new Vector2(50 * _Scale, 65 * _Scale);
        _ballSpeed = 100 * _Scale;
        _ballDirection = new Vector2(-1, -1);
        _ballDimensions = new Vector2(_BallWidthAndHeight, _BallWidthAndHeight);
        
        _paddlePosition = new Vector2(210 * _Scale, 75 * _Scale);
        _paddleSpeed = 100 * _Scale;
        _paddleDimensions = new Vector2(_PaddleWidth, _PaddleHeight);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ballTexture = Content.Load<Texture2D>("Ball");
        _paddleTexture = Content.Load<Texture2D>("Paddle");
    }

    protected override void Update(GameTime gameTime)
    {
        #region ball movement
        _ballPosition += _ballDirection * _ballSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
        //bounce ball off left and right sides
        if(_ballPosition.X <= _playAreaBoundingBox.Left || (_ballPosition.X + _ballDimensions.X) >= _playAreaBoundingBox.Right)
        {
            _ballDirection.X *= -1;
        }
        //bounce ball off top and bottom
        if  (
                _ballPosition.Y <= (_playAreaBoundingBox.Top + _PlayAreaEdgeLineWidth) 
                || (_ballPosition.Y + _ballDimensions.Y) >= (_playAreaBoundingBox.Bottom - _PlayAreaEdgeLineWidth)
            )
        {
            _ballDirection.Y *= -1;
        }
        #endregion
        #region paddle movement

        KeyboardState kbState = Keyboard.GetState();
        if(kbState.IsKeyDown(Keys.W))
        {
            _paddleDirection = new Vector2(0, -1);
        }
        else if(kbState.IsKeyDown(Keys.S))
        {
            _paddleDirection = new Vector2(0, 1);
        }
        else
        {
            _paddleDirection = new Vector2(0, 0);
        }
        _paddlePosition += _paddleDirection * _paddleSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;

        if(_paddlePosition.Y <= _playAreaBoundingBox.Top)
        {
            _paddlePosition.Y = _playAreaBoundingBox.Top;
        }
        else if((_paddlePosition.Y + _paddleDimensions.Y) >= _playAreaBoundingBox.Bottom)
        {
            _paddlePosition.Y = _playAreaBoundingBox.Bottom - _paddleDimensions.Y;
        }


        //1. Make the paddle move up and down in response to keyboard input.
        //2. Make the paddle stop at the top and bottom of the game play area.

        #endregion
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_backgroundTexture, Vector2.Zero, null, Color.White, 0, Vector2.Zero, _Scale, SpriteEffects.None, 0);
        //params:       texture to draw, position, sourceRectangle, color, rotation, origin, SCALE, SpriteEffects, layer depth
        _spriteBatch.Draw(_ballTexture, _ballPosition, null, Color.White, 0, Vector2.Zero, _Scale, SpriteEffects.None, 0);

        _spriteBatch.Draw(_paddleTexture, _paddlePosition, null, Color.White, 0, Vector2.Zero, _Scale, SpriteEffects.None, 0);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
