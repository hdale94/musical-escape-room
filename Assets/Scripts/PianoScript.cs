using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoScript : MonoBehaviour
{
    //Get main script
    public CharacterMainScript mainScript;

    //Get Piano GameObject for audio spatialization
    public GameObject objPiano;

    //Piano-lights
    public GameObject lightPiano1;
    public GameObject lightPiano2;
    public GameObject lightPiano3;
    public GameObject lightPiano4;

    //Create arrays
    private string[] arrayRaycastHit;
    private string[] arrayCorrect;
    
    //Integer for switch-case
    private int switchPianoInt = 0;

    //Colors used to visually confirm if note was correct
    private Color colorGreen = Color.green;
    private Color colorWhite = Color.white;
    private Color colorGrey = Color.grey;

    //Bool that triggers when puzzle is solved
    public static bool solvedPiano = false;

    //FMOD-events for piano keys
    public FMOD.Studio.EventInstance FMOD_C;
    public FMOD.Studio.EventInstance FMOD_Csharp;
    public FMOD.Studio.EventInstance FMOD_D;
    public FMOD.Studio.EventInstance FMOD_Dsharp;
    public FMOD.Studio.EventInstance FMOD_E;
    public FMOD.Studio.EventInstance FMOD_F;
    public FMOD.Studio.EventInstance FMOD_Fsharp;
    public FMOD.Studio.EventInstance FMOD_G;
    public FMOD.Studio.EventInstance FMOD_Gsharp;
    public FMOD.Studio.EventInstance FMOD_A;
    public FMOD.Studio.EventInstance FMOD_Asharp;
    public FMOD.Studio.EventInstance FMOD_B;

    void Start()
    {
        //Define length and content of arrays
        arrayCorrect = new string[] { "Key_C", "Key_B", "Key_E", "Key_G" };
        arrayRaycastHit = new string[4];

        //FMOD event and attaching to objPiano
        FMOD_C = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Piano/C");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_C, objPiano.transform, objPiano.GetComponent<Rigidbody>());

        FMOD_Csharp = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Piano/C#");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Csharp, objPiano.transform, objPiano.GetComponent<Rigidbody>());

        FMOD_D = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Piano/D");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_D, objPiano.transform, objPiano.GetComponent<Rigidbody>());

        FMOD_Dsharp = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Piano/D#");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Dsharp, objPiano.transform, objPiano.GetComponent<Rigidbody>());

        FMOD_E = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Piano/E");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_E, objPiano.transform, objPiano.GetComponent<Rigidbody>());

        FMOD_F = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Piano/F");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_F, objPiano.transform, objPiano.GetComponent<Rigidbody>());

        FMOD_Fsharp = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Piano/F#");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Fsharp, objPiano.transform, objPiano.GetComponent<Rigidbody>());

        FMOD_G = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Piano/G");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_G, objPiano.transform, objPiano.GetComponent<Rigidbody>());

        FMOD_Gsharp = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Piano/G#");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Gsharp, objPiano.transform, objPiano.GetComponent<Rigidbody>());

        FMOD_A = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Piano/A");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_A, objPiano.transform, objPiano.GetComponent<Rigidbody>());

        FMOD_Asharp = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Piano/A#");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Asharp, objPiano.transform, objPiano.GetComponent<Rigidbody>());

        FMOD_B = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Piano/B");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_B, objPiano.transform, objPiano.GetComponent<Rigidbody>());
    }

    //Method for checking and triggering audio for correct key
    public void PianoKeys()
    {
        if (CharacterMainScript.raycastHit.collider.name == "Key_C")
        {
            FMOD_C.start();
        }
        else if (CharacterMainScript.raycastHit.collider.name == "Key_C#")
        {
            FMOD_Csharp.start();
        }
        else if (CharacterMainScript.raycastHit.collider.name == "Key_D")
        {
            FMOD_D.start();
        }
        else if (CharacterMainScript.raycastHit.collider.name == "Key_D#")
        {
            FMOD_Dsharp.start();
        }
        else if (CharacterMainScript.raycastHit.collider.name == "Key_E")
        {
            FMOD_E.start();
        }
        else if (CharacterMainScript.raycastHit.collider.name == "Key_F")
        {
            FMOD_F.start();
        }
        else if (CharacterMainScript.raycastHit.collider.name == "Key_F#")
        {
            FMOD_Fsharp.start();
        }
        else if (CharacterMainScript.raycastHit.collider.name == "Key_G")
        {
            FMOD_G.start();
        }
        else if (CharacterMainScript.raycastHit.collider.name == "Key_G#")
        {
            FMOD_Gsharp.start();
        }
        else if (CharacterMainScript.raycastHit.collider.name == "Key_A")
        {
            FMOD_A.start();
        }
        else if (CharacterMainScript.raycastHit.collider.name == "Key_A#")
        {
            FMOD_Asharp.start();
        }
        else if (CharacterMainScript.raycastHit.collider.name == "Key_B")
        {
            FMOD_B.start();
        }
    }

    //Method for checking if the sequence is played correctly
    public void CheckResults()
    {
        switch (switchPianoInt)
        {
            case 0:
                arrayRaycastHit[0] = CharacterMainScript.raycastHit.collider.name;
                if (CharacterMainScript.raycastHit.collider.name == arrayCorrect[0])
                {
                    //Jump to next case
                    switchPianoInt += 1;

                    //Activate light
                    lightPiano1.GetComponent<MeshRenderer>().material.color = colorGreen;
                }
                else
                {
                    //Reset back to Case 0
                    switchPianoInt = 0;
                }
                break;

            case 1:
                arrayRaycastHit[1] = CharacterMainScript.raycastHit.collider.name;
                if (CharacterMainScript.raycastHit.collider.name == arrayCorrect[1])
                {
                    switchPianoInt += 1;
                    lightPiano2.GetComponent<MeshRenderer>().material.color = colorGreen;
                }

                else
                {
                    lightPiano1.GetComponent<MeshRenderer>().material.color = colorGrey;
                    switchPianoInt = 0;
                }
                break;

            case 2:
                arrayRaycastHit[2] = CharacterMainScript.raycastHit.collider.name;
                if (CharacterMainScript.raycastHit.collider.name == arrayCorrect[2])
                {
                    switchPianoInt += 1;
                    lightPiano3.GetComponent<MeshRenderer>().material.color = colorGreen;
                }

                else
                {
                    lightPiano1.GetComponent<MeshRenderer>().material.color = colorGrey;
                    lightPiano2.GetComponent<MeshRenderer>().material.color = colorGrey;
                    switchPianoInt = 0;
                }
                break;

            case 3:
                arrayRaycastHit[3] = CharacterMainScript.raycastHit.collider.name;
                if (CharacterMainScript.raycastHit.collider.name == arrayCorrect[3])
                {
                    lightPiano4.GetComponent<MeshRenderer>().material.color = colorGreen;
                    switchPianoInt += 1;
                }

                else
                {
                    lightPiano1.GetComponent<MeshRenderer>().material.color = colorGrey;
                    lightPiano2.GetComponent<MeshRenderer>().material.color = colorGrey;
                    lightPiano3.GetComponent<MeshRenderer>().material.color = colorGrey;
                    switchPianoInt = 0;
                }
                break;
        }

        //Check and confirm that all keys have been played correctly
        if ((arrayRaycastHit[0] == arrayCorrect[0]) && (arrayRaycastHit[1] == arrayCorrect[1]) && (arrayRaycastHit[2] == arrayCorrect[2]) && (arrayRaycastHit[3] == arrayCorrect[3]))
        {
            mainScript.PlaySoundSolved();
            solvedPiano = true;
        }
    }
}
