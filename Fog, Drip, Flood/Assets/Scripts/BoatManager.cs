using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BoatManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int lowHealthThreshold = 20;
    private int Health;

    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup fadeCanvas;
    [SerializeField] private float fadeDuration = 3f;

    [Header("Low Health Warning")]
    [SerializeField] private CanvasGroup lowHealthCanvas;
    [SerializeField] private float pulseSpeed = 2f;
    [SerializeField] private float pulseMaxAlpha = 0.5f;

    private bool isDying = false;

    private void Start()
    {
        Health = maxHealth;

        if (fadeCanvas != null)
            fadeCanvas.alpha = 0f;

        if (lowHealthCanvas != null)
            lowHealthCanvas.alpha = 0f;

        StartCoroutine(HealthDrain());
    }

    private void Update()
    {
        HandleLowHealthPulse();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacles") && !isDying)
        {
            // SFX: Vene soundi
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectables") && !isDying)
        {
            AddHealth(100);
            Destroy(other.gameObject);

            // SFX: Filter pickup
        }
    }

    private IEnumerator HealthDrain()
    {
        while (!isDying)
        {
            yield return new WaitForSeconds(1f);
            TakeDamage(1);
        }
    }

    private void TakeDamage(int amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    private void AddHealth(int amount)
    {
        Health += amount;
    }

    private void HandleLowHealthPulse()
    {
        if (lowHealthCanvas == null)
            return;

        if (Health > 0 && Health <= lowHealthThreshold && !isDying)
        {
            float pulse = Mathf.Abs(Mathf.Sin(Time.time * pulseSpeed));
            lowHealthCanvas.alpha = Mathf.Lerp(0f, pulseMaxAlpha, pulse);
        }
        else
        {
            lowHealthCanvas.alpha = 0f;
        }
    }

    private void Die()
    {
        if (isDying)
            return;

        isDying = true;

        // SFX: Player death / suffocation / explosion

        StartCoroutine(FadeOutAndRestart());
    }

    private IEnumerator FadeOutAndRestart()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }

        fadeCanvas.alpha = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}