using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public GameObject target;
    public float speed = 3.00f;
    private Vector3 flyDirection;
    private bool gravityOn = false;
    private Rigidbody rb;
    private bool firstTimePhysics = false;
    Component interactableScript;
    ObjectController oc;
    bool isgrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("CustomPlayer");
        transform.LookAt(target.transform.position);
        flyDirection = target.transform.position - transform.position;
        flyDirection.y += 1f;
        flyDirection = Vector3.Normalize(flyDirection);
        Debug.Log(flyDirection);
        rb = gameObject.GetComponent<Rigidbody>();
        oc = gameObject.GetComponent<ObjectController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!gravityOn)
        {
            transform.position += flyDirection * speed * Time.deltaTime;
            transform.Rotate(new Vector3(3, 2, 1));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("bulletcol");

        if(collision.gameObject.name == "Terrain")
        {
            Debug.Log("Collision with terrain");
            if(!firstTimePhysics)
            {
                rb.useGravity = true;
                rb.AddForce(flyDirection * 500);
                gravityOn = true;
                firstTimePhysics = true;
                oc.setDurability(oc.getDurability() - 5f);
            }
            isgrounded = true;
        }
        else
        {

            Debug.Log("collision with any other Element");

            rb.useGravity = true;
            gravityOn = true;
            firstTimePhysics = true;
            ObjectController toc = collision.gameObject.GetComponent<ObjectController>();
            toc.setDurability(toc.getDurability() - oc.getDmg());
            oc.setDurability(oc.getDurability() - toc.getDmg());
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.name == "Terrain")
        {
            Debug.Log("Collision exit terrain");
            isgrounded = false;
        }
    }
    public void setFirstTimePhysics()
    {
        rb.useGravity = true;
        gravityOn = true;
        firstTimePhysics = true;
    }
}
