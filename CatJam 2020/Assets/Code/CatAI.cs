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

    private bool runAwayCat, uwuCat;


    public enum CatType
    {
        NormalCat = 0,
        LazyCat = 1,
        RunAwayCat = 2,
        UwuCat = 3,
        BabyCat = 4
    }

    private CatType _type;

    public CatType type
    {
        get { return _type; }
        set
        {
            _type = value;
            switch (_type)
            {
                case CatType.NormalCat:
                    {
                        break;
                    }
                case CatType.LazyCat:
                    {
                        speed /= 2;
                        break;
                    }
                case CatType.RunAwayCat:
                    {
                        runAwayCat = true;
                        break;
                    }
                case CatType.UwuCat:
                    {
                        uwuCat = true;
                        break;
                    }
                case CatType.BabyCat:
                    {
                        transform.localScale /= 2;
                        break;
                    }
            }
        }
    }


    void Start()
    {
        type = (CatType) Random.Range(0, 4);
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

            if (runAwayCat && (transform.position - PlayerControl.instance.transform.position).sqrMagnitude < 20f)
            {
                if (PlayerControl.instance.facingRight)
                {
                    sideDirection = 1;
                }
                else
                {
                    sideDirection = -1;
                }

                randomVec += PlayerControl.instance.transform.right * sideDirection * 10f;
            }

            if(uwuCat && (transform.position - PlayerControl.instance.transform.position).sqrMagnitude > 20f)
            {
                if (PlayerControl.instance.facingRight)
                {
                    sideDirection = -1;
                }
                else
                {
                    sideDirection = 1;
                }

                randomVec = PlayerControl.instance.transform.position;
            }

            timer = Random.Range(0, 1.5f);
            
        }
        
        transform.localScale = new Vector3(sideDirection * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        //Vector3 moveTow = Vector3.MoveTowards(transform.position, randomVec, speed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, randomVec, speed * Time.deltaTime);

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

    private IEnumerator MakeBabyBigger()
    {
        while(transform.localScale.y < 1)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.3f);
            yield return new WaitForEndOfFrame();
        }
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
