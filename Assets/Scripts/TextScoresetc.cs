using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScoresetc : MonoBehaviour
{
    private PlayerController playerController;
    private TextMeshProUGUI scoreText; // Reference to the TextMeshPro component

    // Start is called before the first frame update
    void Start()
    {
        // Find the PlayerController instance
        playerController = FindObjectOfType<PlayerController>();
        
        // Get the TextMeshProUGUI component attached to this GameObject
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the text with the player's velocity
        if (playerController != null && scoreText != null)
        {
            int speed= (int)playerController.rg2d.velocity.x;
            scoreText.text = "Speed = " + speed.ToString();
        }
    }
}
