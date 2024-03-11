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


    //Boundary control
    private BoundaryControlScript boundControl;


    //Use Yield return to like not make it start instantly????
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoundHMD") && !narrationHasFinished && !narrationHasStarted) //
        {
            Debug.Log("Entered Water");
            //TestWater.SetActive(true);

            //Removing other bounds temporarily
            boundControl.tempRemoveBoundary("Water");

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

    }

    public void collectDistance()
    {
        if (narrationHasFinished && !dropHasAppeared)
        {
            waterObjectToCollect.SetActive(true);
            audioSource.PlayOneShot(narrationClipTwo);
            dropHasAppeared = true;
            stationCompleted();
        }
    }


    //Method to remove the boundary when station has been completed.
    //Start through Unity event wrapper for when item to collect is selected.

    public void stationCompleted()
    {
        StartCoroutine(RemoveCollectedItem());
        boundControl.removeBoundary("Water");
        boundControl.reactivateBoundary("Water");
        //Insert functionality for starting counter narration etc
    }

    IEnumerator RemoveCollectedItem()
    {
        yield return new WaitForSeconds(2.0f);
        waterObjectToCollect.SetActive(false);
    }

}
