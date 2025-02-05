﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson06_tictactoe01_mouse_input;

public class TicTacToe : Game
{
    private const int _WindowWidth = 170, _WindowHeight = 170; 
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _backgroundImage, _xImage, _oImage;

    private MouseState _currentMouseState, _previousMouseState;
    
    public enum GameSpaceState
    {
        X,
        O
    }
    private GameSpaceState _nextTokenToBePlayed = GameSpaceState.X;

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

        this.IsMouseVisible = true;

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
        _currentMouseState = Mouse.GetState();

        //detect a mouse up event
        if(_previousMouseState.LeftButton == ButtonState.Pressed
            && _currentMouseState.LeftButton == ButtonState.Released)
        {
            //declare a data member that will remember the next token to be played
            //change Draw() so that it draws the next token to be played
            //when this "if" statement is entered, change the next token to be played
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
        _spriteBatch.Draw(_backgroundImage, Vector2.Zero, Color.White);

        // Vector2 adjustedMousePosition = new Vector2(
        //     _currentMouseState.Position.X - (_xImage.Width / 2),
        //     _currentMouseState.Position.Y - (_xImage.Width / 2)
        // );
        //to draw the centre of "X" where the mouse is, subtract the Vector2 that
        //represents the centre of the bounding box of "X"
        Vector2 adjustedMousePosition = 
            _currentMouseState.Position.ToVector2() - _xImage.Bounds.Center.ToVector2();
        
        Texture2D tokenToDraw = _oImage;
        if(_nextTokenToBePlayed == GameSpaceState.X)
        {
            tokenToDraw = _xImage;
        }
        _spriteBatch.Draw(tokenToDraw, adjustedMousePosition, Color.White);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
