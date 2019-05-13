using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBunny {

    Vector2 point;
    float radius;


    public CheckBunny(Vector2 point, float radius)
    {
        this.point = point;
        this.radius = radius;
    }

    public bool isInRadius(Vector2 otherPoint)
    {
        return (this.point - otherPoint).magnitude < this.radius;
    }

}
