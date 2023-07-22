using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float InitialDamage = 10f;
    public float TotalDamage = 0.3f;

    public float AtackRate = 2f;
    float nextAttackTime = 0f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(InitialDamage);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= nextAttackTime)
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(TotalDamage);
                nextAttackTime = Time.time + 1f / AtackRate;
            }
        }
    }
}
