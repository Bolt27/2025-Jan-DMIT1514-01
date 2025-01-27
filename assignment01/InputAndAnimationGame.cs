using System.Data;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace assignment01_animation_and_inputs;

public class InputAndAnimationGame : Game
{
    private const int _WindowWidth = 800;
    private const int _WindowHeight = 384;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Vector2 _priestPosition, _catPosition, _benchPosition;
    private Texture2D _background, _bench;
    private CelAnimationSequence  _priestWalkingLeft, _priestWalkingRight, _catWalking;
    private CelAnimationPlayer _animation01, _animation02, _animation03;
    //private bool walkingLeft = false;

    public InputAndAnimationGame()
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

        _background = Content.Load<Texture2D>("Jesus_Stained_Glass");
        _bench = Content.Load<Texture2D>("Chair_Side");
        Texture2D priestWalkingLeft = Content.Load<Texture2D>("priestWalkingLeft");
        Texture2D priestWalkingRight = Content.Load<Texture2D>("priestWalkingRight");
        Texture2D catWalking = Content.Load<Texture2D>("cat_walk");

        _priestPosition = Vector2.Zero;
        _catPosition = Vector2.Zero;
        _benchPosition = Vector2.Zero;
        _catPosition.Y = 352;
        _priestPosition.Y = 352;
        _benchPosition.X = 375;
        _benchPosition.Y = 360;

        _priestWalkingLeft = new CelAnimationSequence(priestWalkingLeft, 24, 1 / 8f);
        _priestWalkingRight = new CelAnimationSequence(priestWalkingRight, 24, 1 / 8f);
        _catWalking = new CelAnimationSequence(catWalking, 32, 1 / 8f);

        _animation01 = new CelAnimationPlayer();
        _animation02 = new CelAnimationPlayer();
        _animation03 = new CelAnimationPlayer();
        // start animating
        _animation01.Play(_priestWalkingLeft);
        _animation02.Play(_priestWalkingRight);
        _animation03.Play(_catWalking);
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState kbCurrentState = Keyboard.GetState();

        
        if(kbCurrentState.IsKeyDown(Keys.D))
        {  
            //walkingLeft = false;
            _priestPosition.X += 2;
        }
        if(kbCurrentState.IsKeyDown(Keys.A))
        {
            //walkingLeft = true;
            _priestPosition.X -= 2;
        }

        if(_catPosition.X < 768)
        {
            _catPosition.X += 1;
        }
        else
        {
            _catPosition.X -= 1;
        }

        _animation01.Update(gameTime);
        _animation02.Update(gameTime);
        _animation03.Update(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_bench, _benchPosition, null, Color.Brown, 0f, Vector2.Zero, 0.05f, SpriteEffects.None, 0f);
        _animation01.Draw(_spriteBatch, _priestPosition, SpriteEffects.None);
        _animation02.Draw(_spriteBatch, _priestPosition, SpriteEffects.None);
        _animation03.Draw(_spriteBatch, _catPosition, SpriteEffects.None);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
