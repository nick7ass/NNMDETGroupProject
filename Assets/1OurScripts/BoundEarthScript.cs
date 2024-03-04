using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundEarthScript : MonoBehaviour
{

    public GameObject TestEarth;
   
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
        if (other.CompareTag("BoundHMD")) //
        {
            Debug.Log("Entered Earth");
            TestEarth.SetActive(true);
            //Play narration and remove other temp
        }
    }

}
