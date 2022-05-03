using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchScript : MonoBehaviour
{
    //Get Main script
    public CharacterMainScript mainScript;

    //Lightswitch
    public GameObject lightSwitch;

    //Get the mesh for each state of the light switch
    public Mesh lightSwitchOff;
    public Mesh lightSwitchOn;

    //notes on the wall
    public GameObject notes;

    //Bool for checking the state of the light
    public static bool lightOff = true;

    //Light
    public GameObject lightSource;

    //FMOD
    FMOD.Studio.EventInstance FMOD_LightOnOff;

    // Start is called before the first frame update
    void Start()
    {
        //Start scene with lights on
        lightOff = true;

        //FMOD event and attaching to game object
        FMOD_LightOnOff = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Diverse/LightOnOff");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_LightOnOff, lightSwitch.transform, lightSwitch.GetComponent<Rigidbody>());
    }

    public void LightSwitchOn()
    {
        //Turn Light off, turn notes on
        lightSource.SetActive(false);
        notes.SetActive(true);

        StartCoroutine(LightCoroutine());

        lightSwitch.GetComponent<MeshFilter>().sharedMesh = lightSwitchOff;
        FMOD_LightOnOff.start();
    }

    public void LightSwitchOff()
    {
        //Turn Light on, turn notes off
        lightSource.SetActive(true);
        notes.SetActive(false);

        StartCoroutine(LightCoroutine());

        lightSwitch.GetComponent<MeshFilter>().sharedMesh = lightSwitchOn;
        FMOD_LightOnOff.start();
    }

    IEnumerator LightCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        lightOff = !lightOff;
    }
}
