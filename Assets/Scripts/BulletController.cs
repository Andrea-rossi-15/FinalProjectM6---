using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    AudioManager audioManager;
    public float speed = 10f;
    private Rigidbody rb;
    private float lifeTime = 5f;
    private float timer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        timer = lifeTime;

        if (rb != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                rb.velocity = direction * speed;
                transform.forward = direction;
            }
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
        rb.useGravity = true;
        AudioManager.PlaySound(AudioManager.AudioSources.HIT);

    }
}

