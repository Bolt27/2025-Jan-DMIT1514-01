using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class HUD 
{
#region draw logic
    private const int _Height = 40;
    private SpriteFont _textFont;
    private Texture2D _background;
    private Vector2 _position; //top left corner of the HUD

    //the positions below are in local space, they are offset relative to _position
    private Vector2 _xScorePosition, _oScorePosition, _messagePosition;
#endregion

#region game data
    private string _message = "";
    private int _xTurnCount = 0, _oTurnCount = 0;
#endregion

    internal void Initialize(Vector2 position)
    {
        _position = position;

        _xScorePosition = new Vector2(_position.X + 5, _position.Y + 5);
        _oScorePosition = new Vector2(_position.X + 5, _position.Y + 18);
        _messagePosition = new Vector2(_position.X + 52, _position.Y + 10);
    }
    internal void LoadContent(ContentManager content)
    {
        _background = content.Load<Texture2D>("HUDBackground");
        _textFont = content.Load<SpriteFont>("SystemArialFont");
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_background, _position, Color.White);
        spriteBatch.DrawString(_textFont, "X = " + _xTurnCount, _xScorePosition, Color.Blue);
        spriteBatch.DrawString(_textFont, "O = " + _oTurnCount, _oScorePosition, Color.Blue);
        spriteBatch.DrawString(_textFont, _message, _messagePosition, Color.Blue);
    }
}