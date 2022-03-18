using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class DetectPlayer : MonoBehaviour
{
    public bool IsPlayerDetected { get; private set; }
    public Vector3 PlayerPosition { get;private set; }

    private void Start()
    {
        IsPlayerDetected = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { return; }

        IsPlayerDetected = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { return; }

        PlayerPosition = collision.transform.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { return; }

        IsPlayerDetected = false;
    }
}
