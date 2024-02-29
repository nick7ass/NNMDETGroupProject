using Meta.Voice.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollisionAudioScript : MonoBehaviour
{
    public AudioSource audioPlayerFire;
    public AudioSource audioPlayerGround;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GroundTag") // || collision.gameObject.tag == "BrickTag"
        {
            audioPlayerGround.Play();
        }
        else if (collision.gameObject.tag == "CampFireTag") {
            audioPlayerFire.Play();
        }
    }
}
