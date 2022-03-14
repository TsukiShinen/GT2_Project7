using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject _water;

    [SerializeField]
    private GameObject _ice;


    void Start()
    {
        DayNightManager.Instance.EventDay += GridUpdate;
    }

    void GridUpdate(bool IsDay)
    {
        _water.SetActive(IsDay);
        _ice.SetActive(!IsDay);
    }
}
