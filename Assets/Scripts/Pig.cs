using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float health;
    public float maxHealth = 50f;
    public float speed;


    public GameObject PointA;
    public GameObject PointB;


    private Rigidbody2D rb;
    private Transform currentPoint;
    public Animator anim;
    public GameObject DeadPose;
    public bool GoldenPig;
    public bool Ready;
    private bool Fleeing = false;
    // Start is called before the first frame update
    void Start()
    {
        anim.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        currentPoint = PointB.transform;
    }
    private void Update()
    {
        if(transform.position == PointB.transform.position && Fleeing)
        {
            Destroy(gameObject);
        }
        else if (Ready)
        {
            if (currentPoint == PointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
                Debug.Log("Moving Right");
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
                Debug.Log("Moving Left");
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointB.transform)
            {
                flip();
                currentPoint = PointA.transform;
            }
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointA.transform)
            {
                flip();
                currentPoint = PointB.transform;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Ready = true;
            Fleeing = true;
        }
    }
    private void flip()
    {
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        speed += 0.5f;
        anim.SetTrigger("Hurt");
        if(health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        if (GoldenPig) { GameManager.instance.GoldenPigsDead++; }
        else { GameManager.instance.PigsDead++; }
        Instantiate(DeadPose, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);

        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
    }
}
