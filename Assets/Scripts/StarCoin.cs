using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCoin : MonoBehaviour
{
    public bool IsTaken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(Despawn());
        }
    }

    private IEnumerator Despawn()
    {
        gameObject.GetComponent<Animator>().speed *= 8;
        AudioManager.Instance.Play("StarCoin");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        IsTaken = true;
    }
}
