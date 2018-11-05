using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerState2D
{
    public bool isCollidingLeft { get; set; }
    public bool isCollidingRight { get; set; }
    public bool isCollidingAbove { get; set; }
    public bool isCollidingBelow { get; set; }
    public bool IsMovingDownSlope { get; set; }
    public bool isMovingUpSlope { get; set; }
    public bool IsGrounded { get { return isCollidingBelow; } }
    public float SlopeAngle { get; set; }

    public bool HasCollisions { get { return isCollidingRight || isCollidingLeft || isCollidingAbove || isCollidingBelow; } }

    public void Reset()
    {
        isMovingUpSlope =
            IsMovingDownSlope =
            isCollidingLeft =
            isCollidingRight =
            isCollidingAbove =
            isCollidingBelow = false;

        SlopeAngle = 0;

    }

    public override string ToString()
    {
            return string.Format(
                "(controller: r:{0} l:{1} a:{2} b:{3} down-slope:{4} up-slope: {5} angle: {6}",
                isCollidingLeft,
                isCollidingRight,
                isCollidingAbove,
                isCollidingBelow,
                IsMovingDownSlope,
                isMovingUpSlope,
                SlopeAngle);
    }
}
