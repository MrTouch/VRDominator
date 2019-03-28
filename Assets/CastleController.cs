using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleController : MonoBehaviour
{
    ObjectController oc;
    // Start is called before the first frame update
    void Start()
    {
        oc = GetComponent<ObjectController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        oc.setDurability(oc.getDurability() - collision.gameObject.GetComponent<ObjectController>().getDmg());
        oc.healthbar.value = oc.getDurability() / oc.getMaxDurability();
        if(oc.getDurability() <= 0)
        {
            Destroy(gameObject);
            //Particle explosion here
            Debug.Log("Level End");
        }
    }
}
