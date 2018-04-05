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
        // If the user presses right or left arrow
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Set the animator booleans so it shows the walking animation
            animator.SetBool("Hurt", false);
            animator.SetBool("Duck", false);
            animator.SetBool("Walking", true);

            // Select the direction
            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            // Flip the sprite to the adequate direction
            Flip();

            // Transform the sprite to the correct position
            transform.Translate(Vector3.right * direction * speed * Time.deltaTime, Space.World);
        }
        // Stay in idle mode
        else
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Duck", false);
        }

        // If the space or up arroy keys are pressed
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))))
        {
            //animator.SetBool("Hurt", false);
            animator.SetBool("Duck", false);

            // Check if the player is touching a floor collider
            if (grounded)
            {
                // Start the coroutine for jumping
                StartCoroutine(Jump());
            }
        }

        // If down arrow is pressed then duck the player
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("Duck", true);
        }
    }

    /// <summary>
    /// Flips the sprite to the opposite side
    /// </summary>
    void Flip()
    {
        //Flips sprite
        Vector3 scale = transform.localScale;
        scale.x = direction * 5;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player touches special item
        if (collision.gameObject.tag == "YarnWin")
        {
            if (playOnce == 0)
            {
                // Update score, add stage count and change level 
                data.score += 250;
                data.stage += 1;
                Win();
                playOnce++;
            }
        }

        // If the player touches an item
        if (collision.gameObject.tag == "YarnPoint")
        {
            // Add points to the data object
            data.score += 50;
            // Update the score presented in the Canvas
            pointsText.text = data.score.ToString();
            
        }

        // If the player touches the fall collider
        if(collision.gameObject.tag == "Fall")
        {
            Die();
        }
    }

    /// <summary>
    /// Sets the boolean values for the animator to do the jumping animation
    /// </summary>
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
            // Set the animator boolean value so it executes the hurt animation
            animator.SetBool("Hurt", true);

            // Decrease live count and update the canvas' text
            lives--;
            livesText.text = lives.ToString();
            // If live count reaches zero
            if (lives == 0)
            {
                // Take to the loose scene
                Die();
            }

            // Add knockback physics to the player
            // Calculate Angle Between the collision point and the player
            Vector3 dir = collision.transform.position - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            rigidBody.AddForce(dir * force);
        }

        // If the player is touching the floor set the boolean for grounded to true
        if (collision.gameObject.tag == "Floor")
        {
            grounded = true;
        }
    }

    /// <summary>
    /// If the player is not touching the floor set the boolean for grounded to false
    /// </summary>
    /// <param name="collision">A collision with an object</param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            grounded = false;
        }
    }

    /// <summary>
    /// Destroy the player and take user to the loosing scene
    /// </summary>
    void Die()
    {
        Destroy(gameObject);
        levelManager.LoadLevel("Lose");
    }

    /// <summary>
    /// Destroy the player and take user to the next level
    /// </summary>
    void Win()
    {
        Destroy(gameObject);
        levelManager.LoadNextLevel();
    }
}
