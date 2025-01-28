using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerIndicator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerIDText;
    [SerializeField] Image notchImage;
    [SerializeField] Sprite[] notchSprites;
    private void Start()
    {
        playerIDText.text = "P" + (GetComponent<PlayerInput>().playerIndex+1).ToString();
        notchImage.sprite = notchSprites[GetComponent<PlayerInput>().playerIndex];
    }
}
