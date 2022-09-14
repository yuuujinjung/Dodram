using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody2D _characterRigidbody;
    private Vector2 _movement;


    Animator _animator;
    string _animationState = "AnimationState";


    bool isHold = false;
    public GameObject pickupItem;

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
        // 구독 신청! KeyAction이 Invoke 되면 호출할 함수! (중복을 막기위해 빼준 후 추가)
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        
        _animator = GetComponent<Animator>();
        _characterRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

        }

        if(pickupItem != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                pickupItem = null;
            }
        }
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


    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("item") && Input.GetKeyDown(KeyCode.LeftShift))
        {
            pickupItem.transform.position = this.transform.position;
            pickupItem = col.gameObject;
        }
        else if (col.gameObject.tag.Equals("tool") && Input.GetKeyDown(KeyCode.LeftShift))
        {
            pickupItem.transform.position = this.transform.position;
            pickupItem = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            isHold = false;
        }
    }

}