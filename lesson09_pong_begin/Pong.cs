using System;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson09_pong_begin;

public class Pong : Game
{
    const int _Scale = 2;
    private const int _WindowWidth = 250 * _Scale, _WindowHeight = 150 * _Scale;
    private const int _PLayAreaEdgeLineWidth = 4;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _backgroundTexture;

    private Rectangle _playAreaBoundingBox;
    //to keep track of the state of the ball

    private Ball _ball;

    public Pong()
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

        _ball = new Ball();
                            //position        // Direction      // Scale    // Bounding Box
        _ball.Initialize(new Vector2(50, 65), new Vector2(-1, 1), _Scale, _playAreaBoundingBox);

        _playAreaBoundingBox = new Rectangle(0, 0, _WindowWidth, _WindowHeight);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ball.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        _ball.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_backgroundTexture, Vector2.Zero, null, Color.White, 0, Vector2.Zero, _Scale, SpriteEffects.None, 0);

        _ball.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
