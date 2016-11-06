using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

    public float selfDestructTimer = 1.0f;
    public bool shrinkDestruct = true; 
    private float t = 0f;
    private float scale;

    void Start()
    {
        scale = transform.localScale.x;
    }


    void Update() {
	    
        if (shrinkDestruct == true)
        {
            if (transform.localScale.x > 0)
            {
                scale = scale - selfDestructTimer;
                transform.localScale = new Vector3(scale, scale, scale);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (t < selfDestructTimer)
            {
                t = t + Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
	}
}
