using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionWidget : MonoBehaviour
{
    [SerializeField] private GameObject RightTrigger;
    [SerializeField] private GameObject PlantUI;
    
    [SerializeField] private Image emptyImage;
    [SerializeField] private Image pickUpMask;
    [SerializeField] private Image pickedUpMask;
    [SerializeField] private Image throwMask;

    [SerializeField] private Image previewImage;
    [SerializeField] private Image selectedImage;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }   

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(bool isPlantUI)//Sprite previewSprite, sprite selectedSprite)
    {
        gameObject.SetActive(true);
        PlantUI.SetActive(isPlantUI);
        RightTrigger.SetActive(!isPlantUI);

        //this.previewImage.sprite = previewSprite;
        //this.previewImage.sprite = selectedSprite;
        if (previewImage != null) previewImage.gameObject.SetActive(true);
        if (selectedImage != null) selectedImage.gameObject.SetActive(false);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        pickedUpMask.gameObject.SetActive(false);
    }

    public void SetPickedUp()
    {
        pickedUpMask.gameObject.SetActive(true);

        if (previewImage != null) previewImage.gameObject.SetActive(false);
        if (selectedImage != null) selectedImage.gameObject.SetActive(true);
    }

    public void SetPickUpMaskValue(float fillValue)
    {
        pickUpMask.fillAmount = fillValue;
    }

    public void SetThrowMaskValue(float fillValue)
    {
        throwMask.fillAmount = fillValue;
    }
}
