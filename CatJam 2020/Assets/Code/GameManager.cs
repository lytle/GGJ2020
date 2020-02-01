using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

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
    private UnityEngine.UI.Text timerText;
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
        }
    }
    public void makeNewCat(Vector3 pos, GameObject momma, GameObject pappa)
    {
        momma.SetActive(false);
        pappa.SetActive(false);
        StartCoroutine(CatSex(pos, momma, pappa));
    }

    IEnumerator CatSex(Vector3 pos, GameObject momma, GameObject pappa)
    {
        GameObject cloud = GameObject.Instantiate(censorCloud, pos, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        GameObject.Destroy(cloud);
        //instantiate cats
        //throw cats in random directions
        momma.SetActive(true);
        pappa.SetActive(true);
        GameObject newCat = GameObject.Instantiate(catPrefab, pos, Quaternion.identity);
    }

    public GameObject GetNewCat()
    {
        return catFactory.GenerateRandomCat();
    }
}
