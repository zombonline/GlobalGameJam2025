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

    Image cardImage;

    private void Awake()
    {
        cardImage = GetComponent<Image>();
    }

    public void SetPlayer(PlayerData player)
    {
        this.player = player;
        cardImage.sprite = cardEnabled;
        teamImage.sprite = (player.team == Team.Bubble) ? bubble : spike;
        playerNameText.text = "Player " + (player.playerIndex + 1).ToString();
    }
    public void SetReadyDisplay(bool ready)
    {
        readyImage.enabled = ready;
    }
}
