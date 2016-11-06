using UnityEngine;
using System.Collections;

public class DetectSoundSource : MonoBehaviour {


    public float colRange = 0;
    public LayerMask layerMask;
    public GameObject amountVisualizer;
    public float rotSpeed = 1;
    public Collider latestSoundSource;

    // [SerializeField] private AudioClip[] m_IHearYouAudio; 

    private Collider[] soundArray;
    private int n = 0;
    // private AudioSource m_AudioSource;
    public bool isHearing = false; 
    private float t = 0;


    // Use this for initialization
    void Start()
    {
        soundArray = new Collider[20];
        // m_AudioSource = GetComponent<AudioSource>();
    }


    void OnTriggerEnter(Collider col)
    {
        soundArray[n] = col.GetComponent<Collider>();
        n++;
        colRange ++;
        latestSoundSource = col;
    }


    void OnTriggerStay(Collider col)
    {
        for (int i = 0; i < 20; i++)
        {
            if (soundArray[i] != null)
            {
                //float dist = Vector3.Distance(soundArray[i].transform.position, transform.position);
                amountVisualizer.GetComponent<Renderer>().material.color = new Vector4(0, 1, 0, 0);               
            }
        }
        t = 0.1f;
        //Vector3 targetDir = col.transform.position - transform.position;
        //float step = rotSpeed * Time.deltaTime;
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        //Debug.DrawRay(transform.position, newDir, Color.red);
        //transform.rotation = Quaternion.LookRotation(newDir);   
    }


    void OnTriggerExit(Collider col)
    {
        colRange--;
        for (int i = 0; i < 20; i++)
        {
            if (soundArray[i] == col.gameObject)
            {
                soundArray[i] = null;
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (n == 20)
        {
            n = 0;
        }

        if (colRange > 0)
        {
            isHearing = true;
        }
        else
        {
            isHearing = false; 
        }
        if (t <= 0){isHearing = false;}
        else {t = t - Time.deltaTime;}

        if (isHearing == true)
        {
            amountVisualizer.GetComponent<Renderer>().material.color = new Vector4(0, 1, 0, 0);
        //     if (m_AudioSource.isPlaying == false && m_IHearYouAudio.Length > 0)
        //     {
        //         m_AudioSource.clip = m_IHearYouAudio[Random.Range(0,m_IHearYouAudio.Length)];
        //         m_AudioSource.PlayOneShot(m_AudioSource.clip);
        //     }

        }
        else
        {
            amountVisualizer.GetComponent<Renderer>().material.color = new Vector4(0, 0, 0, 0);
        }
    }
}
