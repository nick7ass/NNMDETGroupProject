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


    //Boundary control
    private BoundaryControlScript boundControl;

    //Use Yield return to like not make it start instantly????
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("BoundHMD") && !narrationHasFinished && !narrationHasStarted) //
        {

            //Removing other bounds temporarily
            boundControl.tempRemoveBoundary("Earth");

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

    public void collectForce() {
        if (narrationHasFinished && !seedHasAppeared)
        {
            //Force sensor
            // if (ConnectUnityWithSensors.isForceDetected) 
            // {
            //}
            earthObjectToCollect.SetActive(true);
            audioSource.PlayOneShot(narrationClipTwo);
            seedHasAppeared = true;

        }
    }

    //Method to remove the boundary when station has been completed.
    //Start through Unity event wrapper for when item to collect is selected.
    public void stationCompleted()
    {
        StartCoroutine(RemoveCollectedItem());
        boundControl.removeBoundary("Earth");
        boundControl.reactivateBoundary("Earth");
        //Insert functionality for starting counter narration etc
    }

    IEnumerator RemoveCollectedItem()
    {
        yield return new WaitForSeconds(2.0f);
        earthObjectToCollect.SetActive(false);
    }


}
