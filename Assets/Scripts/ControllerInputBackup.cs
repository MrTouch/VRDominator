using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ControllerInputBackup : MonoBehaviour
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
                //Debug.Log(player.transform.forward);
                //Debug.Log(player.transform.rotation);
                float playerZ = walkDirection.y / 10;
                float playerX = walkDirection.x / 10;
                Vector3 movement = new Vector3(walkDirection.x / 10, 0, walkDirection.y / 10);
                //get height from terrain
                movement = player.transform.rotation * movement;
                float playerY = Terrain.activeTerrain.SampleHeight(player.transform.position);
                player.transform.position += movement;
                player.transform.position = new Vector3(player.transform.position.x, playerY, player.transform.position.z);

            }
            if (rightHandTouched)
            {

                float camY = rotateDirection.x;
                camY = camY * 0.7f;
                Debug.Log(camY);
                player.transform.Rotate(0, camY, 0);
            }
        }
    }
}
