using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterAnim : MonoBehaviour {

    private Animator anim;

   void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetTrigger("jump");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetTrigger("crouch");
        }
    }

}
