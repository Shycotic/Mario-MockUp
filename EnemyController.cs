using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator anim;
    public float speed;
    private bool facingRight = false;

    public LayerMask isGround;
    public LayerMask isPlayer;
    public Transform playerHitBox;
    public Transform wallHitBox;
    private bool wallHit;
    private bool playerHit;
    public float wallHitHeight;
    public float wallHitWidth;
    public float playerHitWidth;
    public float playerHitHeight;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isDead", false);
        playerHit = false;
    }


    void FixedUpdate()
    {
       
        transform.Translate(speed * Time.deltaTime, 0, 0);

        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);

        if (wallHit == true)
        {
            speed = speed * -1;
        }
        playerHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(playerHitWidth, playerHitHeight), 0, isPlayer);
        Debug.Log("playerhit is " + playerHit);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && playerHit == true)
        {
           
            Debug.Log("I can't believe you killed the only other intelligent lifeform in here.");
            collision.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallHitBox.position, new Vector3(wallHitWidth, wallHitHeight, 1));
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(playerHitBox.position, new Vector3(playerHitWidth, playerHitHeight, 1));

    }
}
