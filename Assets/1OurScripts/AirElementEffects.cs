using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirElementEffects : MonoBehaviour
{ /*
    public ParticleSystem masterEmitter; // Assign in inspector
    public ParticleSystem slaveEmitter; // Assign in inspector
    public Animator handAnimator; // Assign in inspector

    private float defaultLifetime = 1.0f; // Default start lifetime, adjust as needed

    void Start()
    {
        // Optionally, initialize defaultLifetime with the current value from the particle system
        defaultLifetime = masterEmitter.main.startLifetime.constant;
    }

    void Update()
    {
        // Check if the hand gesture animation is playing by checking the Animator's boolean parameter
        if (IsHandGestureDetected())
        {
            AdjustParticleSpeed(2.0f); // Increase speed for the duration of the gesture
            StartCoroutine(ResetParticleSpeedAfterGesture(2.0f)); // Assuming gesture lasts for 2 seconds
        }
    }

    void AdjustParticleSpeed(float newLifetime)
    {
        var masterMain = masterEmitter.main;
        masterMain.startLifetime = newLifetime; // Adjust master emitter lifetime

        var slaveMain = slaveEmitter.main;
        slaveMain.duration = newLifetime; // Adjust slave emitter duration to match

        // Restart the particle systems to apply the changes immediately
        masterEmitter.Stop();
        masterEmitter.Play();
        slaveEmitter.Stop();
        slaveEmitter.Play();
    }

    

    


}*/



















    public ParticleSystem masterEmitter; // Assign in the inspector
    public ParticleSystem slaveEmitter; // Assign in the inspector

    
    public GameObject moreSpirals;
    private float defaultLifetime = 0.5f; // Default start lifetime, adjust as needed
    public float fasterLifetime = 2.0f; // Example faster lifetime, adjust as needed
    public bool isWindActive = false;

    void Start()
    {
        // Optionally, initialize defaultLifetime with the current value from the particle system
        defaultLifetime = masterEmitter.main.startLifetime.constant;
    }

    public void AdjustParticleSpeed()
    {
        if (!isWindActive)
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

    }

}

