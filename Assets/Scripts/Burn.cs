using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Burn : MonoBehaviour
{
    BoxCollider2D _box;
    GameObject _HeatWave;

    private void Awake()
    {
        _box = GetComponent<BoxCollider2D>();
        _HeatWave = transform.GetChild(0).gameObject;

        DayNightManager.Instance.EventDay += Activate;
    }

    private void Activate(bool isDay)
    {
        _box.enabled = isDay;
        _HeatWave.SetActive(isDay);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == null) { return; }
        if (!collision.CompareTag("Player")) { return; }

        collision.GetComponent<Entity>().Burn();
    }
}
