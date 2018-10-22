using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private int count;
    private bool facingRight = true;
    private Animator anim;

    public float speed;
    public float jumpforce;
    public Text countText;
  


    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    // private float jumpTimeCounter;
    //public float jumpTime;
    //private bool isJumping;

    private AudioSource source;
    public AudioClip jumpSound;
    public AudioClip pickupSound;
    public AudioClip endSound;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isDead", false);
      
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        SetCountText();

    }

    void Awake()
    {

       source = GetComponent<AudioSource>();

    }

  
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");

        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        Debug.Log(isOnGround);

        
        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

    }

    private void OnCollisionStay2D(Collision2D collision)

    {

        if (collision.collider.tag == "Ground" && isOnGround)
           
        {
          

            if (Input.GetKey(KeyCode.UpArrow))
            {
                float vol = Random.Range(volLowRange, volHighRange);
                        source.PlayOneShot(jumpSound);

                rb2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                rb2d.velocity = Vector2.up * jumpforce;

                

            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Coins"))           
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(pickupSound);

            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

        }

        if (other.gameObject.CompareTag("EndGame"))
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(endSound);

        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8) ;
    }

}
