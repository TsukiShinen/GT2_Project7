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

    [SerializeField] GameObject coin1;
    [SerializeField] GameObject coin2;
    [SerializeField] GameObject coin3;

    [SerializeField]
    private Animator _transition;

    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _virtualCameraNoise;



    void Start()
    {
        _virtualCameraNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _deadEnemy = new GameObject("Dead Enemy");
        _deadEnemy.transform.SetParent(transform);
    }

    #region Checkpoint and enemy
    private Transform _checkpoint;
    private GameObject _deadEnemy;
    public void RegisterCheckpoint(Transform checkpoint)
    {
        _checkpoint = checkpoint;
        for (int i = 0; i < _deadEnemy.transform.childCount; i++)
        {
            Destroy(_deadEnemy.transform.GetChild(i).gameObject);
        }
    }

    public IEnumerator LoadLastCheckPoint()
    {
        _transition.SetTrigger("Start");
        if (_checkpoint == null) { Reload(); StopCoroutine(LoadLastCheckPoint()); }
        for (int i = 0; i < _deadEnemy.transform.childCount; i++)
        {
            GameObject enemy = _deadEnemy.transform.GetChild(i).gameObject;
            enemy.SetActive(true);
            enemy.transform.SetParent(null);
            enemy.GetComponent<Enemy>().Respawn();
        }

        yield return new WaitForSeconds(1f);
        _transition.SetTrigger("End");
        FindObjectOfType<Player>().Respawn(_checkpoint);
    }

    public void AddDeadEnemy(GameObject enemy)
    {
        enemy.transform.SetParent(_deadEnemy.transform);
        enemy.SetActive(false);
    }
    #endregion

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
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
