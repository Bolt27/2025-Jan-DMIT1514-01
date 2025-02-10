using System.Data;
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
    private int correspondingGameBoardRow;
    private int correspondingGameBoardColumn;
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
                    //now, we have to get the X and Y of where they clicked
                    int x = _currentMouseState.X;//84
                    int y = _currentMouseState.Y;//26

                    correspondingGameBoardRow = y / _xImage.Height; //convert 26 to 0
                    correspondingGameBoardColumn = x / _xImage.Width; //convert 84 to 0
                    
                    if(_gameBoard[correspondingGameBoardRow, correspondingGameBoardColumn] == GameSpaceState.Empty)
                    {
                        _currentGameState = GameState.MakePlayerMove;
                    }
                    //now, we can use the above two variables to access our 2D array
                    //and see if the move is valid
                    //todo: adjust for the line width (use a CONSTANT)
                }
                break;
            case GameState.MakePlayerMove:
                //todo: use the technique from above
                //to convert from clicked-on pixels to the 2D array,
                //and place the token in the game space
                _gameBoard[correspondingGameBoardRow, correspondingGameBoardColumn] = _nextTokenToBePlayed;
                _currentGameState = GameState.EvaluatePlayerMove;
                break;
            case GameState.EvaluatePlayerMove:
                //todo: determine if there is a winner by examining _gameBoard
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
                //was there a winner? if so, move to GameOver
                //else, change nextTokenToBePlayed
                //and then go to WaitForPlayerMove

                // _currentGameState = GameState.WaitForPlayerMove;
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
                //todo: 
                GraphicsDevice.Clear(Color.Black);
                string message = _nextTokenToBePlayed == GameSpaceState.X ? "Congratulations X, \nYou Win!\n" : "Congratulations O, \nYou Win!\n";
                if (IsTheBoardFull() && !CheckForWinner(GameSpaceState.X) && !CheckForWinner(GameSpaceState.O))
                {
                    message = "There is no winner, \nit's a tie!\n";
                }
                _spriteBatch.DrawString(_arialFont, message + "\nClick to play again.", new Vector2(5, 5), Color.Red);
                //2. output the "X has won, click anywhere to play again" message
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
            for(int column = 0; column < _gameBoard.GetLength(1); column++)
            {
                if(_gameBoard[row, column] == GameSpaceState.X)
                {
                    Vector2 drawPositionX = new Vector2(column * _xImage.Width, row * _xImage.Height);
                    _spriteBatch.Draw(_xImage, drawPositionX, Color.White);
                }
                else if (_gameBoard[row, column] == GameSpaceState.O)
                {
                    Vector2 drawPositionY = new Vector2(column * _oImage.Width, row * _oImage.Height);
                    _spriteBatch.Draw(_oImage, drawPositionY, Color.White);
                }
            }
        }
    }
    private bool CheckForWinner(GameSpaceState player)
    {
        for (int i = 0; i < _gameBoard.GetLength(0); i++)
        {
            // Check the rows
            if (_gameBoard[i, 0] == player && _gameBoard[i, 1] == player && _gameBoard[i, 2] == player)
            {
                return true;
            }
            // Check the columns
            if (_gameBoard[0, i] == player && _gameBoard[1, i] == player &&        _gameBoard[2, i] == player)
            {
                return true;
            }
        }
        // Check Diagonals
        if (_gameBoard[0, 0] == player && _gameBoard[1, 1] == player && _gameBoard[2, 2] == player)
        {
            return true;
        }
        if (_gameBoard[0, 2] == player && _gameBoard[1, 1] == player && _gameBoard[2, 0] == player)
        {
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
