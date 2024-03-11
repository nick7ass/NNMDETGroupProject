using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryControlScript : MonoBehaviour
{
    public GameObject airBoundary;
    public GameObject earthBoundary;
    public GameObject waterBoundary;
    public GameObject fireBoundary;

    private bool airFinished = false;
    private bool earthFinished = false;
    private bool waterFinished = false;
    private bool fireFinished = false;

    private int collectionCounter = 0;

    // Add references for the AudioSource and narration clips
    public AudioSource narrationSource;
    public AudioClip[] narrationClips; // Ensure this array is populated in the Inspector with your narration clips

    public void tempRemoveBoundary(string bound)
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

    public void reactivateBoundary(string bound)
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

    public void removeBoundary(string bound)
    {
        if (bound == "Air")
        {
            airFinished = true;
            collectionCounter++;
            PlayCollectionNarration();
            airBoundary.SetActive(false);
        }
        else if (bound == "Earth")
        {
            earthFinished = true;
            collectionCounter++;
            PlayCollectionNarration();
            earthBoundary.SetActive(false);
        }
        else if (bound == "Water")
        {
            waterFinished = true;
            collectionCounter++;
            PlayCollectionNarration();
            waterBoundary.SetActive(false);
        }
        else if (bound == "Fire")
        {
            fireFinished = true;
            collectionCounter++;
            PlayCollectionNarration();
            fireBoundary.SetActive(false);
        }
    }
    private void PlayCollectionNarration()
    {
        if (collectionCounter > 0 && collectionCounter <= narrationClips.Length)
        {
            narrationSource.clip = narrationClips[collectionCounter - 1];
            narrationSource.Play();
        }
    }

    //ADD functionality for detecting when all elements have been collected (eg when collectionCounter
    //is 4) 
}