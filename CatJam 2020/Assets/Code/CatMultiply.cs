using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMultiply : MonoBehaviour
{
    private Collider2D catToCat;
    public bool makeBaby;
    [SerializeField]
    private float breedDelay = 3f;
    private Coroutine calmness;
    // Start is called before the first frame update
    void Start()
    {
        catToCat = GetComponent<Collider2D>();
        CalmCat();
    }
    private void OnEnable()
    {
        Debug.Log("WHAT");
        CalmCat();
    }
    // Update is called once per frame
    void Update()
    {
    }

/*    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collide with " + other.gameObject.name);
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
        Debug.Log("CALM");
        makeBaby = false;
        if(calmness != null)
        StopCoroutine(calmness);
        calmness = StartCoroutine("BecomeSexy");
    }

    IEnumerator BecomeSexy()
    {
        float timeToSex = Random.Range(5f, 14f);
        yield return new WaitForSeconds(timeToSex);
        makeBaby = true;
        GameManager.singleton.AddHornyCat(this.gameObject);
        Debug.Log("adding cat");
    }
}
