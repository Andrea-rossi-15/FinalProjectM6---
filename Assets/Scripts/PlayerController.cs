using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    AudioManager audioManager;

    [Header("Movimento")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;

    [Header("Salto")]
    public float jumpForce = 7f;
    public float groundCheckDistance = 0.1f;
    public LayerMask Ground;
    public bool isGrounded;

    [Header("Salute")]
    public int maxHealth = 100;
    public int currentHealth;
    public int maxPossibilities = 3;
    [SerializeField] int Turretdamage;
    public GameObject HealingObject;

    [Header("Spawnpoint")]
    public Transform spawnPoint;
    public Transform CheckPoint;

    [Header("DevImp")]
    [SerializeField] bool Invicibility;

    private Rigidbody rb;
    public bool CheckPointSet = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        Invicibility = false;
        transform.position = spawnPoint.position;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        GroundCheck();
        EndGame();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0f, moveZ);

        rb.MovePosition(rb.position + transform.TransformDirection(move) * moveSpeed * Time.fixedDeltaTime);
    }

    void Jump()
    {
        Vector3 velocity = rb.velocity;
        velocity.y = 0f;
        rb.velocity = velocity;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void GroundCheck()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        float checkDistance = groundCheckDistance + 0.1f;

        isGrounded = Physics.Raycast(origin, Vector3.down, checkDistance, Ground);
    }

    public void TakeDamage(int Turretdamage)
    {
        currentHealth -= Turretdamage;
        if (currentHealth <= 0)
        {
            maxPossibilities--;
            transform.position = spawnPoint.position;
            currentHealth = maxHealth;
            AudioManager.PlaySound(AudioManager.AudioSources.DEATH);
        }
    }
    public void EndGame()
    {
        if (maxPossibilities <= 0)
        {
            SceneManager.LoadScene("LoosePanel");
            AudioManager.PlaySound(AudioManager.AudioSources.LOOSE);

            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet") && Invicibility == false)
        {
            TakeDamage(Turretdamage);
        }
        if (collision.collider.CompareTag("OutOfBound") && CheckPointSet == false)
        {
            if (Invicibility == false)
            {
                maxPossibilities--;

            }
            transform.position = spawnPoint.position;
            AudioManager.PlaySound(AudioManager.AudioSources.DEATH);

        }
        else if (collision.collider.CompareTag("OutOfBound") && CheckPointSet)
        {
            if (Invicibility == false)
            {
                maxPossibilities--;

            }
            transform.position = CheckPoint.position;
            AudioManager.PlaySound(AudioManager.AudioSources.DEATH);

        }
        if (collision.collider.CompareTag("Healer"))
        {
            AudioManager.PlaySound(AudioManager.AudioSources.HEALING);
            maxPossibilities++;
            Destroy(collision.collider.gameObject);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        CheckPointSet = true;
        if (CheckPointSet)
        {
            CheckPoint.gameObject.SetActive(false);
        }
    }
}
