using UnityEngine;

public class A_SpinePlayer : MonoBehaviour
{
    [SerializeField] private float speed;


    private Animator _animator;
    private string _animationState = "AnimationState";
    private string _isDigging = "isDigging";

    private int _currentAnimationState;
    private int _formerAnimationCode = 2; //_currentAnimationState%10, 0==E, 1==N, 2==S
    private bool _isScaleXInverse;

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
        Hit_S = 23,
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
            _isScaleXInverse = false;

            if (_movement.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                _isScaleXInverse = true;
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
                    if (_isScaleXInverse == true)
                    {
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                    }
                    break;
                case 1:
                    _currentAnimationState = (int)States.Idle_N;
                    break;
                case 2:
                    _currentAnimationState = (int)States.Idle_S;
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }


        if (Input.GetKey(KeyCode.LeftControl))
        {
            //_animator.SetTrigger("Hit");
            //_currentAnimationState = (int)States.Hit_S;
            _animator.SetBool(_isDigging, true);
        }
        else
        {
            _animator.SetBool(_isDigging, false);
        }

        _animator.SetInteger(_animationState, _currentAnimationState);
        _formerAnimationCode = _currentAnimationState % 10;

    }
}