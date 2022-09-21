using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class SpinePlayerController : MonoBehaviour
{

    public float speed = 4.0f;
    public SkeletonAnimation SkeletonAnimation;
    public AnimationReferenceAsset[] animClip;

    public enum AnimState
    {
        Idle_E = 0,
        Idle_N = 1,
        Idle_S = 2,

        Walk_E = 3,
        Walk_N = 4,
        Walk_S = 5,

        Hit_E = 6,
        Hit_N = 7,
        Hit_S = 8,
    }

    private AnimState _animState;
    private string CurrentAnimation;

    private Rigidbody2D _characterRigidbody;
    private Vector2 _movement;
    //private Vector2 _formerMovement;

    private void Start()
    {
        // 구독 신청! KeyAction이 Invoke 되면 호출할 함수! (중복을 막기위해 빼준 후 추가)
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;

        _characterRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() => UpdateState();

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

        if (_movement.x > 0)
        {
            _animState = AnimState.Walk_E;
        }
        else if (_movement.x < 0)
        {
            _animState = AnimState.Walk_E;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (_movement.y > 0)
        {
            _animState = AnimState.Walk_N;
        }
        else if (_movement.y < 0)
        {
            _animState = AnimState.Walk_S;
        }
        else
        {
            if (CurrentAnimation == null)
            {
                _animState = AnimState.Idle_S;
                return;
            }

            //작동하지 않는 코드... 이전에 움직이고있던 방향대로 Idle모션을 다르게 주고 싶음
            //if(_formerMovement.x!=0)
            //{
            //    _animState = AnimState.Idle_E;

            //}else if (_formerMovement.y>0)
            //{
            //    _animState = AnimState.Idle_N;
            //}
            else
            {
                _animState = AnimState.Idle_S;
            }
        }

        //
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _animState = AnimState.Hit_S;
        }

        SetCurrentAnimation(_animState);
        //_formerMovement = new Vector2(_movement.x, _movement.y);
    }

    private void AsyncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale)
    {
        //이미 동일한 이름의 애니메이션이 재생중이라면 다시 재생시키지 않음
        if (animClip.name.Equals(CurrentAnimation))
        {
            return;
        }

        SkeletonAnimation.state.SetAnimation(0, animClip, loop).TimeScale = timeScale;
        CurrentAnimation = animClip.name;
    }

    private void SetCurrentAnimation(AnimState state)
    {
        AsyncAnimation(animClip[(int)state], true, 1f);
    }


}