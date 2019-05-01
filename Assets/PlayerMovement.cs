using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 10f;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    public Rigidbody2D player;
    float velocityRecoverFrames = 120;
    float currentDirToVelRatio = 1f;
    private Vector2 movement;
    Vector2 dir = new Vector2();
    Vector2 targetPosition;
    Vector3 mousePosition;

    // Use this for initialization
    void Start() {
        targetPosition = transform.position;

    }

    // Update is called once per frame
    void FixedUpdate() {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        movement = new Vector2(horizontalMove, verticalMove);
        if(movement.magnitude > 0.01)
        {
            dir = movement.normalized;
            if (currentDirToVelRatio == 1)
            {
                player.velocity = dir * speed;
            }
        } 

        if (currentDirToVelRatio < 1f)
        {
            currentDirToVelRatio += 1 / velocityRecoverFrames;
            if (currentDirToVelRatio > 1f)
            {
                currentDirToVelRatio = 1f;
            }
            player.velocity = Vector3.Slerp(player.velocity, dir * speed, currentDirToVelRatio);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentDirToVelRatio = 0;
    }

    private void Update()
    {
        MouseClick();
    }


    void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(this.gameObject.transform.childCount == 0)
            {
                RaycastHit2D hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            }

            //Physics2D.Raycast(ray, out hit)
            if (this.gameObject.transform.childCount != 0)
            {

            }
        }
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir.x = mousePosition.x - transform.position.x;
            dir.y = mousePosition.y - transform.position.y;
            dir = dir.normalized;
            player.velocity = dir * speed;
            //Debug.Log(dir);
        }
    }
}
