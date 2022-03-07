using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    [SerializeField]
    private float _knockback = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) { return; }
        if (!collision.CompareTag("Player")) { return; }

        collision.GetComponent<Player>().Hit((collision.transform.position - transform.position).normalized * _knockback);
    }
}
