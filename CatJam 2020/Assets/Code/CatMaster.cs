﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMaster : MonoBehaviour
{
    CatMultiply breedingAparatus;
    CatAI catMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // put visual updates here
    }

    void GotPickedUp()
    {
        DisableBreed();
        DisableMove();
    }

    void GetThrown()
    {
        // complete this
    }

    void DisableBreed()
    {
        breedingAparatus.enabled = false;
    }

    void DisableMove()
    {
        catMove.enabled = false;
    }
}
