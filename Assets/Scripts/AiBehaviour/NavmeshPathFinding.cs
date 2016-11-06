using UnityEngine;
using System.Collections;

public class NavmeshPathFinding : MonoBehaviour
{

    NavMeshAgent agent;
    [SerializeField] GameObject patrolPathStart;
    [SerializeField] float distanceToTargetTolerance;

    bool needDestination = true;
    private Vector3 currentWalkTarget;
    private float updateTimer;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    public void Patrol()
    {
        updateTimer = updateTimer + Time.deltaTime;

        if (needDestination || updateTimer > 10)
        {
            currentWalkTarget = patrolPathStart.transform.position;
            needDestination = false;
            updateTimer = 0;
        }
        agent.SetDestination(currentWalkTarget);

        float dist = Vector3.Distance(patrolPathStart.transform.position, transform.position);

        if (dist < distanceToTargetTolerance)
        {
            needDestination = true;
            patrolPathStart = patrolPathStart.GetComponent<PatrolPathPoint>().nextTarget;
            //print(patrolPathStart.GetComponent<PatrolPathPoint>().nextTarget);
        }
    }
}
