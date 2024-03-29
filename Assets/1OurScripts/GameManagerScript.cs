using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameManagerScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip introductionClip;

    public VideoPlayer videoPlayerObject;

    public GameObject videoPicture;
    public GameObject videoPictureReplace;
    public GameObject introCanvas;

    public GameObject airBoundary;
    public GameObject earthBoundary;
    public GameObject waterBoundary;
    public GameObject fireBoundary;
    public GameObject introBound;

    private bool introHasBeenEntered = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World Hello World");

        videoPlayerObject.Prepare();
    }

    //Trigger introduction to when passing boundary
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoundHMD") && !introHasBeenEntered)
        {
            introHasBeenEntered = true;
            //Play introduction
            StartCoroutine(IntroductionNarration());

        }
    }

    //Starting introduction
    IEnumerator IntroductionNarration()
    {
        //audioSource.PlayOneShot(introductionClip);
        videoPlayerObject.Play();
        videoPictureReplace.SetActive(true);
        videoPicture.SetActive(false);
        yield return new WaitForSeconds(introductionClip.length);
        ActivateBoundries();
    }

    //Activate all the boundaries when introduction is finished, and inactivate the ones for introduction.
    private void ActivateBoundries()
    {
        fireBoundary.SetActive(true);
        airBoundary.SetActive(true);
        waterBoundary.SetActive(true);
        earthBoundary.SetActive(true);
        introBound.SetActive(false);
        introCanvas.SetActive(false);
    }
}