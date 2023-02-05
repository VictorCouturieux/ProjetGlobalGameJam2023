using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegiesAnimEvent : MonoBehaviour
{
    [SerializeField] private Plant _plant;
    public ParticleSystem flies;
    public bool isDecaying;
    private ParticleSystem.EmissionModule emission;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        emission = flies.emission;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDecaying) emission.rateOverTime = speed*Time.deltaTime; 
    }

    public void DestroyThisVeggie() 
    {
        _plant.DestroyPlant();
    }

    public void StartToDecay() 
    {
        isDecaying = true;
    }

    public void SoundPlantApparitionEvent()
    {
        AudioManager.Instance.PousseApparition(gameObject);
        Debug.Log("sonnnnn");
    }

}
