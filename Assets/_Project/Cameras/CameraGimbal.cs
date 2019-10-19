using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraGimbal : MonoBehaviour 
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 gimbalRotation;
    private GameObject player;
    private GameObject cam;

    // Use this for initialization
    void Start() 
	{
        gimbalRotation = transform.rotation.eulerAngles;
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GetComponentInChildren<Camera>().gameObject;
    }


    void LateUpdate() 
	{
        // Set Gimbal position to player position.
        this.transform.position = player.transform.position;

        // Rotate the entire gimbal around its transform (player position).
        // TODO: Fix camera flipping on highest point. Maybe constrain rotation?
        gimbalRotation.y += CrossPlatformInputManager.GetAxis("Mouse X") * moveSpeed;
        gimbalRotation.x += CrossPlatformInputManager.GetAxis("Mouse Y") * moveSpeed;
        transform.rotation = Quaternion.Euler(gimbalRotation);

        cam.transform.LookAt(this.transform.position);
    }
}
