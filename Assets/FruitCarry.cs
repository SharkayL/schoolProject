using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCarry : MonoBehaviour {

    private Fruit fruit;
    private bool isDelivering = false;
    private Vector2 targetPos;
    private Transform spriteTransform;
    public float dropOffRadius = 0.7f;

    // Use this for initialization
    void Start () {
        spriteTransform = this.transform.GetChild(0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.fruit != null)
        {
            return;
        }
        Fruit otherFruit = collision.GetComponent<Fruit>();
        if(otherFruit != null && otherFruit.isOnTree)
        {
            this.fruit = otherFruit;
            this.fruit.isOnTree = false;
            this.fruit.transform.SetParent(this.spriteTransform);
            this.fruit.transform.localPosition = new Vector3(0.8f, 0, 0);
        }
    }

    // Update is called once per frame
    void Update () {
        if(fruit != null && Input.GetMouseButtonDown(0))
        {
            isDelivering = true;
            targetPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
	    if(isDelivering && fruit != null)
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
