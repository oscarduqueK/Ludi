using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishTitleDisplay : MonoBehaviour
{
    public Aquarium aquarium;
    public Image imageText;

    private void Update()
    {
        if (aquarium == null || aquarium.GetCurrentFishData() == null)
            return;

        int fishId = aquarium.GetCurrentFishIndex();

        switch (fishId)
        {
            case 0:
                imageText.sprite = Resources.Load<Sprite>("loUI/lo1");
                break;
            case 1:
                imageText.sprite = Resources.Load<Sprite>("loUI/lo2");
                break;
            case 2:
                imageText.sprite = Resources.Load<Sprite>("loUI/lo3");
                break;
            case 3:
                imageText.sprite = Resources.Load<Sprite>("loUI/lo4");
                break;
            default:
                imageText.sprite = null;
                break;
        }
    }
}
