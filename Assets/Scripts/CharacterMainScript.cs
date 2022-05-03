using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class CharacterMainScript : MonoBehaviour
{
    //Get other scripts to access their methods and variables
    public PianoScript pianoScript;
    public DrumMachineScript drumScript;
    public CassettePlayerScript cassetteScript;
    public LightSwitchScript lightScript;
    public CodeLockScript codeScript;
    public OscillatorScript oscillatorScript;
    public VinylScript vinylScript;

    //Crosshairs
    public GameObject crosshair;
    public GameObject crosshairHand;
    public GameObject crosshairInteract;

    //Boolean checks
    public static bool activateKey = false;
    public bool openDrawer = false;

    //Diverse
    public GameObject painting;
    public Transform doorNightstand;
    public Transform doorWardrobeL;
    public Transform doorWardrobeR;
    public GameObject keyWardrobe;
    public GameObject pillow;
    public GameObject objCassettePlayerInside;
    public GameObject drawer;
    public GameObject Textbox;
    public GameObject Box;

    //Inventory
    public GameObject invWardrobeKey;
    public GameObject invCassette;

    //Objects - pickup
    public GameObject cassette;

    //Interactable objects
    private Rigidbody rigidbodyBook;
    private Rigidbody rigidbodyPillow;
    private Rigidbody rigidbodyPainting;

    //Animations
    Animator animatorCrosshair;
    Animator animatorDrawer;
    Animator animatorNSDoor;
    Animator animatorWRDoorL;
    Animator animatorWRDoorR;
    Animator animatorpillow;

    //FMOD-events
    //Drawer
    FMOD.Studio.EventInstance FMOD_Drawer_Out;
    FMOD.Studio.EventInstance FMOD_Drawer_In;

    //Diverse
    FMOD.Studio.EventInstance FMOD_Pickup;
    FMOD.Studio.EventInstance FMOD_Solved;
    FMOD.Studio.EventInstance FMOD_Wardrobe_Closed;
    FMOD.Studio.EventInstance FMOD_Wardrobe_Opened;

    //Character Raycast
    public static RaycastHit raycastHit;
    public static GameObject raycastHitObj;
    public LayerMask mask;

    void Start()
    {
        //Hide cursor
        Cursor.visible = false; 

        //Get and Set starting animation states
        animatorCrosshair = crosshair.GetComponent<Animator>();
        animatorCrosshair.SetBool("OnOff", false);

        animatorDrawer = drawer.GetComponent<Animator>();
        animatorDrawer.SetBool("Drawer_Out", false);

        animatorNSDoor = doorNightstand.GetComponent<Animator>();
        animatorNSDoor.SetBool("NS_Door_Open", false);

        animatorWRDoorL = doorWardrobeL.GetComponent<Animator>();
        animatorWRDoorL.SetBool("Wardrobe_L", false);

        animatorWRDoorR = doorWardrobeR.GetComponent<Animator>();
        animatorWRDoorR.SetBool("Wardrobe_R", false);

        animatorpillow = pillow.GetComponent<Animator>();
        animatorpillow.SetBool("pillow", false);

        //Get the Rigidbody for the p to manipulate
        rigidbodyPainting = painting.GetComponent<Rigidbody>();

        //FMOD
        //Drawer
        FMOD_Drawer_Out = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Drawer/DrawerOut");
        FMOD_Drawer_In = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Drawer/DrawerIn");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Drawer_Out, drawer.transform, drawer.GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Drawer_In, drawer.transform, drawer.GetComponent<Rigidbody>());

        //Wardrobe
        FMOD_Wardrobe_Opened = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Diverse/OpenWardrobe");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Wardrobe_Opened, doorWardrobeR.transform, doorWardrobeR.GetComponent<Rigidbody>());
        FMOD_Wardrobe_Closed = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Diverse/ClosedWardrobe");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Wardrobe_Closed, doorWardrobeR.transform, doorWardrobeR.GetComponent<Rigidbody>());
        
        //Diverse
        FMOD_Pickup = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Diverse/Pickup");
        FMOD_Solved = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/PuzzleSolve");
    }

    void Update()
    {
        //Draw Raycast from player
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 3f, Color.red);

        //Check what object the Raycast is hitting
        if (Physics.Raycast(transform.position, fwd, out raycastHit, 3, mask))
        {
            //Store the object the Raycast is currently hitting
            raycastHitObj = raycastHit.transform.gameObject;

           //If-statements for setting correct crosshair depending on what object is being hovered over
            if (raycastHit.collider.tag == "Grab")
            {
                crosshair.SetActive(false);
                crosshairHand.SetActive(true);
            }

            if (raycastHit.collider.tag == "Interact" || (raycastHitObj.tag == "Kick") || (raycastHitObj.tag == "Snare") || (raycastHitObj.tag == "Hihat") || (raycastHitObj.tag == "Piano"))
            {
                crosshair.SetActive(false);
                crosshairInteract.SetActive(true);
            }

            //If-statements for performing the correct action when interacting with objects
            if (Input.GetMouseButtonDown(0))
            {
                //Check what object the ray-cast is hitting and perform appropriate actions
                if (raycastHit.collider.name == "Wardrobe_Key")
                {
                    FMOD_Pickup.start();
                    keyWardrobe.SetActive(false);
                    invWardrobeKey.SetActive(true);
                }

                //Oscillators
                else if ((raycastHit.collider.name == "Freq_1_Up") || (raycastHit.collider.name == "Freq_1_Down") || (raycastHit.collider.name == "Freq_2_Up") || (raycastHit.collider.name == "Freq_2_Down") || (raycastHit.collider.name == "Freq_3_Up") || (raycastHit.collider.name == "Freq_3_Down") || (raycastHit.collider.name == "Freq_Enter"))
                {
                    oscillatorScript.ChangePitch();
                }

                //pillow
                else if (raycastHit.collider.name == "Pillow")
                {
                    animatorpillow.SetBool("Pillow", true);
                }

                //Nightstand door
                else if (raycastHit.collider.name == "Nightstand_Door")
                {
                    animatorNSDoor.SetBool("NS_Door_Open", true);
                }

                //Drawer
                else if (raycastHit.collider.name == "Drawer" && openDrawer == false)
                {
                    FMOD_Drawer_Out.start();
                    animatorDrawer.SetBool("Drawer_Out", true);
                    
                    StartCoroutine(DrawerCoroutine());
                }

                else if (raycastHit.collider.name == "Drawer" && openDrawer == true)
                {
                    FMOD_Drawer_In.start();
                    animatorDrawer.SetBool("Drawer_Out", false);
                    
                    StartCoroutine(DrawerCoroutine());
                }

                //Wardrobe
                else if (((raycastHit.collider.name == "Wardrobe_Door_L") || (raycastHit.collider.name == "Wardrobe_Door_R")) && (invWardrobeKey.activeSelf == false))
                {
                    FMOD_Wardrobe_Closed.start();
                }

                else if (((raycastHit.collider.name == "Wardrobe_Door_L") || (raycastHit.collider.name == "Wardrobe_Door_R")) && (invWardrobeKey.activeSelf == true))
                {
                    FMOD_Wardrobe_Opened.start();
                    invWardrobeKey.SetActive(false);
                    animatorWRDoorL.SetBool("WardrobeL", true);
                    animatorWRDoorR.SetBool("WardrobeR", true);
                }

                else if (raycastHit.collider.name == "Cassette")
                {
                    FMOD_Pickup.start();
                    cassette.SetActive(false);
                    invCassette.SetActive(true);
                }

                //Cassette Player
                else if (raycastHit.collider.name == "Cassette_Play_Forward" && activateKey == true)
                {
                    cassetteScript.PlayCassetteFwd();
                }

                else if (raycastHit.collider.name == "Cassette_Play_Backward" && activateKey == true)
                {
                    cassetteScript.PlayCassetteBack();
                }

                else if (raycastHit.collider.name == "Cassette_Player_Box" && invCassette.activeSelf == true)
                {
                    invCassette.SetActive(false);
                    objCassettePlayerInside.SetActive(true);
                    activateKey = true;
                }

                //CodeLock
                else if (raycastHit.collider.tag == "Code")
                {
                    codeScript.CheckResultsCode();
                }

                //Light switch
                else if (raycastHit.collider.name == "Light_Switch" && LightSwitchScript.lightOff == true)
                {
                    lightScript.LightSwitchOn();
                }

                else if (raycastHit.collider.name == "Light_Switch" && LightSwitchScript.lightOff == false)
                {
                    lightScript.LightSwitchOff();
                }

                //p
                else if (raycastHit.collider.name == "Painting")
                {
                    rigidbodyPainting.isKinematic = false;
                }

                //Piano
                else if (raycastHit.collider.tag == "Piano")
                {
                    pianoScript.CheckResults();
                    pianoScript.PianoKeys();
                }

                //Sequencer and Vinyl player
                else if (raycastHit.collider.name == "Vinyl_Button")
                {
                    vinylScript.PlayVinyl();
                }

                else if (raycastHit.collider.tag == "Kick")
                {
                    StartCoroutine(drumScript.SequencerKick());
                }

                else if (raycastHit.collider.tag == "Snare")
                {
                    StartCoroutine(drumScript.SequencerSnare());
                }

                else if (raycastHit.collider.tag == "Hihat")
                {
                    StartCoroutine(drumScript.SequencerHihat());
                }

                else if (raycastHit.collider.name == "Kick_Play_Button")
                {
                    StartCoroutine(drumScript.PlaySeqKick());
                }

                else if (raycastHit.collider.name == "Snare_Play_Button")
                {
                    StartCoroutine(drumScript.PlaySeqSnare());
                }
                else if (raycastHit.collider.name == "Hihat_Play_Button")
                {
                    StartCoroutine(drumScript.PlaySeqHihat());
                }

                else if (raycastHit.collider.name == "Drums_Together_Button")
                {
                    drumScript.PlayDrumMachine();
                }

                //Finish door
                else if (raycastHit.collider.name == "Door" && (DrumMachineScript.solvedDrum == true && PianoScript.solvedPiano == true && OscillatorScript.solvedOscil == true && CodeLockScript.solvedCode == true))
                {
                    Application.Quit();
                }
            }
        }

        //Set standard crosshair when Raycast is no longer colliding with an interactable oject
        else
        {
            crosshair.SetActive(true);
            crosshairHand.SetActive(false);
            crosshairInteract.SetActive(false);
        }
    }

    //Coroutine for opening and closing drawer
    IEnumerator DrawerCoroutine()
    {
        yield return new WaitForSeconds(1f);
        openDrawer = !openDrawer;
    }

    //Coroutine for a short wait
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
    }

    //Method for playing FMOD-event when completing each puzzle
    public void PlaySoundSolved()
    {
        StartCoroutine(Wait());
        FMOD_Solved.start();
    }
}