using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage = 1f;
    private Rigidbody2D rb;
    private Transform playerTransform;

    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        TryGetComponent(out rb);
    }

    private void FixedUpdate() 
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        var difference = playerTransform.position - transform.position;

        rb.MovePosition(speed * Time.deltaTime * difference.normalized + (Vector3)rb.position);
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthSystem>().TakeDamage(damage);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
