using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthColliderScript : MonoBehaviour
{

    public BoundEarthScript earthScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Hand") && earthScript.narrationHasFinished) //
        {
            //Removing other bounds temporarily
            

            //Play narration
            StartCoroutine(NarrationAndSignalCoroutine());
        }
    }

    IEnumerator NarrationAndSignalCoroutine()
    {
        yield return new WaitForSeconds(3);
        earthScript.collectForce();
    }
}
