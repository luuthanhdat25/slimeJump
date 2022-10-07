using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputPlayer : MonoBehaviour
{
    public static bool Disappear;
    public GameObject CanvaInput;
    public GameObject Warning;
 
    public InputField NamePlayer;
    public InputField IDPlayer;
    public float timeTransition;

    private bool Paused;
    public static string stringA, stringB;
    public Animator anim;
    private void Start()
    {
        CanvaInput.SetActive(true);
    }

    private void Update()
    {
        NamePlayer.text = NamePlayer.text.ToLower();
        IDPlayer.text = IDPlayer.text.ToUpper();
    }

    private void RequestInputAgain()
    {
        NamePlayer.text = null;
        IDPlayer.text = null;
        Warning.SetActive(true);
    }

    public void SaveBeforePlay()
    {
        stringA = NamePlayer.text.Trim(' ');
        stringB = IDPlayer.text.Trim(' ');

        SavingPlayer.Load();

        if (SavingPlayer.CheckExit(stringB))
        {
            RequestInputAgain();
            return;
        }
        anim.SetTrigger("Start");
        Warning.SetActive(false);
        SavingPlayer.RequestAddPalyer(stringA, stringB, 0, 0);
        /* PauseGame();*/
        StartCoroutine(TransitionScene(SceneManager.GetActiveScene().buildIndex + 1));
     
    }

    IEnumerator TransitionScene(int scene)
    {
        yield return new WaitForSeconds(timeTransition);
        SceneManager.LoadScene(scene);
    }

    public void PauseGame()
    {
        if (Paused)
        {
            Paused = false;
            Time.timeScale = 1;
        }
        else
        {
            Paused = true;
            Time.timeScale = 0;
        }
    }
}
