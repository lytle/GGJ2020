using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum State { menu, game, end };


public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    [SerializeField]
    private CatGenerator catFactory;
    [SerializeField]
    private GameObject catPrefab;
    [SerializeField]
    private GameObject censorCloud;

    [SerializeField]
    private Timer timer;
    [SerializeField]
    private Text timerText;
    State state;
    // Start is called before the first frame update
    void Start()
    {
        if (singleton == null)
            singleton = this;
        state = State.menu;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.menu)
        {
            Debug.Log("In Menu");
            if (Input.GetKeyDown(KeyCode.G))
                state = State.game;
        }
        else if (state == State.game)
        {
            if (timer.timeRemaining > 0)
            {
                timerText.text = timer.timeRemaining.ToString("0.00");
                timer.tick();
            }
            else
            {
                timerText.text = "0.00";
                state = State.end;
            }
        }
        else if (state == State.end)
        {
            Debug.Log("Game End State (SCOREBOARD?)");
        }
    }
    public void makeNewCat(Transform pos, GameObject momma, GameObject pappa)
    {
        momma.SetActive(false);
        pappa.SetActive(false);
        StartCoroutine(CatSex(pos, momma, pappa));
    }

    IEnumerator CatSex(Transform pos, GameObject momma, GameObject pappa)
    {
        GameObject cloud = GameObject.Instantiate(censorCloud, pos.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        GameObject.Destroy(cloud);
        //instantiate cats
        //throw cats in random directions
        momma.SetActive(true);
        pappa.SetActive(true);
        GameObject newCat = GameObject.Instantiate(catPrefab, pos.position, Quaternion.identity);
        GameObject visuals = catFactory.GenerateRandomCat();
        visuals.transform.parent = newCat.transform;
        visuals.transform.localPosition = Vector3.zero;
    }
}
