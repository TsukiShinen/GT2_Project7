using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private GameObject _door;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Jar")) { return; }

        _door = GameObject.FindGameObjectWithTag("Door");
        _door.SetActive(false);
        gameObject.SetActive(false);
    }
}
