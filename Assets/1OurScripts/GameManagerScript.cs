using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip introductionClip;

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoundHMD") && !introHasBeenEntered)
        {
            introHasBeenEntered = true;
            //Play introduction
            StartCoroutine(IntroductionNarration());
            

        }
    }

    IEnumerator IntroductionNarration()
    {
        audioSource.PlayOneShot(introductionClip);
        yield return new WaitForSeconds(introductionClip.length);
        ActivateBoundries();
    }

    private void ActivateBoundries()
    {
        fireBoundary.SetActive(true);
        airBoundary.SetActive(true);
        waterBoundary.SetActive(true);
        earthBoundary.SetActive(true);
        introBound.SetActive(false);
    }


}
