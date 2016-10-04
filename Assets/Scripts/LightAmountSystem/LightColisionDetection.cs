using UnityEngine;
using System.Collections;

public class LightColisionDetection : MonoBehaviour {

    public float colRange = 0;
    public float colSunHit;
    public Light[] lightArray;
    public GameObject sunLight;
    public int n = 0;
    public LayerMask layerMask;
    public GameObject amountVisualizer; 


    // Use this for initialization
    void Start () {
	    lightArray = new Light[20];
        sunLight = GameObject.Find("Directional Light");
    }

    void OnTriggerEnter(Collider col)
    {
        lightArray[n] = col.GetComponent<Light>();
        n++; 
        
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

                if (Physics.Raycast(transform.position, (lightArray[i].transform.position - transform.position), dist, layerMask) == true && colLight.shadows != LightShadows.None)
                {
                    colRange = colRange + 0; 
                }
                else
                {
                    if ((((colLight.range - dist) / colLight.range)) > 0)
                    {
                        colRange = colRange + (Mathf.Pow(((colLight.range - dist) / colLight.range), 2.3f) * (colLight.intensity));
                        
                    }
                }
            }
        }
        //colRange = colLight.range - dist;
        amountVisualizer.GetComponent<Renderer>().material.color = new Vector4(colRange, 0, 0, 0);
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
        colSunHit = 0;
        if (n == 20)
        {
            n = 0;
        }

        Quaternion sunQuat = sunLight.transform.rotation;
        Vector3 sunDir = sunQuat * Vector3.forward;

        if (Physics.Raycast(transform.position, (-sunDir), 100, layerMask) == true)
        {
            colSunHit = colSunHit + 0;
        }
        else
        {
            Light sunShadowCheck = sunLight.GetComponent<Light>();
            colSunHit = colSunHit + (sunShadowCheck.intensity);
        }

        if (colRange < 0) { colRange = 0; }
        if (colSunHit < 0) { colSunHit = 0; }
        amountVisualizer.GetComponent<Renderer>().material.color = new Vector4(colRange + colSunHit, 0, 0, 0);
    }
}
