using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{

    public TextMeshProUGUI score;

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
