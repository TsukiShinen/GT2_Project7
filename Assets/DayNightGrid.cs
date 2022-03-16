using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject _water;

    [SerializeField]
    private GameObject _water2;

    [SerializeField]
    private GameObject _ice;


    void Start()
    {
        DayNightManager.Instance.EventDay += GridUpdate;
    }

    void GridUpdate(bool IsDay)
    {
        _water.SetActive(IsDay);
        _water2.SetActive(IsDay);
        _ice.SetActive(!IsDay);
    }
}
