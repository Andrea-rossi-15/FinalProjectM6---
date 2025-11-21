using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinController : MonoBehaviour
{
    AudioManager audioManager;
    void OnCollisionEnter(Collision collision)
    {
        {
            UIController counter = FindObjectOfType<UIController>();
            if (collision.collider.CompareTag("Player") && counter != null)
            {
                AudioManager.PlaySound(AudioManager.AudioSources.COIN);
                counter.AddCoin();
                Destroy(gameObject);
            }
        }
    }
}
