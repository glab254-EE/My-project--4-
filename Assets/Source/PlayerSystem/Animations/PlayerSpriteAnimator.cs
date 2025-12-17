using UnityEngine;

[RequireComponent(typeof(Animation))]
public class PlayerSpriteAnimator : MonoBehaviour
{
    [field:SerializeField]
    private AnimationClip DefaultLoopClip;
    private Animation _animation;
    private const string DEFAULT_ANIMATION_NAME = "DEFAULT";
    void Start()
    {
        _animation = GetComponent<Animation>();
        if (DefaultLoopClip != null)
            _animation.AddClip(DefaultLoopClip,DEFAULT_ANIMATION_NAME);
    }
    public void PlayAnimation(AnimationClip animationClip)
    {
        _animation.clip = animationClip;
        _animation.Play();
        if (DefaultLoopClip != null)
            _animation.PlayQueued(DEFAULT_ANIMATION_NAME);
    }   
}
