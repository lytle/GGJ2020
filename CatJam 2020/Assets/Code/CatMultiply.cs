using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMultiply : MonoBehaviour
{
    private PolygonCollider2D catToCat;
    public bool makeBaby;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(makeBaby && other.tag.Equals("Cat"))
        {
            other.gameObject.GetComponent<CatMultiply>().makeBaby = false;
            GameObject me = transform.parent.gameObject;
            GameObject them = other.transform.parent.gameObject;
            them.SetActive(false);

            GameManager.singleton.makeNewCat(transform, me, them);
        }
    }

    public void CalmCat()
    {
        makeBaby = false;
        StartCoroutine("MakeBabies");
    }

    IEnumerator MakeBabies()
    {
        yield return new WaitForSeconds(3f);
        makeBaby = true;
    }
}
