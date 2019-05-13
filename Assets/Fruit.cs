using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour {

    public bool isOnTree = true;
    public bool isDroped = false;
    Vector2 point;
    Collider2D collider;

    // Use this for initialization
    void Start()
    {
        this.collider = GetComponent<Collider2D>();
    }
    public bool isOverLapping(Vector2 point)
    {
            if (collider.OverlapPoint(point))
            {
                return true;
            }
            else return false;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void ScheduleEating()
    {
        Invoke("Destroy", 3);
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
