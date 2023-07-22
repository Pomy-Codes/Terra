using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerControl : MonoBehaviour
{
    [Header("Player Stats")]
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    // Private Variables
    [Header("Player Variables")]
    [SerializeField]Transform GroundCheck;
    [SerializeField] Transform AttackPoint;
    [SerializeField]float Groundradius = 2f;
    [SerializeField] float Attackradius = 2f;

    [Header("Player Attack")]
    public float AttackDamage = 25f;
    public float AtackRate = 2f;
    float nextAttackTime = 0f;

    private Vector2 MovementX;
    private Rigidbody2D rb;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AttackCoolDown();
        Jump();
        Movement();
    }
    #region Controls
    void Movement()
    {
        // Move the player left and right   
        MovementX.x = Input.GetAxis("Horizontal");

        rb.position += new Vector2(MovementX.x, 0) * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("IsRunning", true);
            speed = 7.5f;
        }
        else {
            Debug.Log("Walking");
            anim.SetBool("IsRunning", false);
            speed = 6f;
        }

        anim.SetFloat("Movement", Mathf.Abs(MovementX.x));

        //Flip the player
        if(MovementX.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(MovementX.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }   
    }
    private void FixedUpdate()
    {
        anim.SetBool("IsGrounded", IsGrounded());
    }
    void Jump() {
       Debug.Log(IsGrounded());
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Jump, transform.position);
            anim.SetTrigger("Jump");
        }
    }
    bool IsGrounded() { 
        return Physics2D.OverlapCircle(GroundCheck.position, Groundradius, 1 << LayerMask.NameToLayer("Ground"));
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, Groundradius);
        Gizmos.DrawWireSphere(AttackPoint.position, Attackradius);
    }
    private void Attack()
    {
         anim.SetTrigger("Attack");
        anim.SetBool("IsAttacking", true);
        int num = Random.Range(0, 10);
         bool result = num >= 5 ? true : false;
         anim.SetBool("AttackType", result);

         AudioManager.instance.PlayOneShot(FMODEvents.instance.Attack, transform.position);
         Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, Attackradius, 1 << LayerMask.NameToLayer("Pig"));
         foreach (Collider2D enemy in hitEnemies)
         {
             Debug.Log("We hit " + enemy.name);
             enemy.GetComponent<Pig>().TakeDamage(AttackDamage);
         }
         StartCoroutine(Cooldown());
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.7f);
        anim.SetBool("IsAttacking", false);
    }
    private void AttackCoolDown()
    {
        if (Time.time >= nextAttackTime)
        {
            if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.R)) && IsGrounded())
            {
                Attack();
                nextAttackTime = Time.time + 1f / AtackRate;
            }
        }
    }
}
