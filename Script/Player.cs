using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 targetPos, OldPos;
    public bool Jump = false;

    void Start()
    {
        targetPos = transform.position;
    }

    public void Move(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.up)
            targetPos += moveDirection;
        if (moveDirection == Vector3.up && Jump == false)
        {
            GetComponent<Animator>().SetBool("jump", true);
             StartCoroutine(Down());
            //  Jump = true;
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    IEnumerator Down()
    {
        yield return new WaitForSeconds(.2f);
        GetComponent<Animator>().SetBool("jump", false);
        Debug.Log("DOWN");

      //  StartCoroutine(Down1());
    }


    IEnumerator Down1()
    {
        yield return new WaitForSeconds(.01f);
        if (transform.position.y > 0)
        {
            targetPos = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            // Jump = false;
            StartCoroutine(Down1());
        }
        else
        {
            GetComponent<Animator>().SetBool("jump", false);
            targetPos = new Vector3(transform.position.x, 0f, transform.position.z);
            Jump = false;
        }

    }

}
