using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerIndicator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerIDText;
    [SerializeField] Image notch;
    [SerializeField] Sprite[] notchSprites;
    private void Awake()
    {
        playerIDText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        playerIDText.text = "P" + (GetComponent<PlayerInput>().playerIndex+1).ToString();
        notch.sprite = notchSprites[GetComponent<PlayerInput>().playerIndex];
    }
}
