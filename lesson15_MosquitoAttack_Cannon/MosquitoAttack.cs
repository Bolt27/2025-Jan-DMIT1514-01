using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson15_MosquitoAttack_Cannon;

public class MosquitoAttack : Game
{
    private const int _WindowWidth = 550, _WindowHeight = 400;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _background;
    private SpriteFont _arial;
    
    private Cannon _cannon;

    protected enum MosquitoAttackState
    {
        Playing, Paused, Over
    }
    protected MosquitoAttackState _gameState;

    protected KeyboardState _kbPreviousState;
    protected string _status = "";
    public MosquitoAttack()
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

        _cannon = new Cannon();

        base.Initialize();
        Rectangle gameBoundingBox = new Rectangle(0, 0, _WindowWidth, _WindowHeight);
        _cannon.Initialize(new Vector2(50, 325), gameBoundingBox);

        _gameState = MosquitoAttackState.Playing;

        _kbPreviousState = Keyboard.GetState();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _background = Content.Load<Texture2D>("Background");
        _arial = Content.Load<SpriteFont>("SystemArialFont");
        _cannon.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState kbState = Keyboard.GetState();

        switch(_gameState)
        {
            case MosquitoAttackState.Playing:
                if(kbState.IsKeyDown(Keys.Left))
                {
                    _cannon.Direction = new Vector2(-1, 0);
                }
                else if (kbState.IsKeyDown(Keys.Right))
                {
                    _cannon.Direction = new Vector2(1, 0);
                }
                else
                {
                    _cannon.Direction = new Vector2(0, 0);
                }
                _cannon.Update(gameTime);
                //is this a new key down event?
                if(kbState.IsKeyDown(Keys.P) && _kbPreviousState.IsKeyUp(Keys.P))
                {
                    _gameState = MosquitoAttackState.Paused;
                    _status = "Game paused press P to start playing again.";
                }
                break;
            case MosquitoAttackState.Paused:
                if(kbState.IsKeyDown(Keys.P) && _kbPreviousState.IsKeyUp(Keys.P))
                {
                    _gameState = MosquitoAttackState.Playing;
                    _status = "";
                }
                break;
            case MosquitoAttackState.Over:
                break;
        }
        _kbPreviousState = kbState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _cannon.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
