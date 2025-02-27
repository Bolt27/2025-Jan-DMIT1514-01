using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson13_TicTacToe_HUD;

public class TicTacToe : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _gameBoardImage, _xImage, _oImage;
    private MouseState _currentMouseState, _previousMouseState;
    private const int _WindowWidth = 170, _WindowHeight = 170;

    private HUD _hud;

#region data structures
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
            {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty},
            {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty},
            {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty}
        };
#endregion
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
        _graphics.PreferredBackBufferHeight = _WindowHeight + HUD.Height;
        _graphics.ApplyChanges();

        _hud = new HUD();
        _hud.Initialize(new Vector2(0, _WindowHeight));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _gameBoardImage = Content.Load<Texture2D>("TicTacToeBoard");
        _xImage = Content.Load<Texture2D>("X");
        _oImage = Content.Load<Texture2D>("O");

        _hud.LoadContent(Content);
    }
#endregion

    protected override void Update(GameTime gameTime)
    {
        _currentMouseState = Mouse.GetState();

        switch(_currentGameState)
        {
            case GameState.Initialize:
                _nextTokenToBePlayed = GameSpaceState.X;
                //TODO: set all game board spaces to empty
                //Exercise: loop through _gameBoard (rows and columns), and set every every
                //game board space to Empty
                // for(int row = 0; row < _gameBoard.GetLength(0); row++)
                // {
                //     for(int column = 0; column < _gameBoard.GetLength(1); column++)
                //     {
                //         _gameBoard[row, column] = GameSpaceState.Empty;
                //     }
                // }
                _currentGameState = GameState.WaitForPlayerMove;
                break;
            case GameState.WaitForPlayerMove:
                if(_previousMouseState.LeftButton == ButtonState.Pressed
                    && _currentMouseState.LeftButton == ButtonState.Released)
                {
                    //todo: check if this move is valid
                    //inside of this "if" statement, we know that there was a click
                    //now, we have to get the X and Y of where they clicked
                    int x = _currentMouseState.X;//84
                    int y = _currentMouseState.Y;//26

                    int correspondingGameBoardRow = y / _xImage.Height; //convert 26 to 0
                    int correspondingGameBoardColumn = x / _xImage.Width; //convert 84 to 0
                    
                    //now, we can use the above two variables to access our 2D array
                    //and see if the move is valid
                    //todo: adjust for the line width (use a CONSTANT)

                    //if the move is valid
                    _currentGameState = GameState.MakePlayerMove;
                }
                break;
            case GameState.MakePlayerMove:
                //todo: use the technique from above
                //to convert from clicked-on pixels to the 2D array,
                //and place the token in the game space
                _currentGameState = GameState.EvaluatePlayerMove;
                break;
            case GameState.EvaluatePlayerMove:
                //todo: determine if there is a winner by examining _gameBoard
                
                //was there a winner? if so, move to GameOver
                //else, change nextTokenToBePlayed
                //and then go to WaitForPlayerMove
                if(_nextTokenToBePlayed == GameSpaceState.X)
                {
                    _nextTokenToBePlayed = GameSpaceState.O;
                    _hud.XTurnCount++;
                }
                else
                {
                    _nextTokenToBePlayed = GameSpaceState.X;
                    _hud.OTurnCount++;
                }
                _currentGameState = GameState.WaitForPlayerMove;

                //if we detect a winner
                _hud.Message = "X Wins";

                break;
            case GameState.GameOver:
                //wait for a click anywhere, and then change _currentGameState
                //to Initialize
                break;
        }

        _previousMouseState = _currentMouseState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_gameBoardImage, Vector2.Zero, Color.White);

        _hud.Draw(_spriteBatch);

        this.DrawCurrentGameBoard();
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
                //1. draw the "game over" background
                //2. output the "X has won, click anywhere to play again" message
                //3. for 10/10 marks, somehow make the winning line visible
                break;
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
    private void DrawCurrentGameBoard()
    {
        //todo: make it draw the "O" tokens as well
        //make it draw the tokens, taking into account the line widths
        for(int row = 0; row < _gameBoard.GetLength(0); row++)
        {
            for(int column = 0; column < _gameBoard.GetLength(1); column++)
            {
                if(_gameBoard[row, column] == GameSpaceState.X)
                {
                    Vector2 drawPosition = new Vector2(column * _xImage.Width, row * _xImage.Height);
                    _spriteBatch.Draw(_xImage, drawPosition, Color.White);
                }
            }
        }
    }
}
