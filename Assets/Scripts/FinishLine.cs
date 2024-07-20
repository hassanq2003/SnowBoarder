using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishLine : MonoBehaviour
{
    [SerializeField]float Delay=1f;
    [SerializeField]ParticleSystem finishEffect;

    AudioSource audioSource;
    // Start is called before the first frame update
    private void Start()
    {
        audioSource=GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player")
        {   audioSource.Play();
            finishEffect.Play();

            Invoke("ReloadScene",Delay);
        }
    }

    void ReloadScene()
    {
            SceneManager.LoadScene(0);
            
    }
}
