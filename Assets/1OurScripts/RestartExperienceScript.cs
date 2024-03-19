using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartExperienceScript : MonoBehaviour
{
    //Entering this boundary restarts the scene
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoundHMD"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        }
    }

}
