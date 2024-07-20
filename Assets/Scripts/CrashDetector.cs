using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CrashDetector : MonoBehaviour
{
    // Start is called before the first frame update
[SerializeField]float Delay=0.6f;
[SerializeField]ParticleSystem crashEffect;
[SerializeField]AudioSource audioSource;


private void Start()
{
    
}
private void OnTriggerEnter2D(Collider2D other) 
{
   if(other.tag=="Ground")
   {
    FindAnyObjectByType<PlayerController>().DisableControls();
    audioSource.Play();
    crashEffect.Play();
    Invoke("ReloadScene",Delay);
   } 
}

void ReloadScene()
    {   
            SceneManager.LoadScene(1);
            Debug.Log("Finished");
    }


}
