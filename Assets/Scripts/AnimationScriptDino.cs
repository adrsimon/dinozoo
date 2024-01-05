using System.Collections;
using UnityEngine;

public class AnimationScriptDino : MonoBehaviour
{
    public GameObject dinosaur_body;
    private Animator animator; // Animator pour contrôler les animations
    private HealthManager healthManager; // Référence au script HealthManager
    private Rigidbody rb; // Rigidbody pour le mouvement
    private bool isHungry = false;
    public float maxHTime = 20f;
    private float HTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = dinosaur_body.GetComponent<Animator>(); // Récupérer le composant Animator
        healthManager = FindObjectOfType<HealthManager>();
        StartCoroutine(HungerCycles());
        healthManager = FindObjectOfType<HealthManager>();
    }

    private void Update()
    {
        if (isHungry)
        {
            HTime += Time.deltaTime;
            if (HTime >= maxHTime)
            {
                animator.SetBool("isHungry", false);
                HTime = 0f;
                healthManager.TakeDamage(1); // Call TakeDamage function from HealthManager
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            animator.SetBool("isHungry", false); // Arrêter l'animation de saut
            Destroy(other.gameObject);
            HTime = 0f;
        }
    }

    // La méthode Update peut être supprimée si elle n'est plus utilisée

    IEnumerator HungerCycles()
    {
        while (true)
        {
            // L'attente aléatoire avant de déclencher l'état de faim
            yield return new WaitForSeconds(Random.Range(10, 30));
            animator.SetBool("isHungry", true); // Déclencher l'animation de saut
            isHungry = true;
        }
    }

    // Les méthodes pour gérer les couleurs peuvent être supprimées
}
