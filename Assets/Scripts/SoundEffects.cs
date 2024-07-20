using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    private Rigidbody2D siblingRigidbody2D; // Rigidbody2D of the sibling GameObject
    [SerializeField] private AudioSource sound1; // Sound for speed less than 0 and between 0 and 10
    [SerializeField] private AudioSource sound2; // Sound for speed between 10 and 45
    [SerializeField] private AudioSource sound3; // Sound for speed greater than 45

    PlayerController playerController;
    public bool enable = false; // To check if the object is touching the ground

    // Start is called before the first frame update
    void Start()
    {
        siblingRigidbody2D = transform.parent.GetComponentInChildren<Rigidbody2D>();
        playerController=FindObjectOfType<PlayerController>();
        // Ensure AudioSources are assigned in the Unity Editor
        if (sound1 == null || sound2 == null || sound3 == null)
        {
            Debug.LogError("Please assign the AudioSource components in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xVelocity = siblingRigidbody2D.velocity.x;

        if (enable)
        {
            if (xVelocity < -15 || (xVelocity > 15 && xVelocity < 25))
            {
                PlaySound(sound1);
                StopSoundsExcept(sound1);
            }
            else if (xVelocity >= 25 && xVelocity < 45)
            {
                PlaySound(sound2);
                StopSoundsExcept(sound2);
            }
            else if (xVelocity >= 45)
            {
                PlaySound(sound3);
                StopSoundsExcept(sound3);
            }
            else
            {
                StopAllSounds();
            }
        }
        else
        {
            StopAllSounds();
        }
    }

    void PlaySound(AudioSource audioSource)
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void StopSoundsExcept(AudioSource exceptSource)
    {
        if (sound1 != exceptSource) sound1.Stop();
        if (sound2 != exceptSource) sound2.Stop();
        if (sound3 != exceptSource) sound3.Stop();
    }

    void StopAllSounds()
    {
        sound1.Stop();
        sound2.Stop();
        sound3.Stop();
    }
    private void OnTriggerEnter2D(){
        enable=true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        enable=false;
    }



}
