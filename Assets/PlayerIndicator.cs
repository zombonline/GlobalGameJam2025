using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIndicator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerIDText;

    private void Awake()
    {
        playerIDText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        playerIDText.text = "P" + (GetComponent<PlayerInput>().playerIndex+1).ToString();
    }
}
