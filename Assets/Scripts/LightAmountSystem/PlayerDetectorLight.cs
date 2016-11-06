using UnityEngine;
using System.Collections;

public class PlayerDetectorLight : MonoBehaviour {

    private GameObject player;
    private Camera mainCam;
    [SerializeField]  private GameObject seePlayerVisualizer; 
    public float lightTolerance;
    public float playerDistanceTolerance;
    public float dectectionTimeTolerance;
    public LayerMask layerMask;
    // private float pleyerDetected = 0;
    public float distanceXLight = 0.5f;
    public float fowTolerance = 0.2f; 

    private float detectTimer;
    private bool playerOutOfRange = true;
    public float playerLumDist; 

    public bool isSeeingPlayer = false;
    public bool playerIsBehindWall = false;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        mainCam = Camera.main; 
    }
	
	// Update is called once per frame
	void Update () {

        float dist = Vector3.Distance(player.transform.position, transform.position);

        float colRange = player.GetComponent<LightColisionDetection>().colRange;
        float colSunHit = player.GetComponent<LightColisionDetection>().colSunHit;
        float playerLightIntensity = colRange + colSunHit;
        float seePlayer = 0;

        float fieldOfView = Vector3.Dot(mainCam.transform.position - transform.position, transform.TransformDirection(Vector3.forward));

        if (playerDistanceTolerance < dist) { playerOutOfRange = true; }
        else { playerOutOfRange = false; }
        
        if (Physics.Raycast(transform.position, (mainCam.transform.position - transform.position), dist, layerMask) == false && fieldOfView > fowTolerance)
        {
            Debug.DrawRay(transform.position, (mainCam.transform.position - transform.position), Color.green);         
            
            if (dist > 0)
            {
                playerLumDist = playerLightIntensity / (dist * distanceXLight);
            }


            if (playerLumDist > lightTolerance && playerOutOfRange == false)
            {
                seePlayerVisualizer.gameObject.GetComponent<Renderer>().material.color = new Vector4(0, 1, 0, 0);
                // pleyerDetected = pleyerDetected + dectectionTimeTolerance;
                // if (pleyerDetected > 100) { pleyerDetected = 100; }
                seePlayer = 1;
                isSeeingPlayer = true;
            }
            else
            {
                seePlayerVisualizer.gameObject.GetComponent<Renderer>().material.color = new Vector4(0, 0, 0, 0);
                isSeeingPlayer = false;
                // pleyerDetected = pleyerDetected - (dectectionTimeTolerance * 0.25f);
                // if (pleyerDetected < 0) { pleyerDetected = 0; }
                seePlayer = 0;
            }
            playerIsBehindWall = false;
        }
        else
        {
            // pleyerDetected = pleyerDetected - (dectectionTimeTolerance*0.25f);
            // if (pleyerDetected < 0) { pleyerDetected = 0; }
            seePlayerVisualizer.gameObject.GetComponent<Renderer>().material.color = new Vector4(0, 0, 0, 0);
            isSeeingPlayer = false;
            playerIsBehindWall = true;
        }
        // if (pleyerDetected >= 10)
        // {
        //     gameObject.GetComponent<Renderer>().material.color = new Vector4(1, (pleyerDetected*0.02f), 0, seePlayer);
        // }
        // else
        // {
        //     gameObject.GetComponent<Renderer>().material.color = new Vector4(0.0f, 0.3f, seePlayer, 0);
        // }
    }
}
