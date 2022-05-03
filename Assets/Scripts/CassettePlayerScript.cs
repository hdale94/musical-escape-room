using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassettePlayerScript : MonoBehaviour
{
    //Get main script
    public CharacterMainScript mainScript;

    //Cassette-Player Objects
    public GameObject objCassettePlayer;

    //FMOD
    FMOD.Studio.EventInstance FMOD_Play_Forward;
    FMOD.Studio.EventInstance FMOD_Play_Backward;
    FMOD.Studio.EventInstance FMOD_Cassette_Button;
    FMOD.Studio.EventInstance FMOD_Cassette_Click;
    FMOD.Studio.EventInstance FMOD_Static_Loop;

    void Start()
    {
        //FMOD-events
        FMOD_Play_Forward = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/CassettePlayer/CassettePlay");
        FMOD_Play_Backward = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/CassettePlayer/CassetteReverse");
        FMOD_Cassette_Button = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/CassettePlayer/CassetteButton");
        FMOD_Cassette_Click = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/CassettePlayer/Click");
        FMOD_Static_Loop = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/CassettePlayer/StaticNoise");

        //Attach event to objects
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Play_Forward, objCassettePlayer.transform, objCassettePlayer.GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Play_Backward, objCassettePlayer.transform, objCassettePlayer.GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Static_Loop, objCassettePlayer.transform, objCassettePlayer.GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Cassette_Click, objCassettePlayer.transform, objCassettePlayer.GetComponent<Rigidbody>());

        //Start static noise event
        FMOD_Static_Loop.start();
    }

    void Update()
    {
        //Stop static noise event if player uses key
        if (CharacterMainScript.activateKey == true)
        {
            FMOD_Static_Loop.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    //Method for playing cassette forward
    public void PlayCassetteFwd()
    {
        FMOD_Cassette_Click.start();

        if (CharacterMainScript.activateKey == true)
        {
            FMOD_Play_Backward.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            FMOD_Play_Forward.start();
        }
    }

    //Method for playing cassette backward
    public void PlayCassetteBack()
    {
        FMOD_Cassette_Click.start();

        if (CharacterMainScript.activateKey == true)
        {
            FMOD_Play_Forward.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            FMOD_Play_Backward.start();
        }
    }
}
