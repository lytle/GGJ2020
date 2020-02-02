using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMultiply : MonoBehaviour
{
    public bool makeBaby, smelly;

    [SerializeField]
    private float breedDelay = 3f;
    [SerializeField]
    private GameObject sexyAura, smellyAura;

    private Coroutine calmness;
    private Coroutine anticalmness;

    public bool pickedup;
    

    // Start is called before the first frame update
    void Start()
    {
        CalmCat();
        pickedup = false;
    }
    // Update is called once per frame
    void Update()
    {
    }

/*    private void OnCollisionEnter2D(Collision2D other)
    {

        if(other.gameObject.GetComponent<CatMultiply>() && makeBaby && other.gameObject.tag.Equals("Cat") && other.gameObject.GetComponent<CatMultiply>().makeBaby)
        {
            Debug.Log("Multiply");
            other.gameObject.GetComponent<CatMultiply>().makeBaby = false;
            GameObject me = transform.gameObject;
            GameObject them = other.transform.gameObject;
            them.SetActive(false);

            GameManager.singleton.makeNewCat(transform.position, me, them);
        }
    }*/

    public void CalmCat()
    {
        makeBaby = false;
        DisengageSexyAura();
        if(calmness != null)
        StopCoroutine(calmness);
        if(!smelly) calmness = StartCoroutine("BecomeSexy");
    }

    public void WashCat()
    {
        smelly = false;
        smellyAura.SetActive(false);
        calmness = StartCoroutine("BecomeSexy");
    }

    IEnumerator BecomeSexy()
    {
        float timeToSex = Random.Range(5f, 8f);
        yield return new WaitForSeconds(timeToSex);
        //Debug.Log("adding cat");
        if (Random.Range(0, 10f) < 3f)
        {
            makeBaby = false;
            DisengageSexyAura();
            smelly = true;
            smellyAura.SetActive(true);
        }
        else
        {
            makeBaby = true;
            EngageSexyAura();
            GameManager.singleton.AddHornyCat(this.gameObject);
            //Debug.Log("adding cat");

            if (anticalmness != null)
                StopCoroutine(EndSexy());
            if(!pickedup)
            anticalmness = StartCoroutine(EndSexy());
        }
    }

    IEnumerator EndSexy()
    {
        float timeToSex = Random.Range(6f, 15f);
        yield return new WaitForSeconds(timeToSex);
        CalmCat();
    }

    public void StopEndSexy()
    {
        if (anticalmness != null)
            StopCoroutine(anticalmness);
    }

    public void CallEndSexy()
    {
        if(makeBaby)
        {
            if (anticalmness != null)
                StopCoroutine(EndSexy());
            anticalmness = StartCoroutine(EndSexy());
        }
    }

    public void EngageSexyAura()
    {
        sexyAura.SetActive(true);
    }

    public void DisengageSexyAura()
    {
        sexyAura.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(smelly && collision.CompareTag("Fountain"))
        {
            Debug.Log("Washing");
            WashCat();
            Fountain.singleton.CatInMe();
        }
    }
}
