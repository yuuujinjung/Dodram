using System;
using UnityEngine;
using MonsterLove.StateMachine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float dashPower;
    private Rigidbody2D _characterRigidbody;
    private Vector2 _movement;

    public bool isMainPlayer;

    private Animator _animator;
    public string currentState;

    private enum States
    {
        Idle,
        Walk,
        Work
    }

    StateMachine<States,StateDriverUnity> FSM;

    public enum Dir
    {
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }
    
    public Dir direction;

    void ChangeAnimation(string newState)
    {
        if (currentState == newState)
        {
            return;
        }
        
        _animator.Play(newState);

        currentState = newState;
    }
    
    bool IsInputMoveKey()
    {
        if (isMainPlayer)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                return true;
            }
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal2") != 0 || Input.GetAxisRaw("Vertical2") != 0)
            {
                return true;
            } 
        }
        return false;
    }

    private void Awake()
    {
        FSM = new StateMachine<States,StateDriverUnity>(this);
        FSM.ChangeState(States.Idle);
    }
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterRigidbody = GetComponent<Rigidbody2D>();
        direction = Dir.Down;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDir();
        FSM.Driver.Update.Invoke();
    }

    private void Movement()
    {
        if (isMainPlayer)
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");   
        }
        else
        {
            _movement.x = Input.GetAxisRaw("Horizontal2");
            _movement.y = Input.GetAxisRaw("Vertical2");   
        }

        _movement.Normalize();

        _characterRigidbody.velocity = _movement * speed;
    }

    private void UpdateDir()
    {
        if (_movement.x > 0)
        {
            direction = Dir.Right;
        }
        else if (_movement.x < 0)
        {
            direction = Dir.Left;
        }
        else if (_movement.y > 0)
        {
            direction = Dir.Up;
        }
        else if (_movement.y < 0)
        {
            direction = Dir.Down;
        }
    }

    /**************************************** Idle ************************************/
    void Idle_Enter()
    {
        
    }
    
    void Idle_Update()
    {
        switch (direction)
        {
            case Dir.Up:
                ChangeAnimation("Player_Idle_Up");
                break;
            case Dir.Down:
                ChangeAnimation("Player_Idle_Down");
                break;
            case Dir.Left:
                ChangeAnimation("Player_Idle_Left");;
                break;
            case Dir.Right:
                ChangeAnimation("Player_Idle_Right");
                break;
        }

        if (IsInputMoveKey())
        {
            FSM.ChangeState(States.Walk);
        }

    }


    
    /**************************************** Walk ************************************/
    protected virtual void Walk_Enter()
    {

    }

    protected virtual void Walk_Update()
    {
        Movement();
        
        switch (direction)
        {
            case Dir.Up:
                ChangeAnimation("Player_Walk_Up");
                break;
            case Dir.Down:
                ChangeAnimation("Player_Walk_Down");
                break;
            case Dir.Left:
                ChangeAnimation("Player_Walk_Left");;
                break;
            case Dir.Right:
                ChangeAnimation("Player_Walk_Right");
                break;
        }

        if (!IsInputMoveKey())
        {
            FSM.ChangeState(States.Idle);
        }

    }
    

}