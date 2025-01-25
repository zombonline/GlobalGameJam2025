using UnityEngine;
using UnityEngine.UI;

public class WinCounter : Counter
{
    [SerializeField] private Image[] winCounterImages;

    [SerializeField] Sprite offImage, onImage;

    override public void IncrementCounter(int value)
    {
        base.IncrementCounter(value);
        for(int i = 0; i < winCounterImages.Length; i++)
        {
            if (i < count)
            {
                winCounterImages[i].sprite = onImage;
            }
            else
            {
                winCounterImages[i].sprite = offImage;
            }
        }
    }

    override public void UpdateCounter(int value)
    {
        base.UpdateCounter(value);
        for (int i = 0; i < winCounterImages.Length; i++)
        {
            if (i < count)
            {
                winCounterImages[i].sprite = onImage;
            }
            else
            {
                winCounterImages[i].sprite = offImage;
            }
        }
    }

    public override void SetMax(int value)
    {
        base.SetMax(value);
        if(max > winCounterImages.Length)
        {
            foreach (var image in winCounterImages)
            {
                image.gameObject.SetActive(false);
            }
            displayText.enabled = true;
            return;
        }
        for (int i = 0; i < winCounterImages.Length; i++)
        {
            if(i >= max)
            {
                winCounterImages[i].gameObject.SetActive(false);
            }
            else
            {
                winCounterImages[i].gameObject.SetActive(true);
            }
        }
        displayText.enabled = false;
    }

}
