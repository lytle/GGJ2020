using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private List<GameObject> hornyCats;
    [SerializeField]
    private List<GameObject> sexingCats;

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
           // if (timer.timeRemaining > 0)
            //{
               // timerText.text = timer.timeRemaining.ToString("0.00");
                //timer.tick();

            for(int i = 0; i < hornyCats.Count-1; i++)
            {
                GameObject cat = hornyCats[i];
                if (cat.GetComponent<CatMultiply>().makeBaby)
                for (int j = i + 1; j < hornyCats.Count; j++)
                {
                    GameObject otherCat = hornyCats[j];
                    Debug.Log("RWO)");
                    if (otherCat.GetComponent<CatMultiply>().makeBaby)
                    {  
                        Debug.Log("CHe kicng two cats");
                        if (Vector3.Distance(cat.transform.position, otherCat.transform.position) < .6f)
                        {
                            cat.GetComponent<CatMultiply>().makeBaby = false;
                            otherCat.GetComponent<CatMultiply>().makeBaby = false;
                            makeNewCat((cat.transform.position + otherCat.transform.position) / 2, cat, otherCat);
                        }
                    }
                }
            }

            for(int i = hornyCats.Count-1; i >= 0; i--)
            {
                if (!hornyCats[i].GetComponent<CatMultiply>().makeBaby)
                    hornyCats.RemoveAt(i);
            }
           // }
            //else
           // {
            //    timerText.text = "0.00";
             //   state = State.end;
           // }
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
        sexingCats.Remove(momma);
        sexingCats.Remove(pappa);
    }

    public GameObject GetNewCat()
    {
        return catFactory.GenerateRandomCat();
    }

    public void AddHornyCat(GameObject cat)
    {
        hornyCats.Add(cat);
    }

    public void RemoveHornyCat(GameObject cat)
    {
        hornyCats.Remove(cat);
    }
}
