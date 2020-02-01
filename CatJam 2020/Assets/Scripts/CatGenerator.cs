using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatGenerator : MonoBehaviour
{
    [SerializeField] GameObject CatPrefab;

    [SerializeField]
    List<Sprite> Bodies, Faces, Tails;

    [SerializeField]
    List<Color> Colors;

    public GameObject GenerateRandomCat()
    {
        GameObject newCat = Instantiate(CatPrefab);
        newCat.GetComponent<Cat>().SetParts(GetRandomSprite(Bodies), GetRandomSprite(Faces), GetRandomSprite(Tails), Colors[Random.Range(0, Colors.Count - 1)]);
        return newCat;
    }

    Sprite GetRandomSprite(List<Sprite> sprites)
    {
        return sprites[Random.Range(0, sprites.Count - 1)];
    }


    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateRandomCat();
        }
    }
}
