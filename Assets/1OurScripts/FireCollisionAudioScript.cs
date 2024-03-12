using Meta.Voice.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollisionAudioScript : MonoBehaviour
{
    public ParticleSystem fireAlpha; // Assign in the inspector
    public ParticleSystem fireAdd; // Assign in the inspector
    public ParticleSystem fireGlow; // Assign in the inspector
    public ParticleSystem fireSparks; // Assign in the inspector

    public AudioSource audioPlayerFire;
    public AudioSource audioPlayerGround;
    public GameObject fireBigger;

    public BoundFireScript boundFireScript;

    private bool isFireBigger = false;


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GroundTag") // || collision.gameObject.tag == "BrickTag"
        {
            audioPlayerGround.Play();
        }
        else if (collision.gameObject.tag == "CampFireTag" && !isFireBigger && boundFireScript.narrationHasFinished) {
            audioPlayerFire.Play(); //Add more dramatic audio
            fireBigger.SetActive(true);
            resetParticles();
            isFireBigger = true;
            boundFireScript.CollectFireObject();

                
        } 
    }

    private void resetParticles()
    {
        fireAlpha.Stop();
        fireAlpha.Play();
        fireAdd.Stop();
        fireAdd.Play();
        fireGlow.Stop();
        fireGlow.Play();
        fireSparks.Stop();
        fireSparks.Play();
    }
}
