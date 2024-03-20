using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryControlScript : MonoBehaviour
{
    //Boundries
    public GameObject airBoundary;
    public GameObject earthBoundary;
    public GameObject waterBoundary;
    public GameObject fireBoundary;

    //Collection images
    public GameObject airNotCollectedImage;
    public GameObject airCollectedImage;
    public GameObject earthNotCollectedImage;
    public GameObject earthCollectedImage;
    public GameObject waterNotCollectedImage;
    public GameObject waterCollectedImage;
    public GameObject fireNotCollectedImage;
    public GameObject fireCollectedImage;

    private bool airFinished = false;
    private bool earthFinished = false;
    private bool waterFinished = false;
    private bool fireFinished = false;

    private bool airHasBeenCollected = false;
    private bool earthHasBeenCollected = false;
    private bool waterHasBeenCollected = false;
    private bool fireHasBeenCollected = false;

    private int collectionCounter = 0;

    // Add references for the AudioSource and narration clips
    public AudioSource narrationSource;
    public AudioClip[] narrationClips; 

    //Removes other boundries when an elemental boundary has been entered.
    public void TempRemoveBoundary(string bound)
    {
        if (bound == "Air")
        {
            earthBoundary.SetActive(false);
            waterBoundary.SetActive(false);
            fireBoundary.SetActive(false);
        }
        else if (bound == "Earth")
        {
            airBoundary.SetActive(false);
            waterBoundary.SetActive(false);
            fireBoundary.SetActive(false);
        }
        else if (bound == "Water")
        {
            earthBoundary.SetActive(false);
            airBoundary.SetActive(false);
            fireBoundary.SetActive(false);
        }
        else if (bound == "Fire")
        {
            earthBoundary.SetActive(false);
            waterBoundary.SetActive(false);
            airBoundary.SetActive(false);
        }
    }

    //Reactivate boundries when station has been finished.
    public void ReactivateBoundary()
    {
        if (!airFinished)
        {
            airBoundary.SetActive(true);
        }

        if (!earthFinished)
        {
            earthBoundary.SetActive(true);
        }

        if (!waterFinished)
        {
            waterBoundary.SetActive(true);
        }

        if (!fireFinished)
        {
            fireBoundary.SetActive(true);
        }
    }

    //Things to happen when an elemental station has been completed. 
    public void RemoveBoundary(string bound)
    {
        if (bound == "Air" && !airHasBeenCollected)
        {
            airFinished = true;
            collectionCounter++;
            airNotCollectedImage.SetActive(false);
            airCollectedImage.SetActive(true);
            StartCoroutine(PlayCollectionNarration());
            airHasBeenCollected = true;
            airBoundary.SetActive(false);
        }
        else if (bound == "Earth" && !earthHasBeenCollected)
        {
            earthFinished = true;
            collectionCounter++;
            earthNotCollectedImage.SetActive(false);
            earthCollectedImage.SetActive(true);
            StartCoroutine(PlayCollectionNarration());

            earthHasBeenCollected = true;
            earthBoundary.SetActive(false);
        }
        else if (bound == "Water" && !waterHasBeenCollected)
        {
            waterFinished = true;
            collectionCounter++;
            waterNotCollectedImage.SetActive(false);
            waterCollectedImage.SetActive(true);
            StartCoroutine(PlayCollectionNarration());

            waterHasBeenCollected = true;
            waterBoundary.SetActive(false);
        }
        else if (bound == "Fire" && !fireHasBeenCollected)
        {
            fireFinished = true;
            collectionCounter++;
            fireNotCollectedImage.SetActive(false);
            fireCollectedImage.SetActive(true);
            StartCoroutine(PlayCollectionNarration());

            fireHasBeenCollected = true;
            fireBoundary.SetActive(false);
        }
    }
    //Collection counter audio.
    IEnumerator PlayCollectionNarration()
    {
        if (collectionCounter > 0 && collectionCounter <= narrationClips.Length)
        {
            narrationSource.Stop();
            narrationSource.clip = narrationClips[collectionCounter - 1];
            narrationSource.Play();
            yield return new WaitForSeconds(narrationClips[collectionCounter - 1].length);
            ReactivateBoundary();
        }
    }
}