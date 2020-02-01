using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    private BoxCollider2D boxCollider;
    private SpriteRenderer ourSprite;


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

        // Retrieve all colliders we have intersected after velocity has been applied.
        //Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        /*foreach (Collider2D hit in hits)
        {
            // Ignore our own collider.
            if (hit == boxCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            // Ensure that we are still overlapping this collider.
            // The overlap may no longer exist due to another intersected collider
            // pushing us out of this one.
            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

            }
        } */
    }
}