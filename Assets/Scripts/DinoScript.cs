using System.Collections;
using UnityEngine;

public class DinoScript : MonoBehaviour
{
    public GameObject dinosaur_body;
    public GameObject dinosaur_extremity;
    private HealthManager healthManager; // Reference to the HealthManager script
    private Color originalBodyColor;
    private Color originalExtremityColor;
    private bool isRed = false;
    private float redTime = 0f;
    private float maxRedTime = 10f; // Maximum time the dino can stay red

    void Start()
    {
        originalBodyColor = dinosaur_body.GetComponent<Renderer>().material.color;
        originalExtremityColor = dinosaur_extremity.GetComponent<Renderer>().material.color;
        StartCoroutine(HungerCycle());
        healthManager = FindObjectOfType<HealthManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            ResetColors();
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (isRed)
        {
            redTime += Time.deltaTime;

            if (redTime >= maxRedTime)
            {
                // Reset colors after staying red for more than maxRedTime
                ResetColors();
                healthManager.TakeDamage(1); // Call TakeDamage function from HealthManager
            }
        }
    }

    IEnumerator HungerCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10, 30));
            SetRedColors();
        }
    }

    void SetRedColors()
    {
        dinosaur_body.GetComponent<Renderer>().material.color = Color.red;
        dinosaur_extremity.GetComponent<Renderer>().material.color = Color.red;
        isRed = true;
        redTime = 0f;
    }

    void ResetColors()
    {
        dinosaur_body.GetComponent<Renderer>().material.color = originalBodyColor;
        dinosaur_extremity.GetComponent<Renderer>().material.color = originalExtremityColor;
        isRed = false;
        redTime = 0f;
    }
}
