using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    public BoxCollider2D boxCollider;
    public SpriteRenderer ourSprite;


    public float maxVel = 10f;
    public float velocity = 0.0f;
    public float accel = 0.2f;
    public float decel = 0.75f;

    private bool facingRight = true;
    private Vector2 ourMoveDir;

    private void Awake()
    {
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        ourSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        // Use GetAxisRaw to ensure our input is either 0, 1 or -1.
        ourMoveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Debug.Log(ourMoveDir);

        // acceleration
        if(ourMoveDir != Vector2.zero)
        {
            velocity += accel;
            if (velocity > maxVel) velocity = maxVel;
        } else
        {
            velocity -= decel;
            if (velocity < 0f) velocity = 0f; 
        }

        this.transform.Translate(ourMoveDir * velocity * Time.deltaTime);

        // restrict y movement
        if(this.transform.position.y > 5.0f)
        {
            this.transform.position = new Vector3(this.transform.position.x, 5.0f);
        }
        else if (this.transform.position.y < -5.0f)
        {
            this.transform.position = new Vector3(this.transform.position.x, -5.0f);
        }
        // restrict x
        if (this.transform.position.x > 9.0f)
        {
            this.transform.position = new Vector3(9.0f, this.transform.position.y);
        }
        else if (this.transform.position.x < -9.0f)
        {
            this.transform.position = new Vector3(-9.0f, this.transform.position.y);
        }

        // Update rotation
        if((ourMoveDir.x > 0.0f && !facingRight) || (ourMoveDir.x < 0.0f && facingRight))
        {
            facingRight = !facingRight;
            ourSprite.flipX = !ourSprite.flipX;
        }

        // Check for Cats.
        //CheckForCats();
    }

    private void CheckForCats()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        foreach (Collider2D hit in hits)
        {
            // if its a cat
            if (hit.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                // if we are facing it
                if (facingRight && hit.gameObject.transform.position.x > this.transform.position.x ||
                    !facingRight && hit.gameObject.transform.position.x < this.transform.position.x)
                {
                    Debug.Log("cat in range!");
                    if (Input.GetButton("PickUp")) StackCat(hit.gameObject);
                }

            }
        }
    }

    private void UpdateCatStack()
    {
        for (int i = 0; i < catStack.Count; i++)
        {
            var catToMove = catStack.ToArray()[i];
            var playerDisplacement = new Vector2(0.0f, (i + 1) * 0.25f);
            //catToMove.transform.position = Mathf.Lerp(catToMove.transform.position, this.transform.position, 0.5f);
        }
    }

    Stack<GameObject> catStack = new Stack<GameObject>();
    int maxCatCount = 3;

    private void StackCat(GameObject catToStack)
    {
        if (catStack.Count < maxCatCount)
        {
            // disable cat AI
            catStack.Push(catToStack);
        }


    }
}