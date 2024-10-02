using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 input;
    private bool isMoving;
    private Animator myAni;

    public LayerMask soildObject;
    public LayerMask longGrass;

    private void Start()
    {
        myAni = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero) 
            {
                var tagetPos = transform.position;
                tagetPos.x += input.x;
                tagetPos.y += input.y;
                myAni.SetFloat("MoveX", input.x);
                myAni.SetFloat("MoveY", input.y);
                if (isWalkable(tagetPos))
                    StartCoroutine(Move(tagetPos));
            }
        }
        myAni.SetBool("isMoving", isMoving);

        IEnumerator Move(Vector3 tagetPos)
        {
            isMoving = true;
            while ((tagetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, tagetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = tagetPos;
            isMoving = false;
            CheckForEncounter();
        }
    }
    private bool isWalkable(Vector3 tagetPos)
    {
        if (Physics2D.OverlapCircle(tagetPos,0.1f,soildObject) != null)
        {
            return false;
        }    
        return true;
    }
    void CheckForEncounter()
    {
        if(Physics2D.OverlapCircle(transform.position,0.1f,longGrass) != null)
        {
            if (Random.Range(1,101) <= 10)
            { 
                Debug.Log("Encountered wild pokemon !");
            }   
        }
    }
}
