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

    private Vector2 initDir, finalDir;

    private float curveTimerMax;

    float timer = 2f;

    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = Random.Range(0, 4f);
            curveTimerMax = timer;
            initDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            finalDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        Vector2 curDir = Vector2.Lerp(initDir, finalDir, timer / curveTimerMax);
        

        if (!DetectSideObstacle())
        {
            //transform.position += ((transform.right * sideDirection + transform.up * upDownDirection ))/2 * speed * Time.deltaTime;
            transform.Translate(curDir * speed * Time.deltaTime);
        }
        else
        {
            sideDirection *= -1;        //CHANGE DIRECTION
            FlipCat(); 
        }


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
                Debug.Log("HIT TOP/BOTTOM");
            }
        }

        foreach (RaycastHit2D hit in sideHits)
        {
            if (hit.transform.CompareTag("Wall"))
            {
                Debug.Log("HIT SIDE");
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
