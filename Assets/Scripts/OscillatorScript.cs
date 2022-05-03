using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatorScript : MonoBehaviour
{
    //Get main script
    public CharacterMainScript mainScript;

    //Get object for audio spatialization
    public GameObject ObjOscillator;

    //FMOD-events
    FMOD.Studio.EventInstance FMOD_Sine_1;
    FMOD.Studio.EventInstance FMOD_Sine_2;
    FMOD.Studio.EventInstance FMOD_Sine_3;
    FMOD.Studio.EventInstance FMOD_Osc_Click;

    //Text from game objects
    public TextMesh sineFreq1;
    public TextMesh sineFreq2;
    public TextMesh sineFreq3;

    //Bool for checking if puzzle is solved
    public static bool solvedOscil = false;

    //Floats for changing pitch-parameter in FMOD
    private float pitch1 = 0.9f;
    private float pitch2 = 0.9f;
    private float pitch3 = 0.9f;

    //Floats for tracking frequency changes
    private float freqDifference1;
    private float freq2Difference;
    private float freq3Difference;

    void Start()
    {
        //FMOD event and attaching to gameobjects
        FMOD_Sine_1 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Safe/C Sine 1");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Sine_1, ObjOscillator.transform, ObjOscillator.GetComponent<Rigidbody>());

        FMOD_Sine_2 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Safe/C Sine 2");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Sine_2, ObjOscillator.transform, ObjOscillator.GetComponent<Rigidbody>());

        FMOD_Sine_3 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Safe/C Sine 3");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Sine_3, ObjOscillator.transform, ObjOscillator.GetComponent<Rigidbody>());

        FMOD_Osc_Click = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Diverse/Click1");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMOD_Osc_Click, ObjOscillator.transform, ObjOscillator.GetComponent<Rigidbody>());
    }

    public void ChangePitch()
    {

        //Oscillators
        if (CharacterMainScript.raycastHit.collider.name == "Freq_1_Up")
        {
            //Add difference between previous and current tone
            float oscil1 = 440f + freqDifference1;

            //Calculate correct frequency change per semiton
            float oscil1FreqChange = oscil1 * Mathf.Pow(2f, (1f / 12f));

            //Calculate difference between previous and current tone
            freqDifference1 = oscil1FreqChange - 440f;

            //Round the frequency change to integer
            int freqDifference1Int = Mathf.RoundToInt(oscil1FreqChange);

            //Convert frequency difference to string
            string freq1DifferenceStr = freqDifference1Int.ToString();

            //Set text-object equal to calculation
            sineFreq1.text = freq1DifferenceStr;

            //Change float that determines FMOD-parameter value
            pitch1 = pitch1 + 0.1f;

            //FMOD
            FMOD_Osc_Click.start();
            FMOD_Sine_1.start();
            FMOD_Sine_1.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        
            //Last calculation is the same for every following if-statement

        else if (CharacterMainScript.raycastHit.collider.name == "Freq_1_Down")
        {
            float oscil1 = 440f + freqDifference1;

            float oscil1FreqChange = oscil1 / Mathf.Pow(2f, (1f / 12f));

            freqDifference1 = oscil1FreqChange - 440f;

            int freqDifference1Int = Mathf.RoundToInt(oscil1FreqChange);

            string freq1DifferenceStr = freqDifference1Int.ToString();

            sineFreq1.text = freq1DifferenceStr;

            pitch1 = pitch1 - 0.1f;

            //FMOD
            FMOD_Osc_Click.start();
            FMOD_Sine_1.start();
            FMOD_Sine_1.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        else if (CharacterMainScript.raycastHit.collider.name == "Freq_2_Up")
        {
            float oscil2 = 440f + freq2Difference;

            float oscil2FreqChange = oscil2 * Mathf.Pow(2f, (1f / 12f));

            freq2Difference = oscil2FreqChange - 440f;

            int freq2DifferenceInt = Mathf.RoundToInt(oscil2FreqChange);

            string freq2DifferenceStr = freq2DifferenceInt.ToString();

            sineFreq2.text = freq2DifferenceStr;

            pitch2 = pitch2 + 0.1f;

            //FMOD
            FMOD_Osc_Click.start();
            FMOD_Sine_2.start();
            FMOD_Sine_2.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        else if (CharacterMainScript.raycastHit.collider.name == "Freq_2_Down")
        {
            float oscil2 = 440f + freq2Difference;

            float oscil2FreqChange = oscil2 / Mathf.Pow(2f, (1f / 12f));
            
            freq2Difference = oscil2FreqChange - 440f;

            int freq2DifferenceInt = Mathf.RoundToInt(oscil2FreqChange);

            string freq2DifferenceStr = freq2DifferenceInt.ToString();

            sineFreq2.text = freq2DifferenceStr;

            pitch2 = pitch2 - 0.1f;

            //FMOD
            FMOD_Osc_Click.start();
            FMOD_Sine_2.start();
            FMOD_Sine_2.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        else if (CharacterMainScript.raycastHit.collider.name == "Freq_3_Up")
        {
            float oscil3 = 440f + freq3Difference;

            float oscil3FreqChange = oscil3 * Mathf.Pow(2f, (1f / 12f));

            freq3Difference = oscil3FreqChange - 440f;

            int freq3DifferenceInt = Mathf.RoundToInt(oscil3FreqChange);

            string freq3DifferenceStr = freq3DifferenceInt.ToString();

            sineFreq3.text = freq3DifferenceStr;

            pitch3 = pitch3 + 0.1f;

            //FMOD
            FMOD_Osc_Click.start();
            FMOD_Sine_3.start();
            FMOD_Sine_3.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        else if (CharacterMainScript.raycastHit.collider.name == "Freq_3_Down")
        {
            float oscil3 = 440f + freq3Difference;

            float oscil3FreqChange = oscil3 / Mathf.Pow(2f, (1f / 12f));

            freq3Difference = oscil3FreqChange - 440f;

            int freq3DifferenceInt = Mathf.RoundToInt(oscil3FreqChange);

            string freq3DifferenceStr = freq3DifferenceInt.ToString();

            sineFreq3.text = freq3DifferenceStr;

            pitch3 = pitch3 - 0.1f;

            //FMOD
            FMOD_Osc_Click.start();
            FMOD_Sine_3.start();
            FMOD_Sine_3.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        //Run coroutine to play and check if tones are in correct sequence
        else if (CharacterMainScript.raycastHit.collider.name == "Freq_Enter")
        {
            FMOD_Osc_Click.start();
            StartCoroutine(OscillatorSolution());
        }

        //FMOD - Set parameter values for oscillators
        FMOD_Sine_1.setParameterByName("Freq Pitch", pitch1);
        FMOD_Sine_2.setParameterByName("Freq Pitch", pitch2);
        FMOD_Sine_3.setParameterByName("Freq Pitch", pitch3);
    }

    //Coroutine to play tones in sequence
    IEnumerator OscillatorSolution()
    {
        FMOD_Sine_1.start();
        FMOD_Sine_1.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        yield return new WaitForSeconds(1f);

        FMOD_Sine_2.start();
        FMOD_Sine_2.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        yield return new WaitForSeconds(1f);

        FMOD_Sine_3.start();
        FMOD_Sine_3.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        yield return new WaitForSeconds(1f);

        //Check if sequencies are in correct order
        if (System.Math.Round(pitch1, 1) == 0 && System.Math.Round(pitch2, 1) == 0.7 & System.Math.Round(pitch3, 1) == 1.2)
        {
            mainScript.PlaySoundSolved();
            solvedOscil = true;
        }
    }
}
