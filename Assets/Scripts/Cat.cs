using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour {
    private Cat instance;
    public int speed;
    public float force;

    public int lives;

    public Text livesText;
    public Text pointsText;

    protected LevelManager levelManager;
    protected Rigidbody2D rigidBody;
    protected Animator animator;
    protected Data data;

    private Vector2 move;
    private bool grounded;
    private int direction, playOnce;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        livesText.text = lives.ToString();
        data = GameObject.Find("Data").GetComponent<Data>();
        pointsText.text = data.score.ToString();

        playOnce = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("Hurt", false);
            animator.SetBool("Duck", false);
            animator.SetBool("Walking", true);
            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            Flip();

            transform.Translate(Vector3.right * direction * speed * Time.deltaTime, Space.World);
        }
        else
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Duck", false);
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))))
        {
            //animator.SetBool("Hurt", false);
            animator.SetBool("Duck", false);
            if (grounded)
            {
                StartCoroutine(Jump());
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("Duck", true);
        }
    }

    void Flip()
    {
        //Flips sprite
        Vector3 scale = transform.localScale;
        scale.x = direction * 5;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "YarnWin")
        {
            if (playOnce == 0)
            {
                data.score += 250;
                data.stage += 1;
                Win();
                playOnce++;
            }
        }

        if (collision.gameObject.tag == "YarnPoint")
        {
            data.score += 50;
            pointsText.text = data.score.ToString();
        }

        if(collision.gameObject.tag == "Fall")
        {
            Die();
        }
    }

    IEnumerator Jump()
    {
        animator.SetBool("Jumping", true);
        rigidBody.velocity = (direction == 1) ? new Vector2(2f, 12f) : new Vector2(-2f, 12f);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Jumping", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            animator.SetBool("Hurt", true);
            lives--;
            livesText.text = lives.ToString();
            if (lives == 0)
            {
                Die();
            }

            // Calculate Angle Between the collision point and the player
            Vector3 dir = collision.transform.position - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            rigidBody.AddForce(dir * force);
        }

        if (collision.gameObject.tag == "Floor")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            grounded = false;
        }
    }

    void Die()
    {
        Destroy(gameObject);
        //LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelManager.LoadLevel("Lose");
    }

    void Win()
    {
        Destroy(gameObject);
        //LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelManager.LoadNextLevel();
    }
}
