using UnityEngine;
using System.Collections;

public class IsInteracting : MonoBehaviour {

    public bool isInteracting = false;
    public bool isHovering = false;
    public float t = 0f;
    public GameObject meshVisualizer;
    //public bool initiateInteracted = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (isHovering)
        {
            meshVisualizer.GetComponent<Renderer>().material.color = new Vector4(1, 1, 1, 1);
        }
        if (t < 0)
        {
            meshVisualizer.GetComponent<Renderer>().material.color = new Vector4(0.5f, 0.5f, 0.5f, 1);
            t = 0;
        }
        t = t - Time.deltaTime;
	}
}
