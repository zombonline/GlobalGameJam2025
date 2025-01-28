using TMPro;
using Unity.VisualScripting;
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

    private void Awake()
    {
        if(teamImage.sprite == null)
        {
            teamImage.enabled = false;
        }
    }
    public void SetPlayer(PlayerData player)
    {
        teamImage.enabled = true;
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
