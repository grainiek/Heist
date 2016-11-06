using UnityEngine;
using System.Collections;

public class PatrolPathPoint : MonoBehaviour {

    public GameObject nextTarget;
    public GameObject prevTarget;
    public string path;
    public int nrInPath;
    public bool first = false;
    public bool last = false;

    [SerializeField] bool debugDrawPath = false;


    // Use this for initialization
    void Start () {
        int prevInPath = nrInPath - 1;
        int nextInPath = nrInPath + 1;
        prevTarget = GameObject.Find(path + "(" + prevInPath.ToString() + ")");
        nextTarget = GameObject.Find(path + "(" + nextInPath.ToString() + ")");

        if (prevTarget == null) { first = true; }
        if (nextTarget == null) { last = true; }

        if (last) { nextTarget = GameObject.Find(path + "(" + "1" + ")"); }
        //print(path + "(" + nrInPath.ToString() + ")");

        if (debugDrawPath)
        {
            if (nextTarget != null) { Debug.DrawRay(transform.position, (nextTarget.transform.position - transform.position), Color.blue); }
            if (prevTarget != null) { Debug.DrawRay(transform.position, (prevTarget.transform.position - transform.position), Color.yellow);}
        }
    }
	
	// Update is called once per frame
	void Update () {

    }
}
