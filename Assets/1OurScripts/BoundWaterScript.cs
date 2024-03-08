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

    public GameObject TestWater;

    public GameObject BoundFire;
    public GameObject BoundEarth;
    public GameObject BoundAir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Use Yield return to like not make it start instantly????
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoundHMD") && !narrationHasFinished && !narrationHasStarted) //
        {
            Debug.Log("Entered Water");
            TestWater.SetActive(true);
            //Play narration and remove other temp

            BoundAir.SetActive(false);
            BoundFire.SetActive(false);
            BoundEarth.SetActive(false);

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
            //Force sensor
            // if (ConnectUnityWithSensors.isForceDetected) 
            // {
            //}
            waterObjectToCollect.SetActive(true);
            audioSource.PlayOneShot(narrationClipTwo);
            dropHasAppeared = true;

            //Make these not go until narration has ended?
            //Or make it so that these are not activated until the object has been collected
            //Implement this for all of the different elements
            BoundAir.SetActive(true);
            BoundFire.SetActive(true);
            BoundEarth.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BoundHMD")) //
        {
            TestWater.SetActive(false);
        }
    }

}
