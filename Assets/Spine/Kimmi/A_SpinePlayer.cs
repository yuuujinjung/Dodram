using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class A_SpinePlayer : MonoBehaviour
{
    [SerializeField] private float speed = 4.0f;
    private Animator _animator;
    private string _animationState = "AnimationState";
    private string _formerAnimationState = "FormerAnimationState";
    private Rigidbody2D _characterRigidbody;
    private Vector2 _movement;

    public enum States
    {
        Idle_E = 0,
        Idle_N = 1,
        Idle_S = 2,

        Walk_E = 10,
        Walk_N = 11,
        Walk_S = 12,

        Hit_E = 20,
        Hit_N = 21,
        Hit_S = 22,
    }

    private void Awake()
    {
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;

        _characterRigidbody = GetComponent<Rigidbody2D>();

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateState();
        //_animator.SetInteger(_formerAnimationState,)
    }

    private void FixedUpdate() => OnKeyboard();

    private void OnKeyboard()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        _movement.Normalize();

        _characterRigidbody.velocity = _movement * speed;
    }

    private void UpdateState()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _animator.SetInteger(_animationState, (int)States.Hit_S);
            return;
        }

        if (_movement.x != 0)
        {
            _animator.SetInteger(_animationState, (int)States.Walk_E);

            if(_movement.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        else if (_movement.y > 0)
        {
            _animator.SetInteger(_animationState, (int)States.Walk_N);
        }
        else if (_movement.y < 0)
        {
            _animator.SetInteger(_animationState, (int)States.Walk_S);
        }
        else
        {
            _animator.SetInteger(_animationState, (int)States.Idle_S);
        }

    }
}

