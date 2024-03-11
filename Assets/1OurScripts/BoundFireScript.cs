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


    //Object to collect found in Fire collision script

    //Boundary control
    private BoundaryControlScript boundControl;


    //Use Yield return to like not make it start instantly????
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoundHMD")) //
        {
            //Removing other bounds temporarily
            boundControl.tempRemoveBoundary("Fire");

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


    //Method to remove the boundary when station has been completed.
    //Start through Unity event wrapper for when item to collect is selected.
    //Make it a coroutine like so that it will wait 2 sec before removing
    //the item to collect (now it goes away instantly)

    //Method for controlling when the item is grabbed

    public void stationCompleted()
    {
        StartCoroutine(RemoveCollectedItem());
        boundControl.removeBoundary("Fire");
        boundControl.reactivateBoundary("Fire");
       //Insert functionality for starting counter narration etc
    }

    IEnumerator RemoveCollectedItem()
    {
        yield return new WaitForSeconds(2.0f);
        fireObjectToCollect.SetActive(false);
    }

}
