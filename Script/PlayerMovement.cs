using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchPos;
    private Quaternion rotY;
    private float speedMod = 0.3f;
    public GameObject PopParticle;
    public TMP_Text killtxt;
    public GameObject KillsPopUp;
    public TMP_Text PopUpTxt;
    public string[] PopUpMessages;

    int killCount=0;

    [SerializeField] private GameObject lightObj = null;

    public bool emptyCharge = false;

    public static PlayerMovement instance;

    void Awake()
    {
        instance = this;
    }

    public void KillUpdate()
    {
        killCount++;
        killtxt.text = "KILL: " + killCount;

        if (killCount>0 && killCount%3==0)
        {
            StartCoroutine(showPopUp());
        }
    }

    IEnumerator showPopUp()
    {
        yield return new WaitForSeconds(3);
        PopParticle.SetActive(true);
        PopUpTxt.text = PopUpMessages[Random.Range(0, PopUpMessages.Length)];
        KillsPopUp.SetActive(true);
        Time.timeScale = 0.0001f;
        
    }

    public void ResetNum()
    {
        killCount = 0;
        killtxt.text = "KILL: 0";
    }

    public void OK()
    {
        PopParticle.SetActive(false);
        KillsPopUp.SetActive(false);
        Time.timeScale = 1;
    }


    // Update is called once per frame
    void Update()
    {

        if (!GameController.GamePaused)
        {
            if (Input.touchCount > 0 || Input.GetKey(KeyCode.Space))
            {
                if (Application.platform != RuntimePlatform.OSXEditor)
                    touch = Input.GetTouch(0);
                Debug.Log("Empty");
                if (!emptyCharge)
                {
                    lightObj.SetActive(true);
                }
                else
                {
                    lightObj.SetActive(false);
                }

                UIControlller.instance.usingCharge = true;

                if (Application.platform != RuntimePlatform.OSXEditor)
                {
                    if (touch.phase == TouchPhase.Moved)
                    {
                        rotY = Quaternion.Euler(0f, touch.deltaPosition.x * speedMod, 0f);
                        transform.rotation = rotY * transform.rotation;
                    }
                }
            }
            else
            {
                lightObj.SetActive(false);
                UIControlller.instance.usingCharge = false;

            }
        }
    }
}
