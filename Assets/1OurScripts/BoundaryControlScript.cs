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

    //Instruction UI's
    public GameObject airInstructionUI;
    public GameObject earthInstructionUI;
    public GameObject waterInstructionUI;
    public GameObject fireInstructionUI;

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
    public AudioClip[] narrationClips; // Ensure this array is populated in the Inspector with your narration clips


    public void TempRemoveBoundary(string bound)
    {
        if (bound == "Air")
        {
            //Instructions UI appear
            airInstructionUI.SetActive(true);
            earthBoundary.SetActive(false);
            waterBoundary.SetActive(false);
            fireBoundary.SetActive(false);
        }
        else if (bound == "Earth")
        {
            earthInstructionUI.SetActive(true);
            airBoundary.SetActive(false);
            waterBoundary.SetActive(false);
            fireBoundary.SetActive(false);
        }
        else if (bound == "Water")
        {
            waterInstructionUI.SetActive(true);
            earthBoundary.SetActive(false);
            airBoundary.SetActive(false);
            fireBoundary.SetActive(false);
        }
        else if (bound == "Fire")
        {
            fireInstructionUI.SetActive(true);
            earthBoundary.SetActive(false);
            waterBoundary.SetActive(false);
            airBoundary.SetActive(false);
        }
    }

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

    public void RemoveBoundary(string bound)
    {
        if (bound == "Air" && !airHasBeenCollected)
        {
            airFinished = true;
            collectionCounter++;
            airInstructionUI.SetActive(false);
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
            earthInstructionUI.SetActive(false);
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
            waterInstructionUI.SetActive(false);
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
            fireInstructionUI.SetActive(false);
            fireNotCollectedImage.SetActive(false);
            fireCollectedImage.SetActive(true);
            StartCoroutine(PlayCollectionNarration());

            fireHasBeenCollected = true;
            fireBoundary.SetActive(false);
        }
    }
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

    //ADD functionality for detecting when all elements have been collected (eg when collectionCounter
    //is 4) 
}