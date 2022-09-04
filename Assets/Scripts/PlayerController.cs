using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody characterRigidbody;
    private Vector3 movement;
    private Vector3 heading;

    private Transform mainCamera;

    public Animator animator;
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
        characterRigidbody = GetComponent<Rigidbody>();
        mainCamera = GameObject.Find("Main Camera").transform;
        heading = mainCamera.localRotation * Vector3.forward;
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
        heading.y = 0;
        heading = heading.normalized;

        //정면이동 대입연산
        movement = heading * Time.deltaTime * Input.GetAxisRaw("Vertical");
        //측면이동 합연산
        movement += Quaternion.Euler(0, 90, 0) * heading * Time.deltaTime * Input.GetAxisRaw("Horizontal");

        characterRigidbody.velocity = transform.TransformDirection(movement.normalized * speed);

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
        else if (movement.z > 0)
        {
            animator.SetInteger(animationState, (int)States.up);
        }
        else if (movement.z < 0)
        {
            animator.SetInteger(animationState, (int)States.down);
        }
        else
        {
            animator.SetInteger(animationState, (int)States.idle);
        }
    }
}
