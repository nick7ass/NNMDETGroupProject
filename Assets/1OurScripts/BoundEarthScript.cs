using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundEarthScript : MonoBehaviour
{
    public bool narrationHasFinished = false;
    public bool seedHasAppeared = false;
    public bool narrationHasStarted = false;

    public AudioSource audioSource;
    public AudioClip narrationClip;
    public AudioClip narrationClipTwo;

    public GameObject seedObject;

    public GameObject TestEarth;

    public GameObject BoundFire;
    public GameObject BoundWater;
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
        Debug.Log("Entered Earth");
        TestEarth.SetActive(true);


        if (other.CompareTag("BoundHMD") && !narrationHasFinished && !narrationHasStarted) //
        {
            Debug.Log("Earth entered");


            //Play narration and remove other temp
            

            BoundAir.SetActive(false);
            BoundFire.SetActive(false);
            BoundWater.SetActive(false);

            StartCoroutine(NarrationAndSignalCoroutine());

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BoundHMD")) //
        {
            TestEarth.SetActive(false);
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
            seedObject.SetActive(true);
            audioSource.PlayOneShot(narrationClipTwo);
            seedHasAppeared = true;
            BoundAir.SetActive(true);
            BoundFire.SetActive(true);
            BoundWater.SetActive(true);
        }
    }

   

}
