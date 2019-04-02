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
    public ParticleSystem psDestroyed;
    private Vector3 turretPos;
    public ObjectController oc;
    Transform rotatableElement;
    public AudioSource shoot;
    public AudioSource explode;

    public Canvas win;
    public GameController gc;
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
            Vector3 position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            
            rotatableElement.transform.LookAt(position);
            if (timer > bulletDeltaTime)
            {
                Debug.Log("shoot");
                shoot.Play();
                ps.Play();
                Instantiate(bullet, BulletSpawnPoint.position, Quaternion.identity);
                timer = 0;
            }
        }
        if (gameObject.GetComponent<ObjectController>()) {
            if (gameObject.GetComponent<ObjectController>().getDurability() <= 0)
            {
                Debug.Log("Destroy turret");
                if(gameObject.transform.name == "BigPyramid")
                {
                    win.enabled = true;
                    gc.reloadScene();
                }
                explode.Play();
                psDestroyed.Play();
                Destroy(gameObject);
            
                
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
