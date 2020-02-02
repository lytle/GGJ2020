using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{

    public TextMesh score;

    void Start()
    {
        score.text = "Score: " + GameManager.score;
    }
    

    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            LoadFirstScene();
        }
    }

    public void LoadFirstScene()
    {
        SceneManager.LoadScene("TestTitle");
    }
}
