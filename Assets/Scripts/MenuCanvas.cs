using Spine.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] SkeletonGraphic menuScreenGraphic;
    [SerializeField] RectTransform buttons, buttonsTarget;
    private IEnumerator Start()
    {
        SessionManager.ClearPlayers();
        SessionManager.ClearWins();
        yield return new WaitForSeconds(2f);
        FMODController.InitiateMusic();
        menuScreenGraphic.AnimationState.SetAnimation(1, "Opening", false);
        yield return new WaitForSeconds(1f);
        LeanTween.moveY(buttons, buttonsTarget.localPosition.y, 1f).setEase(LeanTweenType.easeOutBounce);
    }

}
