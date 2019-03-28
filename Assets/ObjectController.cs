using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour
{
    public float durability;
    public float dmg;

    private float maxDurability;
    public Slider healthbar;

    // Start is called before the first frame update
    void Start()
    {
        maxDurability = durability;   
    }

    // Update is called once per frame
    void Update()
    {
        if(durability <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void setDurability(float d)
    {
        durability = d;
    }

    public float getDurability()
    {
        return durability;
    }
    public float getMaxDurability()
    {
        return maxDurability;
    }
    public void setMaxDurability(float d)
    {
        maxDurability = d;
    }
    public void setDmg(float d)
    {
        dmg = d;
    }
    public float getDmg()
    {
        return dmg;
    }
}
