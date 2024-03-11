using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundFireScript : MonoBehaviour
{
    public bool narrationHasFinished = false;
    public bool narrationHasStarted = false;

    public AudioSource audioSource;
    public AudioClip narrationClip;
    public AudioClip narrationClipTwo;

    public GameObject fireObjectToCollect;

    private bool objectHasBeenCollected = false;


    //Object to collect found in Fire collision script

    //Boundary control
    public BoundaryControlScript boundControl;


    //Use Yield return to like not make it start instantly????
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoundHMD") && !narrationHasFinished && !narrationHasStarted) //
        {
            //Removing other bounds temporarily
            boundControl.TempRemoveBoundary("Fire");

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

    public void CollectFireObject()
    {
        StartCoroutine(SecondNarrationAndObject());
        audioSource.PlayOneShot(narrationClipTwo);
        fireObjectToCollect.SetActive(true);

    }

    IEnumerator SecondNarrationAndObject()
    {
        audioSource.PlayOneShot(narrationClipTwo);
        fireObjectToCollect.SetActive(true);
        yield return new WaitForSeconds(narrationClipTwo.length);

    }


    //Method to remove the boundary when station has been completed.
    //Start through Unity event wrapper for when item to collect is selected.

    //Method for controlling when the item is grabbed

    public void stationCompleted()
    {
        StartCoroutine(RemoveCollectedItem());
        fireObjectToCollect.SetActive(false);
        boundControl.ReactivateBoundary();
        boundControl.RemoveBoundary("Fire");
    }

    IEnumerator RemoveCollectedItem()
    {
        yield return new WaitForSeconds(2.0f);
    }
}
