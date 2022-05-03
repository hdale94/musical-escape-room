using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylScript : MonoBehaviour
{
    //Play-button
    public GameObject vinylButton;

    //Object to attach audio spatialization
    public GameObject vinylBase;

    //Vinyl Player
    public static FMOD.Studio.EventInstance FMOD_vinylPlay;

    void Start()
    {
        //FMOD
        FMOD_vinylPlay = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Sequencer/Solution");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_vinylPlay, vinylBase.transform, vinylBase.GetComponent<Rigidbody>());
    }

    public void PlayVinyl()
    {
        FMOD_vinylPlay.start();
    }
}
