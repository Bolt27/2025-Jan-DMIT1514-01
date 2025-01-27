using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson06_tictactoe01_mouse_input;

public class TicTacToe : Game
{
    private const int _WindowWidth = 500, _WindowHeight = 500; //eventually these will be 170px
    
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _backgroundImage, _xImage, _oImage;

    public TicTacToe()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        this.IsMouseVisible = false;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _backgroundImage = Content.Load<Texture2D>("TicTacToeBoard");
        _xImage = Content.Load<Texture2D>("X");
        _oImage = Content.Load<Texture2D>("O");
    }

    protected override void Update(GameTime gameTime)
    {

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_backgroundImage, Vector2.Zero, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
