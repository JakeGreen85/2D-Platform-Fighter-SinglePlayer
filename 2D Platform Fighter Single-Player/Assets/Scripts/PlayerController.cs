using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Inspector variables
    [SerializeField] private float moveSpeed = 5.0f; // horizontal movement speed
    [SerializeField] private float jumpForce = 10.0f; // force applied when jumping
    [SerializeField] private GameObject projectile;

    // Private variables
    private Rigidbody2D rb; // reference to the Rigidbody2D component
    private float atkRange = 2f;
    [SerializeField] private int health = 100;
    [SerializeField] private int atk = 10;
    [SerializeField] private int ultCharge = 0;

    private float atkSpeed = 1;
    private float lastHit;
    private float lastInput;
    private bool jumpAllowed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastHit = Time.time;
    }

    void Update()
    {
        Move();
        GetUserInput();
        GetComponentInChildren<TextMeshPro>().text = "Health: " + health.ToString() + "\n" + "Attack: " + atk.ToString() + "\n" + "Ultimate Charge: " + ultCharge.ToString() + "%";
        if(this.health <= 0){
            Die();
        }
    }

    private void GetUserInput()
    {
        if(Input.anyKeyDown){
            if(Input.GetKeyDown(KeyCode.J) && Time.time > lastHit + (1/atkSpeed)){
                GetComponent<Animator>().SetTrigger("attack");
                Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, atkRange);
                foreach(Collider2D col in hit){
                    Debug.Log(col.name);
                    EnemyController ec = col.GetComponent<EnemyController>();
                    if(ec != null){
                        Debug.Log("ATTACKED");
                        ec.TakeDamage(atk);
                    }
                }
                lastHit = Time.time;
            }
            if(Input.GetKeyDown(KeyCode.K) && Time.time > lastHit + (1/atkSpeed)){
                GetComponent<Animator>().SetTrigger("attack");
                Instantiate(projectile, transform.position, transform.rotation);
                Debug.Log("FIRE PROJECTILE");
            }
            if(Input.GetKeyDown(KeyCode.L) && ultCharge >= 100){
                GetComponent<Animator>().SetTrigger("attack");
                // ULTIMATE
                ultCharge = 0;
                Debug.Log("ULTIMATE ABILITY");
            }
        }
    }

    void Die(){
        GetComponent<Animator>().SetTrigger("death");
        Invoke("Respawn", 1);
    }

    public void TakeDamage(int damage)
    {
        if(damage < 0){
            GetComponent<Animator>().SetTrigger("hurt");
        }else{
            // Play health up animation
        }
        health -= damage;
    }

    public void IncreaseDamage(int increase){
        atk += increase;
    }

    public void IncreaseUltCharge(int increase){
        ultCharge += increase;
        if(ultCharge > 100){
            ultCharge = 100;
        }
    }

    void Move(){

        // Get input for horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput <= 0 && lastInput < 0){
            GetComponent<Animator>().SetFloat("movement", -horizontalInput);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            GetComponentInChildren<RectTransform>().rotation = new Quaternion(0,0,0,0);
        }else{
            GetComponent<Animator>().SetFloat("movement", horizontalInput);
            transform.rotation = new Quaternion(0, -1, 0, 0);
            GetComponentInChildren<RectTransform>().rotation = new Quaternion(0,-1,0,0);
        } 

        // Move the player horizontally
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        if(IsGrounded()) jumpAllowed = true;
        // Check for jump input
        if(Input.GetButtonDown("Jump"))
        {
            if(IsGrounded()){
                // Apply jump force
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                GetComponent<Animator>().SetTrigger("jump");
                jumpAllowed = true;
            }
            else if(jumpAllowed){
                // Apply jump force
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                GetComponent<Animator>().SetTrigger("jump");
                jumpAllowed = false;
            }
        }
        if(horizontalInput != 0){
            lastInput = horizontalInput;
        }
    }

    // Method to check if player is on the ground
    private bool IsGrounded()
    {
        // Get the position of the ground check point in world space
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        // Check if there is a ground tile at the ground check point
        foreach(Collider2D go in hit){
            if(go.CompareTag("ground")){
                return true;
            }
        }
        return false;
    }

    public void Respawn(){
        GetComponent<Animator>().SetTrigger("alive");
        transform.position = new Vector3(0, 10, 0);
        this.health = 100;
    }
}
