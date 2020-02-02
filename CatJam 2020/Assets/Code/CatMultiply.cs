﻿using System.Collections;
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
        sexyAura.SetActive(false);
        if(calmness != null)
        StopCoroutine(calmness);
        calmness = StartCoroutine("BecomeSexy");
    }

    IEnumerator BecomeSexy()
    {
        float timeToSex = Random.Range(5f, 10f);
        yield return new WaitForSeconds(timeToSex);
        makeBaby = true;
        sexyAura.SetActive(true);
        GameManager.singleton.AddHornyCat(this.gameObject);
        Debug.Log("adding cat");

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
}
