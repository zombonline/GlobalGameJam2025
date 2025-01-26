using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winText;

    private void Awake()
    {
        if(SessionManager.GetWins(Team.Bubble) >= SessionManager.GetRequiredWins())
        {
            winText.text = "Bubbles Win!";
        }
        else
        {
            winText.text = "Spikes Win!";
        }
    }
}
