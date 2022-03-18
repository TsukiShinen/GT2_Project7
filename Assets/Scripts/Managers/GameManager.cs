using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
            Load();
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(this);
        }
    }
    #endregion

    public bool isCoin1Active = false;
    public bool isCoin2Active = false;
    public bool isCoin3Active = false;

    [SerializeField] private Slider finalBossLife;

    [SerializeField] private Animator _transition;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _virtualCameraNoise;

    private void Start()
    {
        AudioManager.Instance.Play("OverworldMusic");
    }

    private void Update()
    {
        if(!(finalBossLife.value <= 0f)) { return; }
        StartCoroutine(Win());
    }

    public void SetCoinTaken(GameObject coin)
    {
        switch(coin.name)
        {
            case "StarCoin 1":
                isCoin1Active = true;
                break;
            case "StarCoin 2":
                isCoin2Active = true;
                break;
            case "StarCoin 3":
                isCoin3Active = true;
                break;
        }
    }

    private IEnumerator Win()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Load()
    {
        _virtualCameraNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _deadEnemies = new GameObject("Dead Enemy");
        AliveEnemies = new GameObject("Alive Enemy");
        _deadEnemies.transform.SetParent(transform);
        AliveEnemies.transform.SetParent(transform);
    }

    #region Checkpoint and enemy
    private Transform _checkpoint;
    private GameObject _deadEnemies;
    public GameObject AliveEnemies;

    public void RegisterCheckpoint(Transform checkpoint)
    {
        _checkpoint = checkpoint;
        for (int i = 0; i < _deadEnemies.transform.childCount; i++)
        {
            Destroy(_deadEnemies.transform.GetChild(i).gameObject);
        }
    }

    public void RegisterEnemy(GameObject enemy)
    {
        enemy.transform.SetParent(AliveEnemies.transform, false);
    }

    public IEnumerator LoadLastCheckPoint()
    {
        // Stop Shake if u die during the dash
        _virtualCameraNoise.m_AmplitudeGain = 0f;
        _virtualCameraNoise.m_FrequencyGain = 0f;

        _transition.SetTrigger("Start");
        if (_checkpoint == null) { Reload(); StopCoroutine(LoadLastCheckPoint()); }
        for (int i = 0; i < _deadEnemies.transform.childCount; i++)
        {
            GameObject enemy = _deadEnemies.transform.GetChild(i).gameObject;
            enemy.SetActive(true);
            enemy.transform.SetParent(AliveEnemies.transform);
            enemy.GetComponent<Enemy>().Respawn();
        }
        for (int i = 0; i < AliveEnemies.transform.childCount; i++)
        {
            GameObject enemy = AliveEnemies.transform.GetChild(i).gameObject;
            enemy.GetComponent<Enemy>().Respawn();
        }

        yield return new WaitForSeconds(1f);
        _transition.SetTrigger("End");
        FindObjectOfType<Player>().Respawn(_checkpoint);
    }

    public void AddDeadEnemy(GameObject enemy)
    {
        enemy.transform.SetParent(_deadEnemies.transform);
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
