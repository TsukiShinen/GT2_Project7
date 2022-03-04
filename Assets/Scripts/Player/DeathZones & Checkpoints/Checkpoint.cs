using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Debug.Log("check");
        collision.GetComponent<Player>().LastCheckPoint = this;
        gameObject.SetActive(false);

    }
}
