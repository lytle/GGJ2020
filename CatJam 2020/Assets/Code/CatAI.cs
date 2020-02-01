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
    


    void Start()
    {
        
    }
    
    void Update()
    {
        Vector3 randomFactor = new Vector3(0f, Random.Range(-1f, 1f), 0f);

        if (!DetectSideObstacle())
        {
            transform.position += ((transform.right * sideDirection + transform.up * upDownDirection))/2 * speed * Time.deltaTime;
        }
        else
        {
            sideDirection *= -1;        //CHANGE DIRECTION
            FlipCat();     //Horizontal
        }


    }


    public bool DetectSideObstacle()
    {
        RaycastHit2D[] sideHits = Physics2D.RaycastAll(transform.position, transform.right * sideDirection, maxDistance);
        RaycastHit2D[] topHits = Physics2D.RaycastAll(transform.position, transform.up * upDownDirection, maxDistance);


        Debug.DrawRay(transform.position, transform.right * maxDistance * sideDirection, Color.blue);

        foreach(RaycastHit2D hit in topHits)
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
