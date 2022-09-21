using UnityEngine;

public class A_SpinePlayer : MonoBehaviour
{
    [SerializeField] private float speed = 4.0f;
    private Animator _animator;
    private string _animationState = "AnimationState";
    private int _currentAnimationState;
    private int _formerAnimationCode; //_currentAnimationState%10, 0==E, 1==N, 2==S
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

        if (_movement.x != 0)
        {
            _currentAnimationState = (int)States.Walk_E;

            if (_movement.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        else if (_movement.y > 0)
        {
            _currentAnimationState = (int)States.Walk_N;
        }
        else if (_movement.y < 0)
        {
            _currentAnimationState = (int)States.Walk_S;
        }
        else
        {
            switch (_formerAnimationCode)
            {
                case 0:
                    _currentAnimationState = (int)States.Idle_E;
                    break;
                case 1:
                    _currentAnimationState = (int)States.Idle_N;
                    break;
                case 2:
                    _currentAnimationState = (int)States.Idle_S;
                    break;
                default:
                    break;
            }
        }


        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _currentAnimationState = (int)States.Hit_S;
        }

        _animator.SetInteger(_animationState, _currentAnimationState);
        _formerAnimationCode = _currentAnimationState % 10;

    }
}

