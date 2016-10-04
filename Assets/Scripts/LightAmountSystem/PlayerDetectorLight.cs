using UnityEngine;
using System.Collections;

public class PlayerDetectorLight : MonoBehaviour {

    public GameObject player;
    public float lightTolerance;
    public float playerDistanceTolerance;
    public float dectectionTimeTolerance;
    public LayerMask layerMask;
    public float pleyerDetected = 0;
    public float distanceXLight = 0.5f;

    private float detectTimer;
    private bool playerOutOfRange = true;
    public float playerLumDist; 

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {

        float dist = Vector3.Distance(player.transform.position, transform.position);

        float colRange = player.GetComponent<LightColisionDetection>().colRange;
        float colSunHit = player.GetComponent<LightColisionDetection>().colSunHit;
        float playerLightIntensity = colRange + colSunHit;
        float seePlayer = 0; 

        
        if (Physics.Raycast(transform.position, (player.transform.position - transform.position), dist, layerMask) == false)
        {
            //Debug.DrawRay(transform.position, (player.transform.position - transform.position), Color.green);

            if (playerDistanceTolerance < dist) { playerOutOfRange = true; }
            else { playerOutOfRange = false; }

            if (dist > 0)
            {
                playerLumDist = playerLightIntensity / (dist * distanceXLight);
            }


            if (playerLumDist > lightTolerance && playerOutOfRange == false)
            {
                //gameObject.GetComponent<Renderer>().material.color = new Vector4(0, 0, 1, 0);
                pleyerDetected = pleyerDetected + dectectionTimeTolerance;
                if (pleyerDetected > 100) { pleyerDetected = 100; }
                seePlayer = 1;
            }
            else
            {
                //gameObject.GetComponent<Renderer>().material.color = new Vector4(0, 0, 0, 0);
                pleyerDetected = pleyerDetected - (dectectionTimeTolerance * 0.25f);
                if (pleyerDetected < 0) { pleyerDetected = 0; }
                seePlayer = 0;
            }
        }
        else
        {
            pleyerDetected = pleyerDetected - (dectectionTimeTolerance*0.25f);
            if (pleyerDetected < 0) { pleyerDetected = 0; }
        }
        if (pleyerDetected >= 10)
        {
            gameObject.GetComponent<Renderer>().material.color = new Vector4(1, (pleyerDetected*0.02f), 0, seePlayer);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = new Vector4(0.3f, 0.3f, seePlayer, 0);
        }
    }
}
