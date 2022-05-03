using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DrumMachineScript : MonoBehaviour
{
    public CharacterMainScript mainScript;

    //Individual Sequencer Buttons
    //Kick
    public GameObject kick1;
    public GameObject kick2;
    public GameObject kick3;
    public GameObject kick4;
    public GameObject kick5;
    public GameObject kick6;
    public GameObject kick7;
    public GameObject kick8;

    //Snare
    public GameObject snare1;
    public GameObject snare2;
    public GameObject snare3;
    public GameObject snare4;
    public GameObject snare5;
    public GameObject snare6;
    public GameObject snare7;
    public GameObject snare8;

    //Hi-Hat
    public GameObject hihat1;
    public GameObject hihat2;
    public GameObject hihat3;
    public GameObject hihat4;
    public GameObject hihat5;
    public GameObject hihat6;
    public GameObject hihat7;
    public GameObject hihat8;

    //Sequencer
    public Material matSeqOn;
    public Material matSeqOff;
    public Material matSeqBlink;
    public GameObject seqMain;

    public List<GameObject> Kick_Parent = new List<GameObject>();
    public List<GameObject> Snare_Parent = new List<GameObject>();
    public List<GameObject> Hihat_Parent = new List<GameObject>();

    public GameObject[] Kick_Parent_Get;
    public GameObject[] Snare_Parent_Get;
    public GameObject[] Hihat_Parent_Get;

    //Bool for confirming if puzzle is solved
    public static bool solvedDrum = false;

    //FMOD
    //Sequencer
    public FMOD.Studio.EventInstance FMOD_Kick;
    public FMOD.Studio.EventInstance FMOD_Snare;
    public FMOD.Studio.EventInstance FMOD_Hihat;

    // Start is called before the first frame update
    void Start()
    {
        //Create 
        Kick_Parent_Get = GameObject.FindGameObjectsWithTag("Kick");
        Snare_Parent_Get = GameObject.FindGameObjectsWithTag("Snare");
        Hihat_Parent_Get = GameObject.FindGameObjectsWithTag("Hihat");

        //Sort
        Kick_Parent = Kick_Parent_Get.OrderBy(tile => tile.name).ToList();
        Snare_Parent = Snare_Parent_Get.OrderBy(tile => tile.name).ToList();
        Hihat_Parent = Hihat_Parent_Get.OrderBy(tile => tile.name).ToList();

        //FMOD
        //Sequencer
        FMOD_Kick = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Sequencer/Kick");
        FMOD_Snare = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Sequencer/Snare");
        FMOD_Hihat = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Sequencer/Hihat");

        //Attach events to game object
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Kick, seqMain.transform, seqMain.GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Snare, seqMain.transform, seqMain.GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Hihat, seqMain.transform, seqMain.GetComponent<Rigidbody>());
    }

    //Method for activating one button for kick-drums
    public IEnumerator PlaySeqKick()
    {
        foreach (var drumKick in Kick_Parent)
        {
            if (drumKick.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn)
            {
                drumKick.GetComponent<MeshRenderer>().material = matSeqBlink;
                FMOD_Kick.start();
                yield return new WaitForSeconds(0.3f);
                drumKick.GetComponent<MeshRenderer>().material = matSeqOn;
            }

            else
            {
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
    //Method for activating one button for snare-drums
    public IEnumerator PlaySeqSnare()
    {
        foreach (var drumSnare in Snare_Parent)
        {
            if (drumSnare.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn)
            {
                drumSnare.GetComponent<MeshRenderer>().material = matSeqBlink;
                FMOD_Snare.start();
                yield return new WaitForSeconds(0.3f);
                drumSnare.GetComponent<MeshRenderer>().material = matSeqOn;
            }

            else
            {
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    //Method for activating one button for hihats
    public IEnumerator PlaySeqHihat()
    {
        foreach (var drumHihat in Hihat_Parent)
        {
            if (drumHihat.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn)
            {
                drumHihat.GetComponent<MeshRenderer>().material = matSeqBlink;
                FMOD_Hihat.start();
                yield return new WaitForSeconds(0.3f);
                drumHihat.GetComponent<MeshRenderer>().material = matSeqOn;
            }

            else
            {
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    //Method for activating kick-sequencer
    public IEnumerator SequencerKick()
    {
        if (CharacterMainScript.raycastHit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn)
        {
            CharacterMainScript.raycastHit.collider.gameObject.GetComponent<MeshRenderer>().material = matSeqOff;
            yield break;
        }
        else
        {
            CharacterMainScript.raycastHit.collider.gameObject.GetComponent<MeshRenderer>().material = matSeqOn;
            FMOD_Kick.start();
            yield return new WaitForSeconds(0.1f);
            yield break;
        }
    }

    //Method for activating snare-sequencer
    public IEnumerator SequencerSnare()
    {
        if (CharacterMainScript.raycastHit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn)
        {
            CharacterMainScript.raycastHit.collider.gameObject.GetComponent<MeshRenderer>().material = matSeqOff;
            yield break;
        }
        else
        {
            CharacterMainScript.raycastHit.collider.gameObject.GetComponent<MeshRenderer>().material = matSeqOn;
            FMOD_Snare.start();
            yield return new WaitForSeconds(0.1f);
            yield break;
        }
    }

    //Method for activating hihat-sequencer
    public IEnumerator SequencerHihat()
    {
        if (CharacterMainScript.raycastHit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn)
        {
            CharacterMainScript.raycastHit.collider.gameObject.GetComponent<MeshRenderer>().material = matSeqOff;
            yield break;
        }
        else
        {
            CharacterMainScript.raycastHit.collider.gameObject.GetComponent<MeshRenderer>().material = matSeqOn;
            FMOD_Hihat.start();
            yield return new WaitForSeconds(0.1f);
            yield break;
        }
    }

    //Method for playing all three sequencers simultaneously
    public void PlayDrumMachine()
    {
        if (kick1.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn && kick4.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn &&
            kick6.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn && snare3.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn &&
            snare7.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn && hihat1.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn &&
            hihat2.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn && hihat3.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn &&
            hihat4.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn && hihat5.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn &&
            hihat6.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn && hihat7.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn &&
            hihat8.GetComponent<MeshRenderer>().sharedMaterial == matSeqOn && solvedDrum == false)
            {
                StartCoroutine(WaitDrum());
                mainScript.PlaySoundSolved();
                solvedDrum = true;
            }

        StartCoroutine(PlaySeqKick());
        StartCoroutine(PlaySeqSnare());
        StartCoroutine(PlaySeqHihat());
    }

    //Coroutine to wait before playing SoundSolved
    IEnumerator WaitDrum()
    {
        yield return new WaitForSeconds(3f);
    }
}