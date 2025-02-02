using Spine.Unity;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpineAnimator: MonoBehaviour
{
    [SerializeField] protected SkeletonAnimation skeletonAnimation;
    [SerializeField] string proximityTag;

    private bool stateLocked = false;

    public void UpdateState(int track, string name, bool loop)
    {
        if(stateLocked)
        {
            return;
        }
        skeletonAnimation.AnimationState.SetAnimation(track, name, loop);
    }
    public void PlayWinFace()
    {
        
        UpdateState(0, "Win Face", true);
        stateLocked = true;
    }
    public void PlayLoseFace()
    {
        UpdateState(0, "Lose Face", true);
        stateLocked = true;
    }

    public virtual void PlayProximityToEnemyFace()
    {

    }
    public void LockState()
    {
        stateLocked = true;
    }
    public void UnlockState()
    {
        stateLocked = false;
    }

    public string GetCurrentAnimation(int track)
    {
        return skeletonAnimation.AnimationState.GetCurrent(track).Animation.Name;
    }


    protected virtual void Update()
    {
        foreach(var e in GameObject.FindGameObjectsWithTag(proximityTag))
        {
            if (Vector2.Distance(e.transform.position, transform.position) < 4)
            {
                PlayProximityToEnemyFace();
            }
        }
    }

}
