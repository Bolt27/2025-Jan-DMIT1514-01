using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson15_MosquitoAttack_Cannon;

public class FireBall : Projectile
{
    private CelAnimationSequence _animationSequence;
    private CelAnimationPlayer _animationPlayer;

    public FireBall()
    {
        _animationPlayer = new CelAnimationPlayer();
    }

    //"override" means "to hide the parent method"
    //the compiler will run this one, not the parent one
    internal override void Initialize(Rectangle gameBoundingBox)
    {
        //"base" = the parent object
        //"this" = the object that we are in
        base.Initialize(gameBoundingBox);
        _animationPlayer.Play(_animationSequence);
        _dimensions = new Vector2(_animationSequence.CelWidth, _animationSequence.CelHeight);
    }
    internal override void LoadContent(ContentManager content)
    {
        _animationSequence = new CelAnimationSequence(content.Load<Texture2D>("FireBall"), 5, 1 / 8f);
    }
    internal override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        switch(_state)
        {
            case State.Flying:
                _animationPlayer.Update(gameTime);
                break;
            case State.NotFlying:
                break;
        }
    }
    internal override void Draw(SpriteBatch spriteBatch)
    {
        switch(_state)
        {
            case State.Flying:
                _animationPlayer.Draw(spriteBatch, _position, SpriteEffects.None);
                break;
            case State.NotFlying:
                break;
        }
    }
}