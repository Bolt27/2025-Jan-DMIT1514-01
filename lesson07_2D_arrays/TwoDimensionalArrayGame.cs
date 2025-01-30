using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson07_2D_arrays;

public class TwoDimensionalArrayGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private SpriteFont _arialFont;

    public enum GameSpaceState
    {
        X, O, Empty
    }
    //using your coding toolkit from CPSC1012 combined with enums, the code below
    //is the most scalable and efficient code data structure that you can create
    //however, the end goal is a 2D array (an array of arrays)
    private GameSpaceState[,] _gameBoard;
    //_gameBoard[1, 2] = GameSpaceState.X;

    public TwoDimensionalArrayGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _arialFont = Content.Load<SpriteFont>("SystemArialFont");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        int[] myNumbers = new int[10];
        for(int c = 0; c < 10; c++)
        {
            myNumbers[c] = c + 3;
        }
        // for(int c = 0; c < 10; c++)
        // {
        //     _spriteBatch.DrawString(_arialFont, myNumbers[c]  + "", new Vector2(c * 30, 0), Color.CadetBlue);
        // }

        //Exercise 01: print the array out in reverse order
        // int xLocation = 0;
        // for(int c = myNumbers.Length - 1; c >= 0; c--)
        // {
        //     _spriteBatch.DrawString(_arialFont, myNumbers[c] + "", new Vector2(xLocation++ * 30, 0), Color.CadetBlue);
        // }
        int[,] numArray = 
        {
            {1, 2, 3, 4 },
            {5, 6, 7, 8 },
            {9, 10, 11, 12 }
        };
        //in a 2D array, the first index is the rows, the second is the columns
        //so, to output the number "12", we use row index 2 and column index 3
        //_spriteBatch.DrawString(_arialFont, numArray[2, 3] + "", Vector2.Zero, Color.CadetBlue);

        for(int row = 0; row < numArray.GetLength(0); row++)
        {
            for(int column = 0; column < numArray.GetLength(1); column++)
            {
                _spriteBatch.DrawString(_arialFont, numArray[row, column] + "", 
                        new Vector2(column * 30, row * 30), Color.CadetBlue);
            }
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
