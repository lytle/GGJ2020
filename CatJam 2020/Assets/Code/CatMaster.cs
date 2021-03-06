﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMaster : MonoBehaviour
{
    CatMultiply breedingAparatus;
    CatAI catMove;

    private GameObject catSprite;

    // Start is called before the first frame update
    void Start()
    {
        catSprite = GameManager.singleton.GetNewCat();
        catSprite.transform.parent = this.gameObject.transform;
        catSprite.transform.localPosition = Vector3.zero;

        breedingAparatus = GetComponent<CatMultiply>();
        catMove = GetComponent<CatAI>();
    }

    // Update is called once per frame
    void Update()
    {
        // put visual updates here
    }

    public void GotPickedUp()
    {
        breedingAparatus.StopEndSexy();
        breedingAparatus.pickedup = true;
        DisableBreed();
        DisableMove();
        GetComponent<Collider2D>().enabled = false;
    }

    public void Throw(bool facingRight, float playerY)
    {
        // complete this
        StartCoroutine(BeingThrown(facingRight, playerY));
    }

    void DisableBreed()
    {
        breedingAparatus.enabled = false;
    }

    void DisableMove()
    {
        catMove.enabled = false;
    }

    void EnableBreed()
    {
        breedingAparatus.enabled = true;
    }

    void EnableMove()
    {
        catMove.enabled = true;
    }

    IEnumerator BeingThrown(bool facingRight, float playerY)
    {
        breedingAparatus.canBeWashed = true;
        float destinationX = 19.0f * (facingRight ? 1.0f : -1.0f);
        float gravity = 0.04f;

        while(this.transform.position.y > playerY) {
            this.transform.Translate(destinationX * Time.deltaTime, -12.0f * gravity * Time.deltaTime, 0.0f);
            gravity += 14.0f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        GetComponent<Collider2D>().enabled = true;
        EnableBreed();
        EnableMove();
        breedingAparatus.pickedup = false;
        StartCoroutine(NoLongerWashable());
        yield return null;
    }

    IEnumerator NoLongerWashable()
    {
        yield return new WaitForSeconds(1.5f);
        breedingAparatus.canBeWashed = false;
    }
}
