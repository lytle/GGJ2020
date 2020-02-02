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

    public AudioSource audioSource;
    
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
            //StartCoroutine(LoadGame());
            leaving = true;
        }
        else if (leaving)
        {
            StartCoroutine(FadeOutAudio(audioSource, 1.5f));
            StartCoroutine(FadeEffect());
            leaving = false;
        }

        this.GetComponent<CanvasRenderer>().SetAlpha(0.5f * (Mathf.Sin(5f * Time.time) + 0.75f));
    }

    IEnumerator FadeEffect()
    {
        float alpha = 0f;

        while(alpha < 1f)
        {
            Debug.Log(alpha);
            black.color = new Color(0, 0, 0, alpha);
            alpha += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene("Pember10 Scene", LoadSceneMode.Single);

    }

    IEnumerator FadeOutAudio(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
