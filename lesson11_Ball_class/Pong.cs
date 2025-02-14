using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson11_Ball_class;

public class Pong : Game
{
    private const int _Scale = 2;
    private const int _WindowWidth = 250 * _Scale, _WindowHeight = 150 * _Scale;
    private const int _PaddleWidth = 2 * _Scale, _PaddleHeight = 18 * _Scale;
    private const int _PlayAreaEdgeLineWidth = 4 * _Scale;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _backgroundTexture;
    private Rectangle _playAreaBoundingBox;


    private Ball _ball;
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

        _ball = new Ball();
                        //position,         direction           scale       game bounding box
        _ball.Initialize(new Vector2(50, 65), new Vector2(-1, -1), _Scale, _playAreaBoundingBox);
        
        _paddlePosition = new Vector2(210 * _Scale, 75 * _Scale);
        _paddleSpeed = 100 * _Scale;
        _paddleDimensions = new Vector2(_PaddleWidth, _PaddleHeight);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _backgroundTexture = Content.Load<Texture2D>("Court");

        _ball.LoadContent(this.Content);


        _paddleTexture = Content.Load<Texture2D>("Paddle");
    }

    protected override void Update(GameTime gameTime)
    {
        _ball.Update(gameTime);
        
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
         
        _ball.Draw(_spriteBatch);
        
        _spriteBatch.Draw(_paddleTexture, _paddlePosition, null, Color.White, 0, Vector2.Zero, _Scale, SpriteEffects.None, 0);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
