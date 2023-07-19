using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    // Variables
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private Quaternion newRotation;

    // References
    private GameObject mainCamera;

    // On start
    private void Start()
    {
        // Expensive call but should suffice for basic prototype
        mainCamera = GameObject.Find("Main Camera");
    }

    // Change Rooms
    public void ChangeRoom()
    {
        mainCamera.transform.position = newPosition;
        mainCamera.transform.rotation = newRotation;
    }
}