using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBillboard : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;

    // Update is called once per frame

    void Update()
    {
        transform.forward = -mainCamera.transform.forward;
        var newRot = new Vector3(0f, transform.rotation.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Euler(newRot);

        //float _camYrotation = mainCamera.transform.rotation.y;
        //float _angleDifference = _camYrotation - gameObject.transform.rotation.y;

        //gameObject.transform.Rotate(0.0f, _angleDifference, 0.0f, Space.World);
        //Debug.Log("Cam Y rotation: " + _camYrotation);
    }
}
