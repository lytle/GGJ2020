using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMultiply : MonoBehaviour
{
    public bool makeBaby;

    [SerializeField]
    private float breedDelay = 3f;
    [SerializeField]
    private GameObject sexyAura;

    private Coroutine calmness;
    private Coroutine anticalmness;

    // Start is called before the first frame update
    void Start()
    {
        CalmCat();
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
        calmness = StartCoroutine("BecomeSexy");
    }

    IEnumerator BecomeSexy()
    {
        float timeToSex = Random.Range(5f, 20f);
        yield return new WaitForSeconds(timeToSex);
        makeBaby = true;
        EngageSexyAura();
        GameManager.singleton.AddHornyCat(this.gameObject);
        //Debug.Log("adding cat");

        if (anticalmness != null)
            StopCoroutine(EndSexy());
        anticalmness = StartCoroutine(EndSexy());
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
}
