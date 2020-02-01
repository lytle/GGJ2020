using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeRemaining;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tick()
    {
        if (timeRemaining > 0)
            timeRemaining -= Time.deltaTime;
        Debug.Log(timeRemaining);
    }
}
