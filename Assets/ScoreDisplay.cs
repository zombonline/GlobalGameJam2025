using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBubbleScore, textSpikeScore;

    public void UpdateDisplay()
    {
        textBubbleScore.text = SessionManager.GetBubbleWins().ToString();
        textSpikeScore.text = SessionManager.GetSpikeWins().ToString();

    }

}
