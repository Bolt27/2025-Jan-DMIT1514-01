using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson15_MosquitoAttack_Cannon;

public class Cannon 
{
    private CelAnimationSequence _animationSequence;
    private CelAnimationPlayer _animationPlayer;
    private Vector2 _position;

    internal void Initialize(Vector2 initialPosition)
    {
        _position = initialPosition;
        _animationPlayer = new CelAnimationPlayer();
        _animationPlayer.Play(_animationSequence);
    }
    internal void LoadContent(ContentManager content)
    {
        Texture2D cannonTexture = content.Load<Texture2D>("Cannon");
        _animationSequence = new CelAnimationSequence(cannonTexture, 40, 1 / 8.0f);
    }
    internal void Update(GameTime gameTime)
    {
        _animationPlayer.Update(gameTime);
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        _animationPlayer.Draw(spriteBatch, _position, SpriteEffects.None);
    }

}