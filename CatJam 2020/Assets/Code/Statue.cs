using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public int health = 10;
    public float intangibilityTime = 1f, flashTime = 0.05f;
    private Coroutine intangible;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (intangible != null && collision.gameObject.GetComponent<CatMaster>()) {
            health -= 1;
            intangible = StartCoroutine(Intangibility());
        }
    }

    IEnumerator Intangibility()
    {
        float timer = intangibilityTime;
        while (timer > 0)
        {
            //ourSprite.enabled = false;
            yield return new WaitForSeconds(flashTime);
        }
        //ourSprite.enabled = true
        intangible = null;
    }
}
