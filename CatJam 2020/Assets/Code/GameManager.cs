using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

enum State { menu, game, end };


public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    //AUDIO
    public AudioSource sexAudioSource;
    public AudioClip[] sexAudioClips;

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

    [SerializeField]
    private float matingHitbox;

    State state;
    // Start is called before the first frame update
    void Start()
    {
        if (singleton == null)
            singleton = this;
        state = State.game;
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

            int counter = 0;
            for(int i = 0; i < hornyCats.Count-1; i++)
            {
                GameObject cat = hornyCats[i];
                CatMultiply firstCatGenitals = cat.GetComponent<CatMultiply>();
                if (firstCatGenitals.enabled && firstCatGenitals.makeBaby)
                for (int j = i + 1; j < hornyCats.Count; j++)
                {
                    counter++;
                    GameObject otherCat = hornyCats[j];
                    CatMultiply secondCatGenitals = otherCat.GetComponent<CatMultiply>();
                    if (secondCatGenitals.enabled && secondCatGenitals.makeBaby)
                    {  
                        if (Vector2.Distance(cat.transform.position, otherCat.transform.position) < matingHitbox)
                        {
                            firstCatGenitals.makeBaby = false;
                            secondCatGenitals.makeBaby = false;
                            makeNewCat((cat.transform.position + otherCat.transform.position) / 2, cat, otherCat);
                        }
                    }
                }
            }

           // Debug.Log("Counter:" + counter);

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
        //AUDIO
        sexAudioSource.clip = sexAudioClips[Random.Range(0, sexAudioClips.Length)];
        sexAudioSource.Play();

    }

    IEnumerator CatSex(Vector3 pos, GameObject momma, GameObject pappa)
    {
        GameObject cloud = GameObject.Instantiate(censorCloud, pos, Quaternion.identity);
        yield return new WaitForSeconds(1.35f);
        //instantiate cats
        //throw cats in random directions
        momma.SetActive(true);
        pappa.SetActive(true);
        GameObject newCat = GameObject.Instantiate(catPrefab, pos, Quaternion.identity);

        momma.GetComponent<CatMultiply>().CalmCat();
        pappa.GetComponent<CatMultiply>().CalmCat();
        newCat.GetComponent<CatMultiply>().CalmCat();
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
