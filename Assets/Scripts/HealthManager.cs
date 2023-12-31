using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public int health = 4;
    public GameObject dinosaur_body1;
    public GameObject dinosaur_body2;
    public GameObject dinosaur_body3;
    private Animator animator1;
    private Animator animator2;
    private Animator animator3;
    public Image heartContainer;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public TextMeshProUGUI gameOverText; 
    private List<Image> hearts = new List<Image>(); 

    void Start()
    {
        RectTransform gameOverTextRect = gameOverText.GetComponent<RectTransform>();
        RectTransform heartContainerRect = heartContainer.GetComponent<RectTransform>();
        gameOverText.enabled = false; // Hide the "Game Over" text at the start of the game
        Vector2 centerPosition = new Vector2(0, 0); // Center position for both UI elements
        heartContainer.rectTransform.anchoredPosition = centerPosition; // Position the hearts at the center
        animator1 = dinosaur_body1.GetComponent<Animator>();
        animator2 = dinosaur_body2.GetComponent<Animator>();
        animator3 = dinosaur_body3.GetComponent<Animator>();

        for (int i = 0; i < health; i++)
        {
            Image heart = new GameObject("Heart").AddComponent<Image>(); // Create a new Image GameObject for each heart
            heart.transform.SetParent(heartContainer.transform, false); // Set the new Image as a child of the heartContainer
            heart.sprite = fullHeart; // Set the sprite of the new Image to the fullHeart sprite
            heart.rectTransform.anchoredPosition = new Vector2(i * 0.2f, 0); // Set a different position for each heart image
            heart.rectTransform.sizeDelta = new Vector2(0.2f, 0.2f); // Set the size of the heart image
            hearts.Add(heart); // Add the new Image to the hearts list
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void Update()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }

        if (health <= 0)
        {
            gameOverText.enabled = true; // Show the "Game Over" text when the player's health reaches zero
            animator1.SetBool("isDead", true); // Animation for dino is then dead
            animator2.SetBool("isDead", true);
            animator3.SetBool("isDead", true);
            FindObjectOfType<ScriptTimer>().StopTimer(); // freeze the timer for the player score
        }
    }
}