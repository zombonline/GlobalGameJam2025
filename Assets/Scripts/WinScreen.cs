using Spine.Unity;
using System.Collections;
using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] SkeletonGraphic winnerGraphic;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        if (SessionManager.GetWins(Team.Bubble) >= SessionManager.GetRequiredWins())
        {
            winnerGraphic.Skeleton.SetSkin("Bubble Wins");
        }
        else
        {
            winnerGraphic.Skeleton.SetSkin("Spike Wins");
        }
        winnerGraphic.freeze = false;
        winnerGraphic.AnimationState.SetAnimation(0, "animation", false);
    }
}
