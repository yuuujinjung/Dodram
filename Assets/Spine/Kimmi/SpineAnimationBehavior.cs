using Spine.Unity;
using UnityEngine;

public class SpineAnimationBehavior : StateMachineBehaviour
{

    public AnimationClip motion;
    string animationClip;
    bool isLoop;

    [Header("Spine Motion Layer")]
    public int layer = 0;
    public float timeScale = 1f;

    private SkeletonAnimation _skeletonAnimation;
    private Spine.AnimationState _spineAnimationState;
    private Spine.TrackEntry _trackEntry;



    private void Awake()
    {

        if (motion != null)
        {
            animationClip = motion.name;
            //Debug.Log(animationClip);
        }
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (_skeletonAnimation == null)
        {
            _skeletonAnimation = animator.GetComponentInChildren<SkeletonAnimation>();
            _spineAnimationState = _skeletonAnimation.state;
        }

        if (animationClip != null)
        {
            isLoop = stateInfo.loop;
            _trackEntry = _spineAnimationState.SetAnimation(layer, animationClip, isLoop);
            _trackEntry.TimeScale = timeScale;
            Debug.Log(animationClip);
        }
    }
}
