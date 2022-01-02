using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMover : MonoBehaviour
{
    public float carSpeed = 15.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * carSpeed * Time.deltaTime);
    }
}
