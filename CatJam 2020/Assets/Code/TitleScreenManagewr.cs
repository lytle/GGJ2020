using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManagewr : MonoBehaviour
{
    private Transform titleScreen;
    [SerializeField]
    private Image black;

    private bool leaving;
    
    // Start is called before the first frame update
    void Start()
    {
        titleScreen = this.transform;
        leaving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start") && !leaving)
        {
            StartCoroutine(LoadGame());
            leaving = true;
        }
        else if (leaving)
        {
            black.CrossFadeAlpha(1, 2.0f, false);
            Debug.Log("fading");
        }

        this.GetComponent<CanvasRenderer>().SetAlpha(0.5f * (Mathf.Sin(5f * Time.time) + 0.75f));
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Ramsey Scene", LoadSceneMode.Single); 
        Debug.Log("meow meow");
    }
}
