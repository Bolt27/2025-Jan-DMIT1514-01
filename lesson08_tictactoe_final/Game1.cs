using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson08_tictactoe_final;

public class TicTacToe : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _gameBoardImage, _xImage, _oImage;
    private MouseState _currentMouseState, _previousMouseState;
    private const int _WindowWidth = 170, _WindowHeight = 170;

    public enum GameSpaceState
    {
        X, O, Empty
    }
    private GameSpaceState _nextTokenToBePlayed = GameSpaceState.X;
    public TicTacToe()
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

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _gameBoardImage = Content.Load<Texture2D>("TicTacToeBoard");
        _xImage = Content.Load<Texture2D>("X");
        _oImage = Content.Load<Texture2D>("O");
    }

    protected override void Update(GameTime gameTime)
    {
        _currentMouseState = Mouse.GetState();
        if(_previousMouseState.LeftButton == ButtonState.Pressed
            && _currentMouseState.LeftButton == ButtonState.Released)
        {
            if(_nextTokenToBePlayed == GameSpaceState.X)
            {
                _nextTokenToBePlayed = GameSpaceState.O;
            }
            else
            {
                _nextTokenToBePlayed = GameSpaceState.X;
            }
        }
        _previousMouseState = _currentMouseState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_gameBoardImage, Vector2.Zero, Color.White);
        Vector2 adjustedMousePosition =
                    _currentMouseState.Position.ToVector2() - _xImage.Bounds.Center.ToVector2();
        if(_nextTokenToBePlayed == GameSpaceState.X)
        {
            _spriteBatch.Draw(_xImage, adjustedMousePosition, Color.White);
        }
        else
        {
            _spriteBatch.Draw(_oImage, adjustedMousePosition, Color.White);
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
