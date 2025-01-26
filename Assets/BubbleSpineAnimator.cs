using UnityEngine;
using Spine;
using Spine.Unity;
public class BubbleSpineAnimator : SpineAnimator
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        UpdateState(1, "Idle Floating", true);
    }
    protected override void Update()
    {
        if (rb.linearVelocity.magnitude > 1f) {
            UpdateState(0, "Movement B", false);
        }
        else
        {
            UpdateState(0, "Idle Face B", true);
        }
        base.Update();
    }

    public override void PlayProximityToEnemyFace()
    {
        UpdateState(0, "Worried", false);

    }
}
