using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RejectionRadius {

    Vector2 point;
    float radius;

    public RejectionRadius(Vector2 point,float radius)
    {
        this.point = point;
        this.radius = radius;
    }

    public bool isInRadius(Vector2 otherPoint)
    {
        return (this.point - otherPoint).magnitude < this.radius;
    }

}
