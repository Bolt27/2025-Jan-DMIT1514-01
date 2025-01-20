using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson05_keyboard_input;

public class KeyboardInputGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private SpriteFont _arial;
    private string _message = "Hi. It's cold out.";

    private KeyboardState _kbPreviousState;

    public KeyboardInputGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _arial = Content.Load<SpriteFont>("SystemArialFont");
    }

    protected override void Update(GameTime gameTime)
    {

        KeyboardState kbCurrentState = Keyboard.GetState();

        _message = "";
        if(kbCurrentState.IsKeyDown(Keys.Down))//"Keys.Down" represents the down arrow on the keyboard
        {
            _message += "Down";
        }
        if(kbCurrentState.IsKeyDown(Keys.Right))
        {
            _message += " Right";
        }

        //remember the state of the keyboard for the next call to update
        _kbPreviousState = kbCurrentState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.DrawString(_arial, _message, Vector2.Zero, Color.BlueViolet);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
