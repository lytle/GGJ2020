using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
