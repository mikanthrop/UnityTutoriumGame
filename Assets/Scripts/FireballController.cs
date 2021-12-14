using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float lifeTime = 5f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        Vector2 velocity = transform.right * speed * Time.fixedDeltaTime;
        rb.velocity = velocity;
    }

}
