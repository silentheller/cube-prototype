using UnityEngine;
using System.Collections.Generic;

public class ActorController : MonoBehaviour
{
 
    [SerializeField]    private Animator animator;
    private bool wasGrounded;
    private bool isGrounded;

    void Awake()
    {
        if(!animator) { gameObject.GetComponent<Animator>(); }
    }

    private void OnCollisionEnter(Collision collision)
    {
       isGrounded = true;
    }

   

    private void OnCollisionExit(Collision collision)
    {
       isGrounded = false;
    }

	void FixedUpdate ()
    {
        animator.SetBool("Grounded", isGrounded);
        DirectUpdate();
        wasGrounded =isGrounded;
    }



    private void DirectUpdate()
    {
        JumpingAndLanding();
    }
    private void JumpingAndLanding()
    {

        if (!wasGrounded && isGrounded)
        {
            animator.SetTrigger("Land");
        }

        if (!isGrounded && wasGrounded)
        {
           animator.SetTrigger("Jump");
        }
    }
}
