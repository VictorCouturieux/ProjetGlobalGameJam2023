using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegiesAnimEvent : MonoBehaviour
{
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
        if (transform.parent != null) 
        {
            Destroy(transform.parent.gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void StartToDecay() 
    {
        isDecaying = true;
    }
}
