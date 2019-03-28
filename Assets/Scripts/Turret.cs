using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
    public Transform BulletSpawnPoint;
    public float shootDistance = 22.0f;
    public float bulletDeltaTime =3.0f;
    float timer;
    public ParticleSystem ps;
    private Vector3 turretPos;
    ObjectController oc;
    Transform rotatableElement;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("CustomPlayer");
        turretPos = transform.position;
        timer = 0;
        oc = gameObject.GetComponent<ObjectController>();
        rotatableElement = transform.Find("Rotatable").transform;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 dVec = turretPos - player.transform.position;
        float distance = dVec.magnitude;

        //Debug.Log(player.transform.position);
        if (distance < shootDistance)
        {
            rotatableElement.transform.LookAt(player.transform);
            if (timer > bulletDeltaTime)
            {
                Debug.Log("shoot");
                ps.Play();
                Instantiate(bullet, BulletSpawnPoint.position, Quaternion.identity);
                timer = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //currently useless if...??
        if(collision.gameObject.tag == "Projectile")
        {
            ObjectController toc = collision.gameObject.GetComponent<ObjectController>();
            toc.setDurability(toc.getDurability() - oc.getDmg());
            oc.setDurability(oc.getDurability() - toc.getDmg());
            oc.healthbar.value = oc.getDurability() / oc.getMaxDurability();
        }
        else if(collision.gameObject.tag == "interactableElement")
        {
            ObjectController toc = collision.gameObject.GetComponent<ObjectController>();
            toc.setDurability(toc.getDurability() - oc.getDmg());
            oc.setDurability(oc.getDurability() - toc.getDmg());
            oc.healthbar.value = oc.getDurability() / oc.getMaxDurability();
        }
    }


}
