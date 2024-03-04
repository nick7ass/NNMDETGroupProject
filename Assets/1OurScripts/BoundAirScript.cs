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
    private bool canActivateAir = false;

    public GameObject TestAir;
    public AudioSource audioSource;
    public AudioClip narrationClip;
    
    // Start is called before the first frame update
    void Start()
    {
        defaultLifetime = masterEmitter.main.startLifetime.constant;

    }

    

    //Use Yield return to like not make it start instantly????
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoundHMD")) //
        {
            Debug.Log("Entered Earth");
            TestAir.SetActive(true);

            //Play narration and remove other temp
            StartCoroutine(NarrationAndSignalCoroutine());
            canActivateAir = true;
        }
    }

    /*public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BoundHMD")) //
        {

        }
    }*/


    IEnumerator NarrationAndSignalCoroutine()
    {
        audioSource.PlayOneShot(narrationClip);
        yield return new WaitForSeconds(narrationClip.length);
        
    }






    //Air effects
    public void AdjustParticleSpeed()
    {
        if (!isWindActive && canActivateAir)
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
        }

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
        masterMain.startLifetime = defaultLifetime; // Adjust master emitter lifetime

        var slaveMain = slaveEmitter.main;
        slaveMain.duration = defaultLifetime; // Adjust slave emitter duration to match

        // Restart the particle systems to apply the changes immediately
        masterEmitter.Stop();
        masterEmitter.Play();

        slaveEmitter.Stop();
        slaveEmitter.Play();

        moreSpirals.SetActive(false);

        isWindActive = false;
        canActivateAir = false;

    }

}
