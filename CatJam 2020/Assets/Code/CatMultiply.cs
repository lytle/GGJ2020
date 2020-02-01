using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMultiply : MonoBehaviour
{
    private PolygonCollider2D catToCat;
    public bool makeBaby;
    [SerializeField]
    private float breedDelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        catToCat = GetComponent<PolygonCollider2D>();
        CalmCat();
    }
    private void OnEnable()
    {
        CalmCat();
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if(other.gameObject.GetComponent<CatMultiply>() && makeBaby && other.gameObject.tag.Equals("Cat") && other.gameObject.GetComponent<CatMultiply>().makeBaby)
        {
            other.gameObject.GetComponent<CatMultiply>().makeBaby = false;
            GameObject me = transform.gameObject;
            GameObject them = other.transform.gameObject;
            them.SetActive(false);

            GameManager.singleton.makeNewCat(transform.position, me, them);
        }
    }

    public void CalmCat()
    {
        makeBaby = false;
        StartCoroutine("MakeBabies");
    }

    IEnumerator MakeBabies()
    {
        yield return new WaitForSeconds(breedDelay);
        makeBaby = true;
    }
}
