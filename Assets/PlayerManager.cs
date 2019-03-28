using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float hp = 100.0f;
    ObjectController oc;

    // Start is called before the first frame update
    void Start()
    {
        oc = gameObject.GetComponent<ObjectController>();
        oc.setMaxDurability(hp);
        oc.setDurability(hp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("collision bro");
        if(col.collider.tag == "Projectile")
        {
            Debug.Log("Bullet hit me!");
            Debug.Log("lower hp");
            hp -= col.gameObject.GetComponent<ObjectController>().getDmg();
            oc.setDurability(hp);
            oc.healthbar.value = hp / oc.getMaxDurability();
            Destroy(col.collider.gameObject);
        }
        else
        {
            Debug.Log("something else is colliding with me");
        }
    }
}
