using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundFruit : MonoBehaviour {
        Vector2 point;
    Vector2 targetPos;
        float radius = 3f;
    float maxVelocity = 3f;
    float acceleration = 3f;
    float deleleration = 40f;
    float friction = 0.99f;
    bool bunnyMove = false;
    Fruit targetFruit;
    bool hide = false;
	// Use this for initialization
	void Start () {
        point = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(targetFruit == null)
        {
            HideUnderATree();
        }
        CheckMove(point, radius);
        if (bunnyMove) {
            MoveBunny(targetPos);
        }
    }
    void CheckMove(Vector2 point, float radius)
    {
        this.point = point;
        this.radius = radius;
        CheckBunny checkBunny = new CheckBunny(point, radius);
        Fruit[] droppedFruits = FindObjectsOfType<Fruit>();
        foreach (var droppedFruit in droppedFruits)
        {
            if (droppedFruit.isOnTree == false && droppedFruit.isDroped)
            {
                Vector2 pos = droppedFruit.transform.position;
                if (checkBunny.isInRadius(pos))
                {
                    bunnyMove = true;
                    targetPos = pos;
                }
            }
        }
    }

    void MoveBunny(Vector2 pos)
    {
        Rigidbody2D bunny = GetComponent<Rigidbody2D>();
        Vector2 bunnyVel = bunny.velocity;
        Vector2 dir = pos - (Vector2)transform.position;
        Vector2 normDir = dir.normalized;
        Vector2 normVel = bunny.velocity.normalized;
        float dot = Vector2.Dot(normDir, normVel);
        float accel = Mathf.Lerp(deleleration, acceleration, (dot + 1) / 2);
        dir = normDir * (accel * Time.deltaTime);

        Vector2 targetVel = bunny.velocity + dir;
        if (targetVel.magnitude > maxVelocity)
        {
            targetVel = targetVel.normalized * maxVelocity;
        }
        bunny.velocity = targetVel * friction;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fruit")
        {
            
            var fruit = collision.GetComponent<Fruit>();
            if(fruit)
            {
                targetFruit = fruit;
                fruit.ScheduleEating();
                HideUnderATree();
            }
        }
    }

    public DetectTree FindClosestTree()
    {
        DetectTree[] trees = FindObjectsOfType<DetectTree>();
        DetectTree closestTree = null;
        float minDist = Mathf.Infinity;
        var treeList = new List<DetectTree>(trees);
        foreach (var tree in trees)
        {
            float dist = Vector2.Distance(tree.transform.position, transform.position);
            if (dist < minDist)
            {
                closestTree = tree;
                minDist = dist;
            }
        }
        return closestTree;
    }

    void HideUnderATree()
    {

        var tree = this.FindClosestTree();
        targetPos = tree.transform.position;
        bunnyMove = true;
        hide = false;
        
    }


}
