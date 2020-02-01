using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer BodySprite, FaceSprite, TailSprite;

    public void SetParts(Sprite body, Sprite face, Sprite tail, Color color)
    {
        BodySprite.sprite = body;
        FaceSprite.sprite = face;
        TailSprite.sprite = tail;
        BodySprite.color = color;
        FaceSprite.color = color;
        TailSprite.color = color;
    }
}
