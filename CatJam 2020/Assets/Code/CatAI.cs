using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CatAI : MonoBehaviour
{
    

    public float speed;

    private bool facingRight;

    private int sideDirection = -1;     // 1 is RIGHT and -1 is LEFT

    private bool runningAround;

    private Vector3 randomVec;

    private float curveTimerMax;

    float timer = 0f;

    public Vector2 min, max;

    public bool canBeHorny = false;
    



    void Start()
    {
    }
    
    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        

        if(timer <= 0)
        {

            randomVec = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0f);

            if ((transform.position - PlayerControl.instance.transform.position).sqrMagnitude < 20f)
            {
                randomVec += PlayerControl.instance.transform.right * sideDirection * 5f;
            }
            ChangeDir(transform.position.x < randomVec.x);

            timer = Random.Range(0f, 6f);
            
        }

        if(Vector3.Distance(transform.position, randomVec) < 1f)
        {
            canBeHorny = !canBeHorny;
        }
        transform.position = Vector3.MoveTowards(transform.position, randomVec, speed * Time.deltaTime);

    }

    public void ChangeDir(bool right)
    {
        if (right)
        {
            sideDirection = 1;
        }
        else
        {
            sideDirection = -1;
        }

        transform.localScale = new Vector3(sideDirection * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

    }


    /*

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

    */
}
