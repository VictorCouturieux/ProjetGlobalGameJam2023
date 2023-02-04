using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantDies : MonoBehaviour
{
    public Collider myCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyThis() 
    {
        Destroy(gameObject);
    }

    public void ImCollectable() 
    {
        myCollider.enabled = true;
    }
}
