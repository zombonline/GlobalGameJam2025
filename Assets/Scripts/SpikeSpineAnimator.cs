using UnityEngine;
using Spine;
using Spine.Unity;
public class SpikeSpineAnimator : SpineAnimator
{
    Rigidbody2D rb;
    SpikeMovement spikeMovement;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spikeMovement = GetComponent<SpikeMovement>();
    }

    private void Start()
    {
        UpdateState(1, "Idle Floating", true);
    }
    protected override void Update()
    {
        if (rb.linearVelocity.magnitude > 1f)
        {
            UpdateState(0, "Movement S", false);
        }
        else if (spikeMovement.GetMoveChargeValue() > 0)
        {
            if(GetCurrentAnimation(0) == "ChargeSpike S") { return; }
            UpdateState(0, "ChargeSpike S", false);
        }
        else
        {
            UpdateState(0, "Idle Face S", true);
        }
        base.Update();
    }

    public override void PlayProximityToEnemyFace()
    {
        UpdateState(0, "Excited to Pop S", false);

    }
}
