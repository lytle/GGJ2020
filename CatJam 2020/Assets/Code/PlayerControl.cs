using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Transform spriteObject;
    public Animator anim;


    public float maxVel = 10f;
    public float velocity = 0.0f;
    public float accel = 0.2f;
    public float decel = 0.75f;

    private bool facingRight = false;
    private Vector2 ourMoveDir;

    private void Awake()
    {
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // Use GetAxisRaw to ensure our input is either 0, 1 or -1.
        ourMoveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // acceleration
        if(ourMoveDir != Vector2.zero)
        {
            velocity += accel;
            if (velocity > maxVel) velocity = maxVel;
            anim.SetBool("isWalking", true);
        } else
        {
            velocity -= decel;
            if (velocity < 0f) velocity = 0f;
            anim.SetBool("isWalking", false);
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
            spriteObject.localScale = new Vector3(spriteObject.localScale.x * -1.0f, spriteObject.localScale.y, spriteObject.localScale.z);
        }

        // Check for Cats.
        CheckForCats();

        // Update Cat Stack
        UpdateCatStack();
    }

    private void CheckForCats()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        foreach (Collider2D hit in hits)
        {
            // if its a cat
            if (hit.gameObject.GetComponent<CatAI>() != null)
            {
                // if we are facing it
                if (facingRight && hit.gameObject.transform.position.x > this.transform.position.x ||
                    !facingRight && hit.gameObject.transform.position.x < this.transform.position.x)
                {
                    Debug.Log("cat in range!");
                    if (Input.GetButtonDown("PickUp")) StackCat(hit.gameObject);
                }

            }
        }
    }
    [Header("Cat Stack Numbers")]
    public float catDisplacementFromPlayer = 2.8f;
    public float catDispalcementInStack = 0.02f;

    private void UpdateCatStack()
    {
        // update player position
        for (int i = 0; i < catStack.Count; i++)
        {
            var catToMove = catStack.ToArray()[i];
            var playerDisplacement = catDisplacementFromPlayer + (catStack.Count - i + 1) * 0.5f;
            var swayingXpos = Mathf.Lerp(catToMove.transform.position.x, this.transform.position.x, 0.2f -(catDispalcementInStack * (catStack.Count - i + 1)));
            catToMove.transform.position = new Vector3(swayingXpos, this.transform.position.y + playerDisplacement, 0.0f); //Vector3.Lerp(catToMove.transform.position, playerDisplacement + this.transform.position, 0.2f - (0.03f * (i + 1)));
        }
    }

    [SerializeField]
    public Stack<GameObject> catStack = new Stack<GameObject>();
    int maxCatCount = 3;

    private void StackCat(GameObject catToStack)
    {
        Debug.Log("Attemping pickup");
        if (catStack.Count < maxCatCount)
        {
            anim.SetTrigger("Pickup");
            anim.SetBool("HandsUp", true);
            StartCoroutine(DelayedCatStack(catToStack, 0.3f));
            
        }


    }

    IEnumerator DelayedCatStack(GameObject catToStack, float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("pickupping");
        catToStack.GetComponent<CatAI>().enabled = false;
        catToStack.GetComponent<BoxCollider2D>().enabled = false;
        catToStack.GetComponent<SpriteRenderer>().sortingOrder = 5;
        catStack.Push(catToStack);
    }



}