using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionUtils
{
    
    public static Direction VectorDirection(Vector2 vector)
    {
        float angle = Vector2.SignedAngle(vector, Vector2.right);
        return AngleDirection(angle);
    }

    public static Direction AngleDirection(float angle)
    {
        if (angle <= 45 && angle >= -45)
        {
            return Direction.RIGHT;
        } else if (angle > 45 && angle <= 135)
        {
            return Direction.FORWARDS;
        } else if (angle < -45 && angle >= -135)
        {
            return Direction.BACKWARDS;
        }
        else
        {
            return Direction.LEFT;
        }
    }

    //UnitVector emerging from the sides of the bounding box
    public static Vector2 UnitVector(Direction direction)
    {
        switch (direction)
        {
            case Direction.LEFT:
                return Vector2.left;
            case Direction.RIGHT:
                return Vector2.right;
            case Direction.FORWARDS:
                return Vector2.down;
            case Direction.BACKWARDS:
            default:
                return Vector2.up;
        }
    }
    
}
