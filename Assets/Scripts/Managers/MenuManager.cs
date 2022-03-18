using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.Play("MainMenuMusic");
    }

    public void Play()
    {
        StartCoroutine(PlayButton());
    }

    public void Quit()
    {
        StartCoroutine(QuitButton());
    }

    private IEnumerator PlayButton()
    {
        AudioManager.Instance.Stop("MainMenuMusic");
        AudioManager.Instance.Play("Click");
        AudioManager.Instance.Play("StartMusic");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator QuitButton()
    {
        AudioManager.Instance.Play("Click");
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
}
