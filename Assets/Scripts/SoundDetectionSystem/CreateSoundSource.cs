using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class CreateSoundSource : MonoBehaviour {

    [SerializeField] private AudioClip[] m_FootstepSoundsStone;    // an array of footstep sounds that will be randomly selected from.
    [SerializeField] private AudioClip[] m_FootstepSoundPlastic;    // an array of footstep sounds that will be randomly selected from.
    [SerializeField] private AudioClip[] m_FootstepSoundsCloth;    // an array of footstep sounds that will be randomly selected from.
    [SerializeField] private AudioClip[] m_FootstepSoundsWood;    // an array of footstep sounds that will be randomly selected from.
    [SerializeField] private AudioClip m_LandSound;
    [SerializeField] GameObject soundVolumeSphere;

    [SerializeField]    float woodRange = 1f;
    [SerializeField]    float stoneRange = 1f;
    [SerializeField]    float plasticRange = 1f;
    [SerializeField]    float clothRange = 1f;

    public float soundState = 1f; 

    private AudioSource m_AudioSource;
    private string groundType = "none";

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void CreateSoundCollider ()
    {
        float speedMul = GetComponent<FirstPersonController>().speed / GetComponent<FirstPersonController>().m_WalkSpeed;
        GetComponent<AudioSource>().volume = 0.5f * speedMul;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, 1.0f))
        {
            if (hit.collider.tag == "Wood") { groundType = "wood"; }
            if (hit.collider.tag == "Stone") { groundType = "stone"; }
            if (hit.collider.tag == "Plastic") { groundType = "plastic"; }
            if (hit.collider.tag == "Cloth") { groundType = "cloth"; }
        }

        GameObject latestSphere = (GameObject) Instantiate(soundVolumeSphere, transform.position, Quaternion.identity);

        if (soundState == 1) //Walking/Running
        {
            if (groundType == "wood")
            {
                int n = Random.Range(1, m_FootstepSoundsWood.Length);
                m_AudioSource.clip = m_FootstepSoundsWood[n];
                m_AudioSource.PlayOneShot(m_AudioSource.clip);

                m_FootstepSoundsWood[n] = m_FootstepSoundsWood[0];
                m_FootstepSoundsWood[0] = m_AudioSource.clip;
                latestSphere.transform.localScale = new Vector3(woodRange* speedMul, woodRange* speedMul, woodRange* speedMul);
            }
            if (groundType == "stone")
            {
                int n = Random.Range(1, m_FootstepSoundsStone.Length);
                m_AudioSource.clip = m_FootstepSoundsStone[n];
                m_AudioSource.PlayOneShot(m_AudioSource.clip);

                m_FootstepSoundsStone[n] = m_FootstepSoundsStone[0];
                m_FootstepSoundsStone[0] = m_AudioSource.clip;
                latestSphere.transform.localScale = new Vector3(stoneRange* speedMul, stoneRange* speedMul, stoneRange* speedMul);
            }
            if (groundType == "plastic")
            {
                int n = Random.Range(1, m_FootstepSoundPlastic.Length);
                m_AudioSource.clip = m_FootstepSoundPlastic[n];
                m_AudioSource.PlayOneShot(m_AudioSource.clip);

                m_FootstepSoundPlastic[n] = m_FootstepSoundPlastic[0];
                m_FootstepSoundPlastic[0] = m_AudioSource.clip;
                latestSphere.transform.localScale = new Vector3(plasticRange* speedMul, plasticRange* speedMul, plasticRange* speedMul);
            }
            if (groundType == "cloth")
            {
                int n = Random.Range(1, m_FootstepSoundsCloth.Length);
                m_AudioSource.clip = m_FootstepSoundsCloth[n];
                m_AudioSource.PlayOneShot(m_AudioSource.clip);

                m_FootstepSoundsCloth[n] = m_FootstepSoundsCloth[0];
                m_FootstepSoundsCloth[0] = m_AudioSource.clip;
                latestSphere.transform.localScale = new Vector3(clothRange* speedMul, clothRange* speedMul, clothRange* speedMul);
            }
        }
        if (soundState == 2) //Landing from a jump
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            //m_NextStep = m_StepCycle + .5f;
            latestSphere.transform.localScale = new Vector3(stoneRange, stoneRange, stoneRange);
        }

    }
}
