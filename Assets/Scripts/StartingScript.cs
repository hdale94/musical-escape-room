using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScript : MonoBehaviour
{
    public GameObject StartTogether;
    FMOD.Studio.EventInstance FMOD_Intro_Speech;

    void Start()
    {
        FMOD_Intro_Speech = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Intro");
    }
    public void StartButton()
    {
        StartCoroutine(StartGame());
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    IEnumerator StartGame()
    {
        StartTogether.SetActive(false);
        FMOD_Intro_Speech.start();
        yield return new WaitForSeconds(14f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

