using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FlagEnd : MonoBehaviour
{
    //Add this Script Directly to The Death Zone
    [SerializeField] private AudioClip saw;    // Add your Audi Clip Here;
    [SerializeField] private string sceneNext;    // Add your Audi Clip Here;
    private AudioSource audioSource;


    // This Will Configure the  AudioSource Component; 
    void Start()
        {
        // Attempt to get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // If no AudioSource is found, add one dynamically
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.clip = saw;
    }

    void OnTriggerEnter(Collider other) // Ensure it has a parameter
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        StartCoroutine(LoadSceneAfterDelay(3f));  // Start coroutine with a 3-second delay
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneNext);
    }
}