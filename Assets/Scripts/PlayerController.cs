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

    private void FixedUpdate()
    {
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

        if (this.GetComponent<PickUpScript>().GaugePer != 0.0f)
        {
            FSM.ChangeState(States.Work);
        }

    }


    
    /**************************************** Walk ************************************/
    protected virtual void Walk_Enter()
    {

    }

    protected virtual void Walk_Update()
    {
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

        if (!IsInputMoveKey())
        {
            FSM.ChangeState(States.Idle);
        }
        
        if (this.GetComponent<PickUpScript>().GaugePer != 0.0f)
        {
            FSM.ChangeState(States.Work);
        }
    }

    
    /**************************************** Work ************************************/

    protected virtual void Work_Enter()
    {
        _movement.x = 0.0f;
        _movement.y = 0.0f;
    }

    protected virtual void Work_Update()
    {
        if (this.GetComponent<PickUpScript>().GaugePer == 0.0f)
        {
            FSM.ChangeState(States.Idle);
        }
        
        switch (direction)
        {
            case Dir.Up:
                if (this.GetComponent<PickUpScript>().Hand.transform.GetChild(0).name == "PickAxe")
                    ChangeAnimation("Player_Use_Pickaxe_Up");
                if (this.GetComponent<PickUpScript>().Hand.transform.GetChild(0).name == "Axe")
                    ChangeAnimation("Player_Use_Axe_Up");
                if (this.GetComponent<PickUpScript>().Hand.transform.GetChild(0).name == "Scythe")
                    ChangeAnimation("Player_Use_Shovel_Up");
                break;
            case Dir.Down:
                if (this.GetComponent<PickUpScript>().Hand.transform.GetChild(0).name == "PickAxe")
                    ChangeAnimation("Player_Use_Pickaxe_Down");
                if (this.GetComponent<PickUpScript>().Hand.transform.GetChild(0).name == "Axe")
                    ChangeAnimation("Player_Use_Axe_Down");
                if (this.GetComponent<PickUpScript>().Hand.transform.GetChild(0).name == "Scythe")
                    ChangeAnimation("Player_Use_Shovel_Down");
                break;
            case Dir.Left:
                if (this.GetComponent<PickUpScript>().Hand.transform.GetChild(0).name == "PickAxe")
                    ChangeAnimation("Player_Use_Pickaxe_Left");
                if (this.GetComponent<PickUpScript>().Hand.transform.GetChild(0).name == "Axe")
                    ChangeAnimation("Player_Use_Axe_Left");
                if (this.GetComponent<PickUpScript>().Hand.transform.GetChild(0).name == "Scythe")
                    ChangeAnimation("Player_Use_Shovel_Left");
                break;
            case Dir.Right:
                if (this.GetComponent<PickUpScript>().Hand.transform.GetChild(0).name == "PickAxe")
                    ChangeAnimation("Player_Use_Pickaxe_Right");
                if (this.GetComponent<PickUpScript>().Hand.transform.GetChild(0).name == "Axe")
                    ChangeAnimation("Player_Use_Axe_Right");
                if (this.GetComponent<PickUpScript>().Hand.transform.GetChild(0).name == "Scythe")
                    ChangeAnimation("Player_Use_Shovel_Right");
                break;
        }
        
        
    }


}