using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ControllerInput2 : MonoBehaviour
{
    //public SteamVR_Action_Boolean openMenu;

    public Hand hand;
    public Hand otherHand;
    public SteamVR_Action_Boolean TouchPad;
    public SteamVR_Action_Vector2 TouchPos;
    public GameObject player;

    private void OnEnable()
    {
        if (hand == null)
            hand = this.GetComponent<Hand>();
    }

    private void OnDisable()
    {
       
    }
    private void Update()
    {
        bool leftHandTouched = TouchPad.GetState(SteamVR_Input_Sources.LeftHand);
        bool rightHandTouched = TouchPad.GetState(SteamVR_Input_Sources.RightHand);

        //Debug.Log("right: " + rightHandTouched);
        //Debug.Log("left: " + leftHandTouched);
        Vector2 walkDirection = TouchPos.GetAxis(SteamVR_Input_Sources.LeftHand);
        Vector2 rotateDirection = TouchPos.GetAxis(SteamVR_Input_Sources.RightHand);

        Vector2 notTouching = new Vector2(0f, 0f);

        Vector2 currentPlayerPos = player.transform.position;

        if (walkDirection != notTouching || rotateDirection != notTouching)
        {
            if (leftHandTouched)
            {
                Debug.Log(player.transform.forward);
                Debug.Log(player.transform.rotation);
                
                float playerZ = player.transform.position.z + walkDirection.y / 10;
                float playerX = player.transform.position.x + walkDirection.x / 10;
                player.transform.position = new Vector3(playerX, player.transform.position.y, playerZ);

                if (walkDirection.y > 0.2f || walkDirection.y < -0.2f)
                {
                    // Move Forward
                    player.transform.position -= player.transform.forward * Time.deltaTime * (walkDirection.y * 5f);

                    // Adjust height to terrain height at player positin
                    currentPlayerPos = player.transform.position;
                    currentPlayerPos.y = Terrain.activeTerrain.SampleHeight(player.transform.position);
                    player.transform.position = currentPlayerPos;
                }

                //Debug.Log("Left hand");
            }
            if (rightHandTouched)
            {
                //float camX = player.transform.rotation.x + rotateDirection.y / 300;
                float camY = player.transform.rotation.y + rotateDirection.x / 100;
                Debug.Log(camY);
                //Debug.Log(rotateDirection.x);
                if (camY > 0.999f && camY < 1.0f &&rotateDirection.x > 0)
                {
                    camY = -1.0f;
                }
                else if(camY < -0.999f && camY > -1f && rotateDirection.x < 0)
                {
                    camY = 1f;
                }

                player.transform.Rotate(0, camY, 0);
                Quaternion newPlayerRotation = new Quaternion(player.transform.rotation.x, camY, player.transform.rotation.z, player.transform.rotation.w);
                player.transform.rotation = newPlayerRotation;
            }
        }
    }
}
