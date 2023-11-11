using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public int health = 3;
    public Image heartContainer; // The Image component where you want to display the hearts
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public TextMeshProUGUI gameOverText; // The Text component where you want to display "Game Over"
    private List<Image> hearts = new List<Image>(); // A list to hold the heart images

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.enabled = false; // Hide the "Game Over" text at the start of the game
        for (int i = 0; i < health; i++)
        {
            Image heart = new GameObject("Heart").AddComponent<Image>(); // Create a new Image GameObject for each heart
            heart.transform.SetParent(heartContainer.transform, false); // Set the new Image as a child of the heartContainer
            heart.sprite = fullHeart; // Set the sprite of the new Image to the fullHeart sprite
            heart.rectTransform.anchoredPosition = new Vector2(i * 30, 0); // Set a different position for each heart image
            heart.rectTransform.sizeDelta = new Vector2(30, 30); // Set the size of the heart image
            hearts.Add(heart); // Add the new Image to the hearts list
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHearts();
    }

    void UpdateHearts()
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
        }
    }
}