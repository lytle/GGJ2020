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
        if(Input.GetButtonDown("Start")) SceneManager.LoadScene("Chance Scene", LoadSceneMode.Additive);

        titleScreen.transform.position = new Vector3(0f, 3f * Mathf.Sin(0.25f * Time.time), 1.0f);
    }
}
