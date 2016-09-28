using UnityEngine;
using System.Collections;

public class LightColisionDetection : MonoBehaviour {

    public float colRange = 0; 
    public Light[] lightArray;
    public int n = 0; 

    // Use this for initialization
    void Start () {
	    lightArray = new Light[20];
    }

    void OnTriggerEnter(Collider col)
    {
        lightArray[n] = col.GetComponent<Light>();
        n++; 
        if (n == 20)
        {
            n = 0;
        }
    }

    void OnTriggerStay(Collider col)
    {
        
        //colLight.intensity = colLight.range - dist;

        colRange = 0;
        for (int i = 0; i < 20; i++)
        {
            if (lightArray[i] != null)
            {
                float dist = Vector3.Distance(lightArray[i].transform.position, transform.position);
                Light colLight = lightArray[i].GetComponent<Light>();
                colRange = colRange + ((colLight.range - dist)/colLight.range);
            }
             
        }

        //colRange = colLight.range - dist;
        gameObject.GetComponent<Renderer> ().material.color = new Vector4(colRange,0,0,0);
    }

    void OnTriggerExit(Collider col)
    {
        for (int i = 0; i < 20; i++)
        {
            if (lightArray[i] == col.GetComponent<Light>())
            {
                lightArray[i] = null;
            }

        }

    }

    // Update is called once per frame
    void Update () {
	
	}
}
