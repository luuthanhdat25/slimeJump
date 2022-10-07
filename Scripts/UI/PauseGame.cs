using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public AudioSource Click;
    // Start is called before the first frame update
    public void Resume()
    {
        Click.Play();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void PlayAgain()
    {
        Click.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        Click.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1;
    }
}
