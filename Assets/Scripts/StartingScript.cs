using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScript : MonoBehaviour
{
    //Text object to get deactivated before intro speech
    public GameObject StartTogether;

    //FMOD
    FMOD.Studio.EventInstance FMOD_Intro_Speech;

    void Start()
    {
        //FMOD
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
        yield return new WaitForSeconds(1f); //Changed from 14f to 1f due to fmod bug in web build
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

