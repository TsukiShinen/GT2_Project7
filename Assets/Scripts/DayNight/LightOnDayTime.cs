using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightOnDayTime : MonoBehaviour
{
    [SerializeField]
    private bool _isNightLight;

    private float _minIntensity;
    private float _maxIntensity;

    private Light2D _light;

    void Start()
    {
        _light = GetComponent<Light2D>();
        _minIntensity = _isNightLight ? _light.intensity : 0f;
        _maxIntensity = _isNightLight ? 0f : _light.intensity;

        DayNightManager.Instance.SetLightIntensity += UpdateLight;
    }

    private void UpdateLight(float percentage)
    {
        _light.intensity = Mathf.Lerp(_minIntensity, _maxIntensity, percentage);
    }
}
