using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float dashSpeed = 2.4f;
    [SerializeField] private float dashTimeOut = 0.5f;
    private bool canDash = true;
    [SerializeField] private FireballController fireball;
    GameObject activeFireball;
    private bool canToss = true;
    private Vector2 moveDirection = Vector2.zero;
    [SerializeField] private float interactionRadius = 2f;


    //gets referenced every frame
    public void Update()
    {
        Move();
    }
    //sets new direction the player wants to go, always
    public void ChangeMoveDirection(Vector2 _dir)
    {
        moveDirection = _dir;
    }

    //Movement Animation
    public void Move()
    {
        //player shouldn't be able to walk while dashing
        if (canDash == true)
        {
            rb.velocity = moveDirection * speed;

            if (moveDirection.magnitude >= 0.3f)
            {
                anim.SetFloat("MovementHorizontal", moveDirection.x);
                anim.SetFloat("MovementVertical", moveDirection.y);
            }
        }
    }
    //Dash Animation
    public void Dash()
    {
        //player should only be able to dash if they aren't dashing already
        if (canDash == true)
        {
            rb.velocity *= dashSpeed;
            anim.SetFloat("MovementHorizontal", rb.velocity.x);
            anim.SetFloat("MovementVertical", rb.velocity.y);
            StartCoroutine(DashTimer());
        }
    }

    //sets velocity back to original speed
    IEnumerator DashTimer()
    {
        canDash = false;
        yield return new WaitForSecondsRealtime(dashTimeOut);
        canDash = true;
    }

    //makes fireballs in the direction player is looking
    public void Fireball()
    {
        if (activeFireball != null) return;

        activeFireball = Instantiate(fireball.gameObject, transform.position, Quaternion.identity);
        Vector3 dir = Vector3.zero;
        dir.x = anim.GetFloat("MovementHorizontal");
        dir.y = anim.GetFloat("MovementVertical");
        activeFireball.transform.right = dir;
    }

    public void Interact()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, interactionRadius, moveDirection);
        foreach (RaycastHit2D hit in hits)
        {
            Interactable isInteractable = hit.transform.GetComponent<Interactable>();
            if (isInteractable != null) {
                isInteractable.Interact(this);
            }
        }
    }
}
