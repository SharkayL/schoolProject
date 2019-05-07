using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTree : MonoBehaviour {
    Vector2 point;
    private Collider2D collider;
    
	// Use this for initialization
	void Start () {
        this.collider = GetComponent<Collider2D>();
	}

    public bool isOverlapping(Vector2 point)
    {
        if(collider != null)
        {
            return collider.OverlapPoint(point);
        }
        return false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
