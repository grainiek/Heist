using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInteractWithObjects : MonoBehaviour {

    [SerializeField] float dist = 1f;
    [SerializeField] LayerMask layerMask;

    private Camera mainCam;
    private RaycastHit hit;

    // Use this for initialization
    void Start () {
        mainCam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.TransformDirection(Vector3.forward));
        //Debug.DrawRay(mainCam.transform.position, mainCam.transform.TransformDirection(Vector3.forward), Color.green);
        
        if (Physics.Raycast(ray, out hit, dist, layerMask))
        {            
            if (hit.collider.tag == "Interactable")
            {
                hit.transform.GetComponent<IsInteracting>().isHovering = true;
                hit.transform.GetComponent<IsInteracting>().t = 0.1f;

                if (CrossPlatformInputManager.GetButtonDown("Interact"))
                {
                    hit.transform.GetComponent<IsInteracting>().isInteracting = true;
                }
            }
        }

    }
}
