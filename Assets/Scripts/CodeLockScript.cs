using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeLockScript : MonoBehaviour
{
    //Get main script
    public CharacterMainScript mainScript;

    //Codelock
    public GameObject mainFrame;

    //Strings to store temporily store codes
    public string code = "";
    public string attemptedCode;

    //Arrays to store player-code and correct code
    public string[] digitPlayer;
    public string[] digitCorrect;

    //Initialize Switch-case
    private int digitSwitch = 0;

    //Get text for digits
    public TextMesh digitText;

    //Bool for confirming when the puzzle is solved
    public static bool solvedCode = false;

    //FMOD
    FMOD.Studio.EventInstance FMOD_Codelock_Beep;
    FMOD.Studio.EventInstance FMOD_Codelock_Beep_Finish;
    FMOD.Studio.EventInstance FMOD_Codelock_Click;
    FMOD.Studio.EventInstance FMOD_Wrong;

    void Start()
    {
         //Set correct values in arrays
        digitCorrect = new string[] { "8", "1", "3", "4" };
        digitPlayer = new string[4];

        //Get Text-component from GameObject
        digitText = digitText.GetComponent<TextMesh>();

        //FMOD events
        FMOD_Codelock_Beep = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Codelock/Beep");
        FMOD_Codelock_Beep_Finish = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Codelock/Beep Finish");
        FMOD_Codelock_Click = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Diverse/Click1");
        FMOD_Wrong = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Diverse/Wrong");

        //Attach events to objects
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Codelock_Beep, mainFrame.transform, mainFrame.GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Codelock_Beep_Finish, mainFrame.transform, mainFrame.GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Codelock_Click, mainFrame.transform, mainFrame.GetComponent<Rigidbody>());
       
    }

    public void CheckResultsCode()
    {
        //FMOD - Play button sounds
        FMOD_Codelock_Click.start();
        FMOD_Codelock_Beep.start();

        //Check if the player-code is correct
        if (CharacterMainScript.raycastHit.collider.name == "Enter")
        {
            //Correct code
            if ((digitPlayer[0] == digitCorrect[0] && (digitPlayer[1] == digitCorrect[1] && digitPlayer[2] == digitCorrect[2] && digitPlayer[3] == digitCorrect[3]) && (solvedCode == false)))
            {
                FMOD_Codelock_Beep_Finish.start();
                mainScript.PlaySoundSolved();
                solvedCode = true;
            }
        
            //Wrong code
            else
            {
                FMOD_Wrong.start();
                digitSwitch = 5;
            }

        }
        
        //Switch-case to check each player-digit
        switch (digitSwitch)
        {
            case 0:
                digitPlayer[0] = CharacterMainScript.raycastHit.collider.name;
                digitSwitch += 1;
                digitText.text += CharacterMainScript.raycastHit.collider.name.ToString();
                break;

            case 1:
                digitPlayer[1] = CharacterMainScript.raycastHit.collider.name;
                digitSwitch += 1;
                digitText.text += CharacterMainScript.raycastHit.collider.name.ToString();
                break;

            case 2:
                digitPlayer[2] = CharacterMainScript.raycastHit.collider.name;
                digitSwitch += 1;
                digitText.text += CharacterMainScript.raycastHit.collider.name.ToString();
                break;

            case 3:
                digitPlayer[3] = CharacterMainScript.raycastHit.collider.name;
                digitSwitch += 1;
                digitText.text += CharacterMainScript.raycastHit.collider.name.ToString();
                break;

            case 5:
                digitPlayer[0] = "";
                digitPlayer[1] = "";
                digitPlayer[2] = "";
                digitPlayer[3] = "";
                digitText.text = "";
                digitSwitch = 0;
                break;
        }
    }
}
