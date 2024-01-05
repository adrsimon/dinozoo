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
    private Rigidbody rb; // Rigidbody for movement and jumping
    public float jumpForce = 300f;
    private bool onGround = true;

    void Start()
    {
        originalBodyColor = dinosaur_body.GetComponent<Renderer>().material.color;
        originalExtremityColor = dinosaur_extremity.GetComponent<Renderer>().material.color;
        rb = GetComponent<Rigidbody>();
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
        else if (other.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void Update()
    {
        if (isRed)
        {
            redTime += Time.deltaTime;
            if (onGround)
            {
                AngryMovement();
            }

            if (redTime >= maxRedTime)
            {
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

    // Used before for changing color of dinosaurs.
    void SetRedColors()
    {
        dinosaur_body.GetComponent<Renderer>().material.color = Color.red;
        dinosaur_extremity.GetComponent<Renderer>().material.color = Color.red;
        isRed = true;
        redTime = 0f;
        AngryMovement();
    }

    void ResetColors()
    {
        dinosaur_body.GetComponent<Renderer>().material.color = originalBodyColor;
        dinosaur_extremity.GetComponent<Renderer>().material.color = originalExtremityColor;
        isRed = false;
        redTime = 0f;
    }

    void AngryMovement()
    {
        // Start the JumpInRow coroutine
        StartCoroutine(JumpInRow());
    }

    // Same as SetRedColor, used for another type of dino we no longer use in this scene
    IEnumerator JumpInRow()
    {
        // While the dinosaur is red, make it jump and then wait for a short period before the next jump
        while (isRed)
        {
            // Make the dinosaur jump vertically
            rb.AddForce(Vector3.up * jumpForce);
            onGround = false;

            // Wait for a short period before the next jump
            yield return new WaitForSeconds(1f);
        }
    }
}
