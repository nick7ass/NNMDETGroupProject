using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject TestFire;
    public GameObject TestAir;
    public GameObject TestEarth;
    public GameObject TestWater;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Use Yield return to like not make it start instantly????
    /*public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoundEarth")) //
        {
            Debug.Log("Entered Earth");
            TestEarth.SetActive(true);
            //Play narration and remove other temp
        }
        else if (other.CompareTag("BoundWater"))
        {
            Debug.Log("Entered Water");
            TestWater.SetActive(true);
            //Play narration and remove ot
        }
        else if (other.CompareTag("BoundFire"))
        {
            Debug.Log("Entered Fire");
            TestFire.SetActive(true);
            //Play narration and remove otehr temp
        }
        else if (other.CompareTag("BoundAir"))
        {
            Debug.Log("Entered Air");
            TestAir.SetActive(true);
            //Play narration and remove otehr temp
        }
    }*/
}
