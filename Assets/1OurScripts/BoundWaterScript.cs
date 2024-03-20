using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundWaterScript : MonoBehaviour
{
    public bool narrationHasFinished = false;
    public bool dropHasAppeared = false;
    public bool narrationHasStarted = false;

    public AudioSource audioSource;
    public AudioClip narrationClip;
    public AudioClip narrationClipTwo;

    public GameObject waterObjectToCollect;

    public GameObject waterInstructionUI;

    private bool objectHasBeenCollected = false;


    //Boundary control
    public BoundaryControlScript boundControl;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoundHMD") && !narrationHasFinished && !narrationHasStarted) //
        {
            Debug.Log("Entered Water");
            //TestWater.SetActive(true);

            //Removing other bounds temporarily
            boundControl.TempRemoveBoundary("Water");

            //Play narration
            StartCoroutine(NarrationAndSignalCoroutine());

        }
    }

    IEnumerator NarrationAndSignalCoroutine()
    {
        narrationHasStarted = true;
        audioSource.PlayOneShot(narrationClip);
        yield return new WaitForSeconds(narrationClip.length);
        waterInstructionUI.SetActive(true);
        narrationHasFinished = true;

    }

    public void collectTouch()
    {
        if (narrationHasFinished && !dropHasAppeared)
        {
            dropHasAppeared = true;
            waterObjectToCollect.SetActive(true);
            audioSource.PlayOneShot(narrationClipTwo);
            
        }
    }


    public void stationCompleted()
    {
        waterInstructionUI.SetActive(false);
        waterObjectToCollect.SetActive(false);
        //boundControl.ReactivateBoundary();
        boundControl.RemoveBoundary("Water");
    }

}
