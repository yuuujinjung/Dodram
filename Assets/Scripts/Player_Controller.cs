using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    [SerializeField] private float speed;

    private Vector2 movement;
    private new Rigidbody2D rigidbody;

    Animator animator;
    string animationState = "AnimationState";

    enum States
    {
        right = 1,
        left = 2,
        up = 3,
        down = 4,
        idle = 5
    }


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    private void FixedUpdate()
    {
        MoveCharactor();
    }

    private void MoveCharactor()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        rigidbody.velocity = movement.normalized * speed;
    }

    private void UpdateState()
    {
        if (movement.x > 0) 
        {
            animator.SetInteger(animationState, (int)States.right);
        }
        else if (movement.x < 0)
        {
            animator.SetInteger(animationState, (int)States.left);
        }
        else if (movement.y > 0)
        {
            animator.SetInteger(animationState, (int)States.up);
        }
        else if (movement.y < 0)
        {
            animator.SetInteger(animationState, (int)States.down);
        }
        else
        {
            animator.SetInteger(animationState, (int)States.idle);
        }



    }
}
