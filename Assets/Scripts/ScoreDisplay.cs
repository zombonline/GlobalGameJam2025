using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBubbleScore, textSpikeScore;

    public void UpdateDisplay()
    {
        textBubbleScore.text = SessionManager.GetWins(Team.Bubble).ToString();
        textSpikeScore.text = SessionManager.GetWins(Team.Spike).ToString();

    }

}
