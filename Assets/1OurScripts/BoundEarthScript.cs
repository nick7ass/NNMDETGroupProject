using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundEarthScript : MonoBehaviour
{
    public bool seedHasAppeared = false;

    public bool narrationHasFinished = false;
    public bool narrationHasStarted = false;

    public AudioSource audioSource;
    public AudioClip narrationClip;
    public AudioClip narrationClipTwo;

    public GameObject earthObjectToCollect;

    public GameObject earthInstructionUI;

    private bool objectHasBeenCollected = false;


    //Boundary control
    public BoundaryControlScript boundControl;

    //Use Yield return to like not make it start instantly????
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("BoundHMD") && !narrationHasFinished && !narrationHasStarted) //
        {
            //Removing other bounds temporarily
            boundControl.TempRemoveBoundary("Earth");

            //Play narration
            StartCoroutine(NarrationAndSignalCoroutine());
        }
    }

    IEnumerator NarrationAndSignalCoroutine()
    {
        narrationHasStarted = true;
        audioSource.PlayOneShot(narrationClip);
        
        yield return new WaitForSeconds(narrationClip.length);
        narrationHasFinished = true;
        earthInstructionUI.SetActive(true);
    }

    public void collectForce() {
        if (narrationHasFinished && !seedHasAppeared)
        {
            earthObjectToCollect.SetActive(true);
            audioSource.PlayOneShot(narrationClipTwo);
            seedHasAppeared = true;
        }
    }

    public void stationCompleted()
    {
        earthInstructionUI.SetActive(false);
        earthObjectToCollect.SetActive(false);
        //boundControl.ReactivateBoundary();
        boundControl.RemoveBoundary("Earth");
    }

}
