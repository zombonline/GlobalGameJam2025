using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] protected int min, max;
    protected int count;
    public delegate void OnCountChange(int count);
    public event OnCountChange onCountChange;

    [SerializeField] protected TextMeshProUGUI displayText;
    public virtual void IncrementCounter(int value)
    {
        count += value;
        count = Mathf.Clamp(count, min, max);
        UpdateDisplay();
        onCountChange?.Invoke(count);
    }
    public virtual void UpdateCounter(int value)
    {
        count = value;
        count = Mathf.Clamp(count, min, max);
        UpdateDisplay();
        onCountChange?.Invoke(count);
    }

    public void UpdateDisplay()
    {
        if (displayText == null)
        {
            return;
        }
        displayText.text = count.ToString();
    }

    public virtual void SetMin(int value)
    {
        min = value;
        count = Mathf.Clamp(count, min, max);
    }
    public virtual void SetMax(int value)
    {
        max = value;
        count = Mathf.Clamp(count, min, max);
    }

}
