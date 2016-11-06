using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class SafeNumberLock : MonoBehaviour {

    // All Interactables has to have an Interacted class

    [SerializeField] GameObject numberLock;
    [SerializeField] float rotSpeed = 1;
    [SerializeField] bool randomizeCode = true;
    [SerializeField] Vector4 code = new Vector4(1,1,1,1);
    [SerializeField] GameObject door;
    [SerializeField] AudioClip stepSound;
    [SerializeField] AudioClip unlockStepSound;
    [SerializeField] AnimationClip openAnim;
    [SerializeField] AnimationClip closeAnim;

    private AudioSource[] audioSourceArray;
    private bool currentlyInteracting = false;
    private bool safeOpen = false;
    private bool dirLeft = true;
    private bool openedDirLeft = true;
    private bool needReverseDir = false;

    private int digitOne;
    private int digitTwo;
    private int digitThree;
    private int digitFour;
    private int digitsUnlocked = 0;
    private int prevDigit = 0;

    private int n = 0;

    void Awake()
    {
        audioSourceArray = new AudioSource[20];
        for (int i = 0; i < 20; i++)
        {
            audioSourceArray[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    // Use this for initialization
    void Start () {
        if (randomizeCode == true)
        {
            digitOne = Random.Range(2, 50);
            digitTwo = Random.Range(2, 50);
            digitThree = Random.Range(2, 50);
            digitFour = Random.Range(2, 50);

            code = new Vector4(digitOne, digitTwo, digitThree, digitFour);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (n > 19) { n = 0; }
        if (GetComponent<IsInteracting>().isInteracting == true)
        {
            currentlyInteracting = true;
            Interacted();
            GetComponent<IsInteracting>().isInteracting = false;
        }
        if (!currentlyInteracting) { return; }

        //Code for interaction here! ------------------------------------------

        //Calculate current digit.
        int currentDigit = Mathf.RoundToInt((numberLock.transform.eulerAngles.z/7.2f));

        numberLock.transform.Rotate(Vector3.forward, Input.GetAxis("Mouse X") * rotSpeed);

        if (digitsUnlocked == 0 && currentDigit == code.w)
        {
            audioSourceArray[n].clip = unlockStepSound;
            audioSourceArray[n].PlayOneShot(audioSourceArray[n].clip);
            n = n + 1;
            digitsUnlocked = 1;
            openedDirLeft = dirLeft;
            needReverseDir = true;
        }
        else if (digitsUnlocked == 1 && currentDigit == code.x)
        {
            audioSourceArray[n].clip = unlockStepSound;
            audioSourceArray[n].PlayOneShot(audioSourceArray[n].clip);
            n = n + 1;
            digitsUnlocked = 2;
            openedDirLeft = dirLeft;
            needReverseDir = true;
        }
        else if (digitsUnlocked == 2 && currentDigit == code.y)
        {
            audioSourceArray[n].clip = unlockStepSound;
            audioSourceArray[n].PlayOneShot(audioSourceArray[n].clip);
            n = n + 1;
            digitsUnlocked = 3;
            openedDirLeft = dirLeft;
            needReverseDir = true;
        }
        else if (digitsUnlocked == 3 && currentDigit == code.z)
        {
            audioSourceArray[n].clip = unlockStepSound;
            audioSourceArray[n].PlayOneShot(audioSourceArray[n].clip);
            n = n + 1;
            digitsUnlocked = 4;
            openedDirLeft = dirLeft;
            needReverseDir = true;
        }
        else if (currentDigit != prevDigit)
        {
            audioSourceArray[n].clip = stepSound;
            audioSourceArray[n].PlayOneShot(audioSourceArray[n].clip);
            n = n +1;
            if (needReverseDir)
            {
                if (prevDigit > currentDigit) { dirLeft = true; }
                if (prevDigit < currentDigit) { dirLeft = false; }
                if (dirLeft == openedDirLeft) { digitsUnlocked = 0; needReverseDir = false; }
                else if (dirLeft != openedDirLeft) {  needReverseDir = false; }
                //else { needReverseDir = false; }             
            }
        }
        //print(digitsUnlocked);
        //print(needReverseDir);

        if (digitsUnlocked == 4 && safeOpen == false)
        {
            safeOpen = true;
            GetComponent<Animation>().AddClip(openAnim, "Safe 01 Open");
            GetComponent<Animation>().Play("Safe 01 Open");
        }

        prevDigit = currentDigit;
        
        
        // End interaction by canceling-------------------------------------------
        if (CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            currentlyInteracting = false;
            GetComponent<IsInteracting>().isInteracting = false;
        }
    }

    void Interacted()
    {
        EventManager.TriggerEvent("PlayerStartInteract");
        currentlyInteracting = true;
    }
}
