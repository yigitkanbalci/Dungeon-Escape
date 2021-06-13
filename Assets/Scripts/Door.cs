using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject keyExists;
    [SerializeField]
    private GameObject doorOpen;
    [SerializeField]
    private GameObject doorLocked;
    [SerializeField]
    private AudioSource doorLockedSound;
    [SerializeField]
    private AudioSource doorOpenSound;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && keyExists.activeSelf)
        {
            other.gameObject.SetActive(false);
            doorLocked.SetActive(false);
            doorOpen.SetActive(true);
            doorOpenSound.Play();
            SceneManager.LoadScene("secondLevel");
        }
        else if(other.gameObject.tag == "Player")
        {
            doorLockedSound.Play();
        }
    }
}
