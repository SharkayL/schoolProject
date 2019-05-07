using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCarry : MonoBehaviour {

    private Fruit fruit;
    private Fruit targetFruit;
    private bool isDelivering = false;

    public Vector2 targetPos;
    private Transform spriteTransform;
    public float dropOffRadius = 0.7f;

    // Use this for initialization
    void Start () {
        spriteTransform = this.transform.GetChild(0);
        fruit = spriteTransform.GetComponentInChildren<Fruit>();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.fruit != null || this.targetFruit == null)
        {
            return;
        }
        Fruit otherFruit = collision.GetComponent<Fruit>();
        if(otherFruit == this.targetFruit)
        {
            this.targetFruit = null;
            this.fruit = otherFruit;
            this.fruit.isOnTree = false;
            this.fruit.transform.SetParent(this.spriteTransform);
            this.fruit.transform.localPosition = new Vector3(0.8f, 0, 0);
        }
    }

    public bool canPlaceFruit(Vector2 point)
    {
        DetectTree[] trees = FindObjectsOfType<DetectTree>();
        foreach(var tree in trees)
        {
            if(tree.isOverlapping(point))
            {
                return false;
            }
        }

        return true;
    }

    public Fruit checkForFruit(Vector2 point)
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();
        foreach(var fruit in fruits)
        {
            if(fruit.isOnTree && fruit.isOverLapping(point))
            {
                return fruit;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            var clickPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (fruit != null) { 

                if (this.canPlaceFruit(clickPos))
                {
                    isDelivering = true;
                    targetPos = clickPos;
                }
                else
                {
                    isDelivering = false;
                }
            }
            else
            {
                this.targetFruit = this.checkForFruit(clickPos);
            }
            targetPos = clickPos;
        }
	    if(isDelivering && fruit != null )
        {
            if((((Vector2)transform.position)-targetPos).magnitude < dropOffRadius)
            {
                fruit.transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);      
                fruit.isDroped = true;
                fruit.transform.SetParent(null);
                isDelivering = false;
                fruit = null;
            }
        }	

	}
}