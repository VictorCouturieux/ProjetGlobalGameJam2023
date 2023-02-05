using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionWidget : MonoBehaviour
{
    [SerializeField] private Image emptyImage;
    [SerializeField] private Image pickUpMask;
    [SerializeField] private Image pickedUpMask;
    [SerializeField] private Image throwMask;

    [SerializeField] private Image previewIcon;
    [SerializeField] private Image selectedIcon;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }   

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
        previewIcon.gameObject.SetActive(true);
        selectedIcon.gameObject.SetActive(false);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        pickedUpMask.gameObject.SetActive(false);
    }

    public void SetPickedUp()
    {
        pickedUpMask.gameObject.SetActive(true);

        previewIcon.gameObject.SetActive(false);
        selectedIcon.gameObject.SetActive(true);
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
