using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerMenu : MonoBehaviour
{
    public GameObject InputBox;
    public AudioSource Click;

    public void PlayGame()
    {
        Click.Play();
       /* SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);*/
    }
    public void BackToMenu()
    {
        InputBox.SetActive(false);
    }
    public void PlayAudio() 
    {
        Click.Play();
    }

    public void Rank()
    {
        SavingPlayer.Load();
        Click.Play();
        /*CanvaEdit AppearRankTable = new CanvaEdit();
        AppearRankTable.gameObject.SetActive(true);*/
    }

    public void QuitGame()
    {
        Click.Play();
        Application.Quit();
    }
}