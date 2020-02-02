using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    public static Fountain singleton;
    public AudioSource fountainSound;
    public ParticleSystem bubble;

    public void Start()
    {
        if (singleton == null)
            singleton = this;
    }

    public void CatInMe()
    {
        //fountainSound.PlayOneShot();
        bubble.Play();
    }


}
