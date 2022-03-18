using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuManager : MonoBehaviour
{
    [SerializeField] GameObject coin1;
    [SerializeField] GameObject coin2;
    [SerializeField] GameObject coin3;

    private void Start()
    {
        AudioManager.Instance.Play("EndMusic");
        ShowCoins();
    }

    public void Replay()
    {
        StartCoroutine(ReplayButton());
    }

    public void Quit()
    {
        StartCoroutine(QuitButton());
    }

    private IEnumerator ReplayButton()
    {
        AudioManager.Instance.Stop("EndMusic");
        AudioManager.Instance.Play("Click");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private IEnumerator QuitButton()
    {
        AudioManager.Instance.Play("Click");
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    private void ShowCoins()
    {
        coin1.SetActive(GameManager.Instance.isCoin1Active);
        coin2.SetActive(GameManager.Instance.isCoin2Active);
        coin3.SetActive(GameManager.Instance.isCoin3Active);
    }
}
