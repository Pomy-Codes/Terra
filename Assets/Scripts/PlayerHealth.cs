using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;

    public Image FronthealthBar;
    public Image BackhealthBar;
    public Animator anim;

    public UIMenu uiMenu;
    public GameObject DeadPose;
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -16f)
        {
            Die();
        }
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
    }
    public void UpdateHealthUI()
    {
        float fillB = BackhealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if(fillB > hFraction)
        {
            FronthealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            BackhealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        if (health <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
        else if(health >= 0)
        {
            anim.SetBool("Hurting", true);
            anim.SetTrigger("Hurt");
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Hit, transform.position);
            StartCoroutine(Hurt());
        }
    }
    IEnumerator Hurt()
    {
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Hurting", false);
    }
    public void Die()
    {
        Debug.Log("You died");
        StartCoroutine(Dead());
    }
    IEnumerator Dead()
    {
        anim.SetBool("Dead", true);
        yield return new WaitForSeconds(0.4f);
        AudioManager.instance.GamePlayMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Instantiate(DeadPose, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.05f);
        uiMenu.OnDead();

    }

}
