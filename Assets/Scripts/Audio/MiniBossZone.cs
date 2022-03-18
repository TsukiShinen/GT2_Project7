using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniBossZone : MonoBehaviour
{
    [SerializeField] private Slider _miniBossLife;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!AudioManager.Instance.isPlaying("BossMusic") && _miniBossLife.value > 0)
            {
                AudioManager.Instance.Pause("OverworldMusic");
                AudioManager.Instance.PlayMusic("BossMusic");
            }
        }
    }
}
