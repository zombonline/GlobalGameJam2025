using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LobbyPlayerUI : MonoBehaviour
{
    private PlayerData player;
    [SerializeField] Image teamImage;
    [SerializeField] Sprite bubble, spike;
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] Image readyImage;
    [SerializeField] Sprite cardEnabled, cardDisabled;

    [SerializeField] Image cardImage;
    public void SetPlayer(PlayerData player)
    {
        Debug.Log(player);
        this.player = player;
        cardImage.sprite = cardEnabled;
        teamImage.sprite = (player.team == Team.Bubble) ? bubble : spike;
        playerNameText.text = "Player " + (player.playerIndex + 1).ToString();
        readyImage.enabled = false;
    }
    public void SetReadyDisplay(bool val)
    {
        readyImage.enabled = val;
    }
}
