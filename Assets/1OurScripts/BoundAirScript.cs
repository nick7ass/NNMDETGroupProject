using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundAirScript : MonoBehaviour
{
    public ParticleSystem masterEmitter; // Assign in the inspector
    public ParticleSystem slaveEmitter; // Assign in the inspector
    public GameObject moreSpirals;
    private float defaultLifetime = 0.5f; // Default start lifetime, adjust as needed
    public float fasterLifetime = 2.0f; // Example faster lifetime, adjust as needed

    public bool isWindActive = false;
    public bool canActivateAir = false;

    public bool narrationHasPlayed = false;
    public AudioSource audioSource;
    public AudioClip narrationClip;
    public AudioClip narrationClipTwo;

    public GameObject windObjectToCollect;


    //Boundary control
    private BoundaryControlScript boundControl;

    // Start is called before the first frame update
    void Start()
    {
        defaultLifetime = masterEmitter.main.startLifetime.constant;

    }

    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoundHMD") && !isWindActive && !narrationHasPlayed) //
        {
            boundControl.tempRemoveBoundary("Air");

            narrationHasPlayed = true;
            //Play narration
            StartCoroutine(NarrationAndSignalCoroutine());

        }
    }


    IEnumerator NarrationAndSignalCoroutine()
    {
        audioSource.PlayOneShot(narrationClip);
        yield return new WaitForSeconds(narrationClip.length);
        canActivateAir = true;
    }


    public void AttemptActivatedAirEffect()
    {
        if (!isWindActive && canActivateAir)
        {
            AdjustParticleSpeed();
        }
    }



    //Air effects
    public void AdjustParticleSpeed()
    {
        var masterMain = masterEmitter.main;
        masterMain.startLifetime = fasterLifetime; // Adjust master emitter lifetime

        var slaveMain = slaveEmitter.main;
        slaveMain.duration = fasterLifetime; // Adjust slave emitter duration to match

        // Restart the particle systems to apply the changes immediately
        masterEmitter.Stop();
        masterEmitter.Play();

        slaveEmitter.Stop();
        slaveEmitter.Play();

        moreSpirals.SetActive(true);

        isWindActive = true;

        StartCoroutine(ResetParticleSpeed(5.0f)); // Assuming gesture lasts for * seconds

        //Insert second narration here
        windObjectToCollect.SetActive(true);

    }

    IEnumerator ResetParticleSpeed(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset particle system speed to default

        AdjustParticleSpeedReset();
    }

    public void AdjustParticleSpeedReset()
    {
        var masterMain = masterEmitter.main;
        masterMain.startLifetime = defaultLifetime; 

        var slaveMain = slaveEmitter.main;
        slaveMain.duration = defaultLifetime; 

        // Restart the particle systems to apply the changes immediately
        masterEmitter.Stop();
        masterEmitter.Play();

        slaveEmitter.Stop();
        slaveEmitter.Play();

        moreSpirals.SetActive(false);

        //isWindActive = false;
        //canActivateAir = false;

    }

    //Method to remove the boundary when station has been completed.
    //Start through Unity event wrapper for when item to collect is selected.
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
        windObjectToCollect.SetActive(false);
    }

}
