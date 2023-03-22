using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public float freezeTime = 3f;
    public Text score;
    public Text livesText; // New UI text object for displaying player lives
    public Text winText;
    private int scoreValue = 0;
    private int lives = 3; // New variable for player lives

    public int coinsCollected = 0;
    public int coinsToWin = 7;

    public AudioSource winSound;
    //for the win sound effect

    Animator anim;

    private bool facingRight = true;

    private float hozMovement;

    private bool isOnGround;

    
public Transform groundcheck;
public float checkRadius;
public LayerMask allGround;

    void Start()
    
    

    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        livesText.text = "Lives: " + lives.ToString(); // Initialize lives UI text
        winText.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
    }

void FixedUpdate()
{
    hozMovement = Input.GetAxis("Horizontal");
    float vertMovement = Input.GetAxis("Vertical");
    rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

    // update facingRight based on the player's movement
    if (hozMovement > 0 && !facingRight)
    {
        Flip();
    }
    else if (hozMovement < 0 && facingRight)
    {
        Flip();
    }
}

private void OnCollisionEnter2D(Collision2D collision)
{
    
    if (collision.collider.tag == "Coin")
    {
        scoreValue += 1;
        score.text = scoreValue.ToString();
        Destroy(collision.collider.gameObject);
        CollectCoin();
    }

    if (collision.collider.tag == "Enemy")
    {
        if (collision.collider.gameObject.GetComponent<EnemyScript>().isDestroyed == false)
        {
            lives -= 1;
            livesText.text = "Lives: " + lives.ToString();
            collision.collider.gameObject.GetComponent<EnemyScript>().isDestroyed = true;
            Destroy(collision.collider.gameObject);
            if (lives <= 0)
            {
                Debug.Log("Game over!");
                // Add code to end the game
                winText.gameObject.SetActive(true); // Display "You Lose" text
                winText.text = "You Lose! -Game by Jesus Blanco";
                Time.timeScale = 0f;
            }
        }
    }
}

private void OnCollisionStay2D(Collision2D collision)
{
    if (collision.collider.tag == "Ground")
    {
        if (Input.GetKey(KeyCode.W))
        {
            rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            anim.SetInteger("State", 3); // Change animation state to 3
        }
        else
        {
            anim.SetInteger("State", 0); // Change animation state back to 0
        }
    }
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.D))

        {

          anim.SetInteger("State", 2);

         }

     if (Input.GetKeyUp(KeyCode.D))

        {

          anim.SetInteger("State", 0);

         }

         if (Input.GetKeyDown(KeyCode.A))

        {

          anim.SetInteger("State", 2);

         }

     if (Input.GetKeyUp(KeyCode.A))

        {

          anim.SetInteger("State", 0);

         }

        /* if (Input.GetKeyDown(KeyCode.W))

        {

          anim.SetInteger("State", 3);

         }

     if (Input.GetKeyUp(KeyCode.W))

        {

          anim.SetInteger("State", 0);

         }

   */
    }

void Flip()
{
    facingRight = !facingRight;
    Vector3 theScale = transform.localScale;
    theScale.x *= -1;
    transform.localScale = theScale;
}

    public void CollectCoin()
{
    coinsCollected++;

    if (SceneManager.GetActiveScene().buildIndex == 0) // if on scene 0
    {
        if (coinsCollected == coinsToWin)
        {
            Debug.Log("You win!");
            winText.gameObject.SetActive(true);
            winText.text = "Level 1 Complete";
            winSound.Play();

            // Freeze time for 3 seconds
            //Time.timeScale = 0.5f;
            StartCoroutine(LoadNextLevel());
        }
    }
    else if (SceneManager.GetActiveScene().buildIndex == 1) // if on scene 1
    {
        if (coinsCollected == coinsToWin ) 
        {
            Debug.Log("You win!");
            winText.gameObject.SetActive(true);
            winText.text = "You Win! -Game by Jesus Blanco";
            winSound.Play();

            // Freeze time for 3 seconds
            Time.timeScale = 0f;
            //StartCoroutine(LoadNextLevel());
        }
    }
}

    IEnumerator LoadNextLevel()
{
    //yield return new WaitForSecondsRealtime(freezeTime);
    Time.timeScale = 0f;
    yield return new WaitForSecondsRealtime(3f);
    Time.timeScale = 1f;
    // Load scene 1 after the freeze time
    SceneManager.LoadScene(1);
}


}

