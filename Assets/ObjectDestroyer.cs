using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public ObjectController oc;
    // Start is called before the first frame update
    void Start()
    {
        oc = GetComponent<ObjectController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(oc.durability <= 0)
        {
            Destroy(gameObject);
        }
    }
}
