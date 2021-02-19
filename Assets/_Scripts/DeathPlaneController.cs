using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            var controller = other.gameObject.GetComponent<CharacterController>();

            controller.enabled = false;

            other.transform.position = spawnPoint.position;

            controller.enabled = true;
        }
    }
}
