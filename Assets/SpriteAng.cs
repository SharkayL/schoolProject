using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAng : MonoBehaviour {

    private Transform spriteTransform;
    private Rigidbody2D player;

    // Use this for initialization
    void Start () {
        spriteTransform = this.transform.GetChild(0);
        player = this.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (player.velocity.magnitude != 0)
        {
            Vector3 velocity = player.velocity;
            float angle = Mathf.Atan2(velocity.y, velocity.x);
            spriteTransform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * angle);
        }
    }
}
