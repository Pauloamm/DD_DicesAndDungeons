using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Essential Components----------------------------------------

    [SerializeField] private GameObject playerCamera;


    // Camera variables--------------------------------------------

    private Camera playerView;
    private float defaultFOV;

    private float yawPlayer, pitchPlayer;

    private PlayerMovement.playerState playerCurrentState;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Gets acess to camera GameObject
        playerCamera = GameObject.Find("Main Camera");

        // Gets player Camera component and default field of view
        playerView = playerCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        playerCurrentState = this.GetComponentInChildren<PlayerMovement>().playerCurrentState;

        cameraManager();
    }

    private void cameraManager()
    {
        // Gets player mouse movement
        float tempPitchPlayer = pitchPlayer - Input.GetAxis("Mouse Y");

        pitchPlayer = Mathf.Clamp(tempPitchPlayer, -89f, 89f);

        yawPlayer += Input.GetAxis("Mouse X");

        // tranforms entire player
        this.transform.eulerAngles = new Vector3(0f, yawPlayer, 0f);


        this.playerCamera.transform.eulerAngles = new Vector3(pitchPlayer, this.transform.eulerAngles.y,
            this.playerCamera.transform.eulerAngles.z);
    }
}