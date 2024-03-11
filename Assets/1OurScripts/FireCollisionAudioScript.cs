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

    private BoundFireScript boundFireScript;

    private bool isFireBigger = false;


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GroundTag") // || collision.gameObject.tag == "BrickTag"
        {
            audioPlayerGround.Play();
        }
        else if (collision.gameObject.tag == "CampFireTag" && !isFireBigger) {
            audioPlayerFire.Play();
            fireBigger.SetActive(true);
            isFireBigger = true;
            boundFireScript.fireObjectToCollect.SetActive(true);
        } /*else if (collision.gameObject.tag == "CampFireTag" && isFireBigger)
        {
            fireEvenBigger.SetActive(true);
        }*/
    }
}
