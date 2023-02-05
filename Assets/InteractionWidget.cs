using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionWidget : MonoBehaviour
{
    [SerializeField] private Image emptyImage;
    [SerializeField] private Image pickUpMask;
    [SerializeField] private Image throwMask;

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
    }

    public void Hide()
    {
        gameObject.SetActive(false);
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
