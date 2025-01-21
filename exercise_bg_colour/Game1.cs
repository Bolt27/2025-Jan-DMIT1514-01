using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace exercise_bg_colour;

public class BackgroundColourGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _arial;
    private string _message = "Hi, it's cold out.";

    private KeyboardState _kbPreviousState;
    private Color _bgColour = Color.CornflowerBlue;

    public BackgroundColourGame()
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

        #region Background Colour
        if(kbCurrentState.IsKeyDown(Keys.R)) // "Keys.R" represents the R key on the keyboard
        {
            _bgColour = Color.Red;
        }
        if(kbCurrentState.IsKeyDown(Keys.B))
        {
            _bgColour = Color.Blue;
        }
        if(kbCurrentState.IsKeyDown(Keys.G))
        {
            _bgColour = Color.Green;
        }
        #endregion

        #region "key down" event
        if (_kbPreviousState.IsKeyUp(Keys.Space) && kbCurrentState.IsKeyDown(Keys.Space))
        {
            _message += "------------------------------------------------------\n";
            _message += "------------------------------------------------------\n";
            _message += "------------------------------------------------------\n";
            _message += "------------------------------------------------------\n";
            _message += "------------------------------------------------------\n"; 
        }
        #endregion
        // "key hold" event
        else if (kbCurrentState.IsKeyDown(Keys.Space))
        {
            _message += "space";
        }
        #region "key up" event
        else if (kbCurrentState.IsKeyDown(Keys.Space))
        {
            //the space key is not being held down right now
            //but it was being held down on the last call to Update()
            //so, this is a "key up" event
            _message += "######################################################\n";
            _message += "######################################################\n";
            _message += "######################################################\n";
            _message += "######################################################\n";
            _message += "######################################################\n";
            _message += "######################################################\n";
        }
        #endregion

        // Remember the state of the keyboard for the next call to update
        _kbPreviousState = kbCurrentState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.DrawString(_arial, _message, Vector2.Zero, Color.BlueViolet);
        GraphicsDevice.Clear(_bgColour);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
