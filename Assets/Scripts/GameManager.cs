using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(this);
        }
    }
    #endregion

    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _virtualCameraNoise;

    void Start()
    {
        _virtualCameraNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator ShakeCamera(float durartion, float amplitude, float frequency)
    {
        float counter = 0f;
        while (counter < durartion)
        {
            _virtualCameraNoise.m_AmplitudeGain = amplitude;
            _virtualCameraNoise.m_FrequencyGain = frequency;
            counter += Time.deltaTime;

            yield return null;
        }

        _virtualCameraNoise.m_AmplitudeGain = 0f;
        _virtualCameraNoise.m_FrequencyGain = 0f;

    }
}
