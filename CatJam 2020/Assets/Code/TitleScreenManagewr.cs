using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManagewr : MonoBehaviour
{
    private Transform titleScreen;

    // Start is called before the first frame update
    void Start()
    {
        titleScreen = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start")) { SceneManager.LoadScene("Ramsey Scene", LoadSceneMode.Additive); Debug.Log("meow meow"); }

        this.GetComponent<CanvasRenderer>().SetAlpha(0.5f * (Mathf.Sin(5f * Time.time) + 0.75f));
    }
}
