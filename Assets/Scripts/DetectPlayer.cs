using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DetectPlayer : MonoBehaviour
{
    public bool IsPlayerDetected { get; private set; }

    private void Start()
    {
        IsPlayerDetected = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { IsPlayerDetected = false;  return; }

        IsPlayerDetected = true;
    }
}
