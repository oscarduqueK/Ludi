using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishUnlockDisplay : MonoBehaviour
{
    public Image image;
    public Image imageText;

    void Start()
    {
        int fishId = PlayerPrefs.GetInt("LastUnlockedFish", -1);
        if (fishId == -1) return;

        switch (fishId)
        {
            case 0:
                image.sprite = Resources.Load<Sprite>("UI/fish1");
                imageText.sprite = Resources.Load<Sprite>("UI/text1");
                break;
            case 1:
                image.sprite = Resources.Load<Sprite>("UI/fish2");
                imageText.sprite = Resources.Load<Sprite>("UI/text2");
                break;
            case 2:
                image.sprite = Resources.Load<Sprite>("UI/fish3");
                imageText.sprite = Resources.Load<Sprite>("UI/text3");
                break;
            case 3:
                image.sprite = Resources.Load<Sprite>("UI/fish4");
                imageText.sprite = Resources.Load<Sprite>("UI/text4");
                break;
            default:
                image.sprite = null;
                imageText.sprite = null;
                break;
        }
    }
}
