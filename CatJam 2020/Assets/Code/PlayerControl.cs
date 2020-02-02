using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance = null;
    public BoxCollider2D boxCollider;
    public Transform spriteObject;
    public Animator anim;


    public float maxVel = 10f;
    public float velocity = 0.0f;
    public float accel = 0.2f;
    public float decel = 0.75f;

    //Audio for throwing
    public AudioSource throwAudioSource;
    public AudioClip[] throwAudioClips;

    public AudioSource pickUpAudioSource;
    public AudioClip[] pickUpAudioClips;

    public bool facingRight = false;
    private Vector2 ourMoveDir;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private Vector3 centerDispalcement = new Vector3(0.0f, 1.47f, 0.0f); // this should be CameraCenter's y
    public Transform reticle;

    private void Update()
    {
        // Use GetAxisRaw to ensure our input is either 0, 1 or -1.
        ourMoveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // acceleration
        if(ourMoveDir != Vector2.zero)
        {
            velocity += accel;
            if (velocity > maxVel) velocity = maxVel;

            // walking anim
            anim.SetBool("isWalking", true);
            // reticle math
            reticle.position = Vector3.Lerp(reticle.position, this.transform.position + new Vector3(ourMoveDir.x, ourMoveDir.y, 0.0f) * 1.7f, 0.25f);
        }
        else
        {
            velocity -= decel;
            if (velocity < 0f) velocity = 0f;
            anim.SetBool("isWalking", false);
        }

        this.transform.Translate(ourMoveDir * velocity * Time.deltaTime);

        
        // restrict y movement
        if(this.transform.position.y > 13.0f)
        {
            this.transform.position = new Vector3(this.transform.position.x, 13.0f);
        }
        else if (this.transform.position.y < -13.0f)
        {
            this.transform.position = new Vector3(this.transform.position.x, -13.0f);
        }
        // restrict x
        if (this.transform.position.x > 25.0f)
        {
            this.transform.position = new Vector3(25.0f, this.transform.position.y);
        }
        else if (this.transform.position.x < -25.0f)
        {
            this.transform.position = new Vector3(-25.0f, this.transform.position.y);
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

        // Check for throw
        if (Input.GetButton("Drop")) ThrowCats();
    }

    private void CheckForCats()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size*1.1f, 0);
        // if its a cat
        foreach (var hit in hits)
        {
            if (hit.transform.root.gameObject.GetComponent<CatAI>() != null)
            {
                // if we are facing it
                if (facingRight && hit.gameObject.transform.position.x > this.transform.position.x ||
                    !facingRight && hit.gameObject.transform.position.x < this.transform.position.x)
                {
                    // Debug.Log("cat in range!");
                    if (isThrowing == null && pickingUp == null && Input.GetButtonDown("PickUp"))
                    {
                        StackCat(hit.gameObject);
                    }
                }

            }
        }
    }
    [Header("Cat Stack Numbers")]
    public float catDisplacementFromPlayer = 2.8f;
    public float catDispalcementInStack = 0.02f;

    private Coroutine isThrowing;

    private void UpdateCatStack()
    {
        // update player position
        for (int i = 0; i < catStack.Count; i++)
        {
            var catToMove = catStack.ToArray()[i];
            var playerDisplacement = catDisplacementFromPlayer + (catStack.Count - i + 1) * 1f;
            var swayingXpos = Mathf.Lerp(catToMove.transform.position.x, this.transform.position.x, 0.25f -(catDispalcementInStack * (catStack.Count - i + 1)));
            catToMove.transform.position = new Vector3(swayingXpos, this.transform.position.y + playerDisplacement, 0.0f); //Vector3.Lerp(catToMove.transform.position, playerDisplacement + this.transform.position, 0.2f - (0.03f * (i + 1)));
        }
        anim.SetBool("HandsUp", catStack.Count > 0);
    }

    [SerializeField]
    public Stack<GameObject> catStack = new Stack<GameObject>();
    int maxCatCount = 2;

    private void ThrowCats()
    {
        if (catStack.Count > 0)
        {
            anim.SetBool("HandsUp", false);
            isThrowing = StartCoroutine("ThrowAllCats");
            //Audio for throwing
            throwAudioSource.clip = throwAudioClips[Random.Range(0, throwAudioClips.Length)];
            throwAudioSource.Play();
        }
    }
    IEnumerator ThrowAllCats()
    {
        while(catStack.Count > 0)
        {
            var catToThrow = catStack.Pop();
            catToThrow.GetComponent<CatMaster>().Throw(facingRight, reticle.position.y);
            anim.SetTrigger("Drop");
            yield return new WaitForSeconds(0.3f);
        }
        anim.SetBool("HandsUp", false);
        isThrowing = null;
    }

    private Coroutine pickingUp;

    private void StackCat(GameObject catToStack)
    {
        if (catStack.Count < maxCatCount)
        {
            pickUpAudioSource.clip = pickUpAudioClips[Random.Range(0, pickUpAudioClips.Length)];
            pickUpAudioSource.Play(); 
            anim.SetTrigger("Pickup");
            anim.SetBool("HandsUp", true);
            pickingUp = StartCoroutine(DelayedCatStack(catToStack, 0.15f));
            
        }
    }

    IEnumerator DelayedCatStack(GameObject catToStack, float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("pickupping");
        catToStack.GetComponent<CatMaster>().GotPickedUp();
        /*foreach(SpriteRenderer sr in catToStack.transform.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.sortingOrder += 5;
        }*/
        catStack.Push(catToStack.transform.gameObject);
        pickingUp = null;
    }



}