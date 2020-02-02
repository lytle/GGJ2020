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
        RunAwayCat = 1,
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
                        Debug.Log("Normal");
                        break;
                    }
                case CatType.RunAwayCat:
                    {
                        Debug.Log("RunAway");
                        runAwayCat = true;
                        break;
                    }
            }
        }
    }


    void Start()
    {
        type = (CatType)1;
    }
    
    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        

        if(timer <= 0)
        {


            if(!uwuCat)
            randomVec = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0f);
            
            ChangeDir(transform.position.x < randomVec.x);

            if (runAwayCat && (transform.position - PlayerControl.instance.transform.position).sqrMagnitude < 20f)
            {
                //ChangeDir(PlayerControl.instance.facingRight);
                randomVec += PlayerControl.instance.transform.right * sideDirection * 5f;
            }

            if(uwuCat && (transform.position - PlayerControl.instance.transform.position).sqrMagnitude > 20f)
            {
                //ChangeDir(!PlayerControl.instance.facingRight);
                randomVec = PlayerControl.instance.transform.position + new Vector3(Random.Range(0f,1f), Random.Range(0f,1f), 0);
            }

            timer = Random.Range(0f, 3f);
            
        }


        //transform.position = Vector3.Lerp(transform.position, randomVec, Time.deltaTime * speed );

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

    private IEnumerator MakeBabyBigger()
    {
        while(transform.localScale.y < 1)
        {
            GetComponentInChildren<Cat>().transform.localScale = Vector3.Lerp(GetComponentInChildren<Cat>().transform.localScale, Vector3.one, 0.3f);
            yield return new WaitForEndOfFrame();
        }
    }

    private void CheckWithinRadius(float radius)
    {

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
