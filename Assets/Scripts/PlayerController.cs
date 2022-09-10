using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody2D _characterRigidbody;
    private Vector2 _movement;


    Animator _animator;
    string _animationState = "AnimationState";

    enum States 
    {
        Right = 1,
        Left = 2,
        Up = 3,
        Down = 4,
        Idle = 5
    }

    void Start()
    {
        Managers.Input.KeyAction += OnKeyboard;
        _animator = GetComponent<Animator>();
        _characterRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    private void FixedUpdate()
    {
        OnKeyboard();
    }

    private void OnKeyboard()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        _movement.Normalize();

        _characterRigidbody.velocity = _movement * speed;
    }
    
    private void UpdateState()
    {
        if (_movement.x > 0)
        {
            _animator.SetInteger(_animationState, (int)States.Right);
        }
        else if (_movement.x < 0)
        {
            _animator.SetInteger(_animationState, (int)States.Left);
        }
        else if (_movement.y > 0)
        {
            _animator.SetInteger(_animationState, (int)States.Up);
        }
        else if (_movement.y < 0)
        {
            _animator.SetInteger(_animationState, (int)States.Down);
        }
        else
        {
            _animator.SetInteger(_animationState, (int)States.Idle);
        }
    }
}
