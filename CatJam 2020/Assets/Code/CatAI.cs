using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAI : MonoBehaviour
{

    public float maxDistance;

    public float speed;

    private bool facingRight;

    private int sideDirection = -1;     // 1 is RIGHT and -1 is LEFT
    private int upDownDirection = 1;    // 1 is UP and -1 is DOWN

    private bool runningAround;

    private Vector3 randomVec;

    private float curveTimerMax;

    float timer = 0f;

    public Vector2 min, max;



    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            randomVec = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0f);
            if (transform.position.x < randomVec.x)
            {
                sideDirection = 1;
            }
            else
            {
                sideDirection = -1;
            }

            transform.localScale = new Vector3(sideDirection * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            timer = Random.Range(0, Mathf.Abs(randomVec.magnitude)/2);
            
        }
        
        Vector3 moveTow = Vector3.MoveTowards(transform.position, randomVec, speed * Time.deltaTime);

        transform.position = Vector3.Slerp(transform.position, moveTow, Time.deltaTime);

/*        if (!DetectSideObstacle())
        {
            *//*
            //transform.position += ((transform.right * sideDirection + transform.up * upDownDirection ))/2 * speed * Time.deltaTime;
            transform.Translate((transform.right * sideDirection + curDir * upDownDirection)/2 * speed * Time.deltaTime);
            *//*
        }

        else
        {
            sideDirection *= -1;        //CHANGE DIRECTION
            FlipCat(); 
        }*/


    }


    public bool DetectSideObstacle()
    {
        RaycastHit2D[] sideHits = Physics2D.RaycastAll(transform.position, transform.right * sideDirection, maxDistance);
        RaycastHit2D[] topHits = Physics2D.RaycastAll(transform.position, transform.up * upDownDirection, maxDistance);


        Debug.DrawRay(transform.position, transform.right * maxDistance * sideDirection, Color.blue);

        Debug.DrawRay(transform.position, transform.up * maxDistance * upDownDirection, Color.red);


        foreach (RaycastHit2D hit in topHits)
        {
            if (hit.transform.CompareTag("Wall"))
            {
                upDownDirection *= -1;
            }
        }

        foreach (RaycastHit2D hit in sideHits)
        {
            if (hit.transform.CompareTag("Wall"))
            {
                return true;
            }
        }
        return false;
    }

    

    public void FlipCat()
    {
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }


}
