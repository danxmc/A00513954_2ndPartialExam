using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour {
    public int speed;
    public int direction = -1;
    public GameObject sensor;
    public GameObject player;

    public Transform left;
    public Transform right;

    private Vector2 move;
    private bool movingLeft;
    private Vector3 walk;
    private bool moving = true;

    protected Rigidbody2D rigidBody;
    protected Animator animator;
    protected BoxCollider2D box;
    protected BoxCollider2D playerBox;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        box = sensor.GetComponent<BoxCollider2D>();
        playerBox = player.GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (box.IsTouching(playerBox))
        {
            moving = false;
            animator.SetBool("Attacking", true);
            animator.SetBool("Walking", false);
        }
        else
        {
            moving = true;
            animator.SetBool("Attacking", false);
            animator.SetBool("Walking", true);
        }

        if (moving == true)
        {
            if (movingLeft)
            {
                //animator.SetBool("Attacking", false);
                MoveLeft();
            }
            else
            {
                //animator.SetBool("Attacking", false);
                MoveRight();
            }
        }
    }

    void Flip()
    {
        //Flips sprite
        Vector3 scale = transform.localScale;
        scale.x = walk.x * 5;
        transform.localScale = scale;
    }

    void MoveLeft()
    {
        movingLeft = true;
        walk.x = -1;
        Flip();
        transform.position += walk * speed * Time.deltaTime;
    }
    
    void MoveRight()
    {
        movingLeft = false;
        walk.x = 1;
        Flip();
        transform.position += walk * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (movingLeft == false)
            {
                movingLeft = true;
            }
            else
            {
                movingLeft = false;
            }
        }

    }
}
