using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLightOnDay : MonoBehaviour
{
    [SerializeField]
    private Color _dayColor;
    [SerializeField]
    private float _dayIntensity;
    [Space(10)]
    [SerializeField]
    private Color _nightColor;
    [SerializeField]
    private float _nightIntensity;


    private UnityEngine.Rendering.Universal.Light2D _light;

    void Start()
    {
        _light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();

        DayNightManager.Instance.SetLightIntensity += UpdateLight;
    }

    private void UpdateLight(float percentage)
    {
        _light.intensity = Mathf.Lerp(_nightIntensity, _dayIntensity, percentage);
        _light.color = Color.Lerp(_nightColor, _dayColor, percentage);
    }
}
