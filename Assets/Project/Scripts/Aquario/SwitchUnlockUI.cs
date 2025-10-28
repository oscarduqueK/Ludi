using UnityEngine;
using TMPro;
using System.Collections;

public class FishInfoPanel : MonoBehaviour
{
    public Aquarium aquarium;
    public TextMeshProUGUI infoText;
    public Transform fishContainer;
    public float rotationSpeed = 50f;

    private GameObject currentFishInstance;

    public void Learn()
    {
        if (currentFishInstance != null)
            Destroy(currentFishInstance);

        FishData data = aquarium != null ? aquarium.GetCurrentFishData() : null;

        if (data == null || !data.unlocked)
        {
            infoText.text = "";
            return;
        }

        if (data.prefab != null)
        {
            currentFishInstance = Instantiate(data.prefab, fishContainer.position, Quaternion.identity, fishContainer);
            Fish fishComp = currentFishInstance.GetComponent<Fish>();
            if (fishComp != null)
                infoText.text = fishComp.GetInfoString();
            else
                infoText.text = $"<b>{data.fishName}</b>\n\n(Info no disponible)";
        }
        else
        {
            infoText.text = $"<b>{data.fishName}</b>\n\n(Info no disponible)";
        }

        infoText.transform.SetParent(fishContainer, true);
    }

    public void View()
    {
        infoText.text = "";
        FishData data = aquarium != null ? aquarium.GetCurrentFishData() : null;

        if (data == null || !data.unlocked || data.prefab == null)
        {
            if (currentFishInstance != null)
            {
                Destroy(currentFishInstance);
                currentFishInstance = null;
            }
            return;
        }

        if (currentFishInstance != null)
            Destroy(currentFishInstance);

        currentFishInstance = Instantiate(data.prefab, fishContainer.position, Quaternion.identity, fishContainer);
        Fish fishComp = currentFishInstance.GetComponent<Fish>();
        if (fishComp != null)
            //fishComp.Initialize(FishData data);
            fishComp.OnSpawn();
    }

    //void Update()
    //{
    //    if (currentFishInstance != null)
    //    {
    //        fishContainer.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    //    }
    //}
}
