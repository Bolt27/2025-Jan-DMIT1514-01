using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson18_Platformer;

public class Player
{
    private enum State
    {
        Idle, Walking, Jumping
    }
    private State _state;
    private CelAnimationSequence _idleSequence;
    private CelAnimationSequence _jumpSequence;
    private CelAnimationSequence _walkSequence;

    private CelAnimationPlayer _animationPlayer;

    private Vector2 _position;
    private Vector2 _velocity;
    private Rectangle _gameBoundingBox;
    private Vector2 _dimensions;
}