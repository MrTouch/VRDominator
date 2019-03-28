using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleController : MonoBehaviour
{
    ObjectController oc;
    public ParticleSystem ps;
    public ParticleSystem ps2;
    public ParticleSystem ps3;
    public ParticleSystem ps4;
    public ParticleSystem ps5;
    public ParticleSystem ps6;
    public ParticleSystem ps7;
    public ParticleSystem ps8;
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
            ps.Play();
            ps2.Play();
            ps3.Play();
            ps4.Play();
            ps5.Play();
            ps6.Play();
            ps7.Play();
            ps8.Play();
            Destroy(gameObject);
            Debug.Log("Level End");
        }
    }
}
