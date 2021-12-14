using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private GameObject explosionPrefab = null;
    //fireball is destroyed after its lifetime
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

    private void Die(float _lifetime = 0f)
    {
        explosionPrefab = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, _lifetime);
    }
    //if fireball is colliding with smth, which isn't the player character, make it explode
    private void OnTriggerEnter2D(Collider2D _collision)
    {
        PlayerControl player = _collision.gameObject.GetComponent<PlayerControl>();
        if (player != null) return;

        Die();
    }
    
}
