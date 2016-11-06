using UnityEngine;
using System.Collections;

public class AiStateHandeler : MonoBehaviour {

    private GameObject player;

    [Header("What task shal be enabled for the guard?")]
    public bool patrol;
    public bool idle;
    public bool watchCam;
    public bool hunger;
    public bool toiletNeed;

    [Space(10)]
    [Header("Time to continue searching after hering something")]
    public float searchForPlayerTimer = 5f;
    [Space(10)]
    public float walkSpeed = 4f; 
    public float runSpeed = 8f; 

    [Header("Above this limit, the guard has agro on player")]

    public float agroLimit = 10f;
    public float agroCurrent = 0f;
    [Header("When alert, sense near player without sound or light")]
    public float nearSenseDist = 6f; 

    [SerializeField] private AudioClip[] m_IHearYouAudio;
    [SerializeField] private AudioClip[] m_ISeeYouAudio;

    private float timer = 0;
    private int defaultState = 0;
    private float searchAreaDist = 0;
    private Vector3 currentWalkTarget = new Vector3(0,0,0);
    private Vector3 lastKnownPlayerPos = new Vector3(0,0,0);
    private AudioSource m_AudioSource;

    public int aiCurrentState = 1;
    //1 - patroling
    //2 - searching for sound source - I heard something
    //3 - player found, apprehend player
    //4 - Seen player, currently looking to find player again. 

    PlayerDetectorLight seePlayer;
    DetectSoundSource hearPlayer;
    NavMeshAgent agent;


	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");

        seePlayer = GetComponent<PlayerDetectorLight>();
        hearPlayer = GetComponent<DetectSoundSource>();
        agent = GetComponent<NavMeshAgent>();
        defaultState = aiCurrentState;
        m_AudioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (aiCurrentState == 1)   //1 - patroling
        {
            GetComponent<NavmeshPathFinding>().Patrol();
            HearPlayerFunction();
            SeePlayerFuntion ();
            agent.speed = walkSpeed;
            GetComponent<Renderer>().material.color = new Vector4(0.1f, 0.1f, 0.1f, 0);
        }

        if (aiCurrentState == 2)    //2 - searching for sound source - I heard something
        {
            HearPlayerFunction();
            SeePlayerFuntion ();
            if (timer > 0) { timer = timer - Time.deltaTime; }
            if (timer <= 0) { aiCurrentState = defaultState; }
            if (hearPlayer.isHearing == true) {currentWalkTarget = hearPlayer.latestSoundSource.transform.position;}
            agent.speed = walkSpeed;
            GetComponent<Renderer>().material.color = new Vector4(0, 0.5f, 0, 0);
        }
        if (aiCurrentState == 3)    //3 - player found, apprehend player
        {
            agent.speed = runSpeed;
            SeePlayerFuntion ();
            // SenseNearPlayerFunction ();
            currentWalkTarget = lastKnownPlayerPos;
            agent.SetDestination(currentWalkTarget);
            GetComponent<Renderer>().material.color = new Vector4(1, 0, 0, 0);
            // if (Vector3.Distance(currentWalkTarget, transform.position) > 1){aiCurrentState = 4;}
        }

        if (aiCurrentState == 4)    //4 - Seen player, currently looking to find player again. 
        {
            HearPlayerFunction ();
            SeePlayerFuntion ();
            // SenseNearPlayerFunction ();
            SearchAreaPlayerFunction ();
            GetComponent<Renderer>().material.color = new Vector4(1, 1, 0, 0);
        }
	}

    void HearPlayerFunction () {
        if (hearPlayer.isHearing == true)
        {
            timer = searchForPlayerTimer;
            aiCurrentState = 2;                
            agent.SetDestination(currentWalkTarget);
            //PLAY HEAR SOMETHING SOUND
            if (m_AudioSource.isPlaying == false && m_IHearYouAudio.Length > 0)
            {
                m_AudioSource.clip = m_IHearYouAudio[Random.Range(0,m_IHearYouAudio.Length)];
                m_AudioSource.PlayOneShot(m_AudioSource.clip);
            }
        }
    }

    void SeePlayerFuntion () {
        if (seePlayer.isSeeingPlayer == true)
        {
            lastKnownPlayerPos = player.transform.position;
            agroCurrent = agroCurrent + seePlayer.playerLumDist;
            //PLAY SEE PLAYER SOUND
            if (m_AudioSource.isPlaying == false && m_ISeeYouAudio.Length > 0)
            {
                m_AudioSource.clip = m_ISeeYouAudio[Random.Range(0,m_ISeeYouAudio.Length)];
                m_AudioSource.PlayOneShot(m_AudioSource.clip);
            }
            // Agro Controls
            if (agroCurrent > agroLimit)
            {
                aiCurrentState = 3;
            }
        }
    }

    void SenseNearPlayerFunction () {
        float distToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distToPlayer < nearSenseDist && seePlayer.playerIsBehindWall == false)
        {
            lastKnownPlayerPos = player.transform.position;
            aiCurrentState = 3;
        }
    }

    void SearchAreaPlayerFunction () {
        float distToWalkTarget = Vector3.Distance(currentWalkTarget, transform.position);
        if (distToWalkTarget < 1)
        {
            currentWalkTarget = new Vector3(lastKnownPlayerPos.x + searchAreaDist,lastKnownPlayerPos.y + searchAreaDist,lastKnownPlayerPos.z + searchAreaDist);
            searchAreaDist = searchAreaDist + 3f;
        }
    }
}