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

        #region arrow keys
        if(kbCurrentState.IsKeyDown(Keys.Down))//"Keys.Down" represents the down arrow on the keyboard
        {
            _message += "Down";
        }
        if(kbCurrentState.IsKeyDown(Keys.Up))
        {
            _message += "Up ";
        }
        if(kbCurrentState.IsKeyDown(Keys.Left))
        {
            _message += "Left ";
        }
        if(kbCurrentState.IsKeyDown(Keys.Right))
        {
            _message += " Right";
        }
        #endregion
        
        #region "key down" event
        if(_kbPreviousState.IsKeyUp(Keys.Space) && kbCurrentState.IsKeyDown(Keys.Space))
        {
            _message += "------------------------------------------------------\n";
            _message += "------------------------------------------------------\n";
            _message += "------------------------------------------------------\n";
            _message += "------------------------------------------------------\n";
            _message += "------------------------------------------------------\n";            
        }
        #endregion 
        //"key hold" event
        else if(kbCurrentState.IsKeyDown(Keys.Space))
        {
            _message += "Space ";
        }
        #region "key up" event
        else if(_kbPreviousState.IsKeyDown(Keys.Space))
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
