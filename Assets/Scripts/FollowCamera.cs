using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private GameObject playerGameobject;
    private Vector3 startCameraPosition;
    private void Awake()
    {
        playerGameobject = GameObject.Find("CarVehicle");
    }
    // Start is called before the first frame update
    void Start()
    {
        startCameraPosition = transform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = startCameraPosition + playerGameobject.transform.position;
    }
}
