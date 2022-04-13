using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.red;
    }
}
