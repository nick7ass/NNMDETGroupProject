using Meta.Voice.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollisionAudioScript : MonoBehaviour
{
    public AudioSource audioPlayerFire;
    public AudioSource audioPlayerGround;
    public GameObject fireBigger;
    public GameObject fireEvenBigger;

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
            isFireBigger = true;
            boundFireScript.CollectFireObject();
                
        } /*else if (collision.gameObject.tag == "CampFireTag" && isFireBigger)
        {
            fireEvenBigger.SetActive(true);
        }*/
    }
}
