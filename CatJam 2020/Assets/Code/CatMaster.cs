using System.Collections;
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
        float destinationX = 22.0f * (facingRight ? 1.0f : -1.0f);
        float gravity = 0.02f;

        while(this.transform.position.y > playerY) {
            this.transform.Translate(destinationX * Time.deltaTime, -12.0f * gravity * Time.deltaTime, 0.0f);
            gravity += 5.0f * Time.deltaTime;
            Debug.Log("falling");
            yield return new WaitForEndOfFrame();
        }

        GetComponent<Collider2D>().enabled = true;
        EnableBreed();
        EnableMove();
        yield return null;
    }
}
