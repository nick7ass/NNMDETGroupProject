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

    public GameObject fireObjectToThrow;
    public GameObject fireObjectToCollect;

    private bool objectHasBeenCollected = false;


    //Object to collect found in Fire collision script

    //Boundary control
    public BoundaryControlScript boundControl;


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
        fireObjectToThrow.SetActive(true);
        narrationHasFinished = true;

    }

    public void CollectFireObject()
    {
        StartCoroutine(SecondNarrationAndObject());
        fireObjectToCollect.SetActive(true);
    }

    IEnumerator SecondNarrationAndObject()
    {
        audioSource.PlayOneShot(narrationClipTwo);
        fireObjectToCollect.SetActive(true);
        yield return new WaitForSeconds(narrationClipTwo.length);
    }

    public void stationCompleted()
    {
        fireObjectToCollect.SetActive(false);
        //boundControl.ReactivateBoundary();
        boundControl.RemoveBoundary("Fire");
    }
}
