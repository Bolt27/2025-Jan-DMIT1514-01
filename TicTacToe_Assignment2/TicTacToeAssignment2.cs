using System.Collections.Generic;
using System.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TicTacToe_Assignment2;

public class TicTacToe : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _gameBoardImage, _xImage, _oImage;
    private MouseState _currentMouseState, _previousMouseState;
    private const int _WindowWidth = 170, _WindowHeight = 170;
    private int correspondingGameBoardRow;
    private int correspondingGameBoardColumn;
    Vector2 tokenPosition;
    Color tokenColor = Color.White;
    private List<Vector2> _winningToken = new List<Vector2>();
    private SpriteFont _arialFont;
    public enum GameState
    {
        Initialize, WaitForPlayerMove, MakePlayerMove, EvaluatePlayerMove, GameOver
    }
    private GameState _currentGameState = GameState.Initialize;

    public enum GameSpaceState
    {
        X, O, Empty
    }
    private GameSpaceState _nextTokenToBePlayed;

    private GameSpaceState[,] _gameBoard = 
        new GameSpaceState[,]
        {
            {GameSpaceState.Empty, GameSpaceState.O, GameSpaceState.Empty},
            {GameSpaceState.X, GameSpaceState.Empty, GameSpaceState.Empty},
            {GameSpaceState.Empty, GameSpaceState.X, GameSpaceState.O}
        };

#region set up methods
    public TicTacToe()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
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

        _arialFont = Content.Load<SpriteFont>("SystemArialFont");
        _gameBoardImage = Content.Load<Texture2D>("TicTacToeBoard");
        _xImage = Content.Load<Texture2D>("X");
        _oImage = Content.Load<Texture2D>("O");

    }
#endregion

    protected override void Update(GameTime gameTime)
    {
        _currentMouseState = Mouse.GetState();

        switch(_currentGameState)
        {
            case GameState.Initialize:
                _nextTokenToBePlayed = GameSpaceState.X;
                for (int r = 0; r < _gameBoard.GetLength(0); r++)
                {
                    for (int c = 0; c < _gameBoard.GetLength(1); c++)
                    {
                        _gameBoard[r, c] = GameSpaceState.Empty;
                    }
                }
                _currentGameState = GameState.WaitForPlayerMove;
                break;
            case GameState.WaitForPlayerMove:
                if(_previousMouseState.LeftButton == ButtonState.Pressed
                    && _currentMouseState.LeftButton == ButtonState.Released)
                {                    
                    int x = _currentMouseState.X;
                    int y = _currentMouseState.Y;

                    correspondingGameBoardRow = y / _xImage.Height; 
                    correspondingGameBoardColumn = x / _xImage.Width; 
                    
                    if(_gameBoard[correspondingGameBoardRow, correspondingGameBoardColumn] == GameSpaceState.Empty)
                    {
                        _currentGameState = GameState.MakePlayerMove;
                    }
                }
                break;
            case GameState.MakePlayerMove:
                _gameBoard[correspondingGameBoardRow, correspondingGameBoardColumn] = _nextTokenToBePlayed;
                _currentGameState = GameState.EvaluatePlayerMove;
                break;
            case GameState.EvaluatePlayerMove:
                if (CheckForWinner(_nextTokenToBePlayed))
                {
                    _currentGameState = GameState.GameOver;
                }   
                else if (IsTheBoardFull())
                {
                    _currentGameState = GameState.GameOver;
                }
                else
                {
                    _nextTokenToBePlayed = _nextTokenToBePlayed == GameSpaceState. X ? GameSpaceState.O : GameSpaceState.X;
                    _currentGameState = GameState.WaitForPlayerMove;
                }
                break;
            case GameState.GameOver:
                 if(_previousMouseState.LeftButton == ButtonState.Pressed
                    && _currentMouseState.LeftButton == ButtonState.Released)
                    {
                        _currentGameState = GameState.Initialize;
                    }
                break;
        }

        _previousMouseState = _currentMouseState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        if (_currentGameState != GameState.GameOver)
        {
            _spriteBatch.Draw(_gameBoardImage, Vector2.Zero, Color.White);
            this.DrawCurrentGameBoard();
        }
        switch(_currentGameState)
        {
            case GameState.Initialize:
                break;
            case GameState.WaitForPlayerMove:
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
                break;
            case GameState.MakePlayerMove:
                break;
            case GameState.EvaluatePlayerMove:
                break;
            case GameState.GameOver:
                GraphicsDevice.Clear(Color.Black);
                string message = _nextTokenToBePlayed == GameSpaceState.X ? "Congratulations X, \nYou Win!\n" : "Congratulations O, \nYou Win!\n";
                _spriteBatch.DrawString(_arialFont, "Press Space to \nsee winner", new Vector2(0, 100), Color.White);
                if(Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (CheckForWinner(GameSpaceState.X) || CheckForWinner(GameSpaceState.O))
                    {
                        if (_winningToken.Contains(tokenPosition))
                        {
                            this.DrawCurrentGameBoard();
                            tokenColor = Color.Green;
                        }
                    }
                }
                if (IsTheBoardFull() && !CheckForWinner(GameSpaceState.X) && !CheckForWinner(GameSpaceState.O))
                {
                    message = "There is no winner, \nit's a tie!\n";
                }
                _spriteBatch.DrawString(_arialFont, message + "\nClick to play again.", new Vector2(5, 5), Color.Red);
                //3. for 10/10 marks, somehow make the winning line visible
                break;
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
    private void DrawCurrentGameBoard()
    {
        //make it draw the tokens, taking into account the line widths
        for(int row = 0; row < _gameBoard.GetLength(0); row++)
        {
            int lineOffset = 18;
            for(int column = 0; column < _gameBoard.GetLength(1); column++)
            {
                int xOffset = (column == 0) ? 0 : (column == 1) ? lineOffset / 2 : lineOffset;
                int yOffset = (row == 0) ? 0 : (row == 1) ? lineOffset / 2 : lineOffset;
                tokenPosition = new Vector2((column * _xImage.Width) + xOffset, (row * _xImage.Height) + yOffset);
                if(_gameBoard[row, column] == GameSpaceState.X)
                {
                    Vector2 drawPositionX = new Vector2((column * _xImage.Width) + xOffset, (row * _xImage.Height) + yOffset);
                    _spriteBatch.Draw(_xImage, drawPositionX, Color.White);
                }
                else if (_gameBoard[row, column] == GameSpaceState.O)
                {
                    Vector2 drawPositionO = new Vector2((column * _oImage.Width) + xOffset, (row * _oImage.Height) + yOffset);
                    _spriteBatch.Draw(_oImage, drawPositionO, Color.White);
                }
            }
        }
    }
    private bool CheckForWinner(GameSpaceState player)
    {
        _winningToken.Clear();
        for (int i = 0; i < _gameBoard.GetLength(0); i++)
        {
            // Check the rows
            if (_gameBoard[i, 0] == player && _gameBoard[i, 1] == player && _gameBoard[i, 2] == player)
            {
                _winningToken.Add(new Vector2(i, 0));
                _winningToken.Add(new Vector2(i, 1));
                _winningToken.Add(new Vector2(i, 2));
                return true;
            }
            // Check the columns
            if (_gameBoard[0, i] == player && _gameBoard[1, i] == player &&        _gameBoard[2, i] == player)
            {
                _winningToken.Add(new Vector2(0, i));
                _winningToken.Add(new Vector2(1, i));
                _winningToken.Add(new Vector2(2, i));
                return true;
            }
        }
        // Check Diagonals
        if (_gameBoard[0, 0] == player && _gameBoard[1, 1] == player && _gameBoard[2, 2] == player)
        {
            _winningToken.Add(new Vector2(0, 0));
            _winningToken.Add(new Vector2(1, 1));
            _winningToken.Add(new Vector2(2, 2));
            return true;
        }
        if (_gameBoard[0, 2] == player && _gameBoard[1, 1] == player && _gameBoard[2, 0] == player)
        {
            _winningToken.Add(new Vector2(0, 2));
            _winningToken.Add(new Vector2(1, 1));
            _winningToken.Add(new Vector2(2, 0));
            return true;
        }
        
        return false;
    }
    private bool IsTheBoardFull()
    {
        for (int r = 0; r < _gameBoard.GetLength(0); r++)
        {
            for (int c = 0; c < _gameBoard.GetLength(1); c++)
            {
                if (_gameBoard[r, c] == GameSpaceState.Empty)
                {
                    return false;
                }
            }
        }
        return true;
    }
}

/// I tried to get the code to work to change the winning lines tokens to green, i couldnt get it to work before the deadline, would appreciate if 
/// you could leave feedback how I COULD have gotten it to work. If i wasn't on the right track and is a difficult explanation that is alright without feedback! 
/// Thank you, Nash C