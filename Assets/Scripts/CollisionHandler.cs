using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //  Parameters
    [SerializeField] float invokeDelay = 1.0f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip win;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem winParticles;

    // CACHE
    AudioSource myAudio; // Audio

    // STATE
    bool isTransitioning = false;
    bool collisionDisabled = false;    

    void Start()
    {
        myAudio = GetComponent<AudioSource>(); // audio
    }

    void Update() {
        RespondToDebugKeys();    
    }


    private void RespondToDebugKeys() //Cheat Keys
    {
        if(Input.GetKeyDown(KeyCode.L)) 
        {
            LoadNextLevel();
        } else if (Input.GetKeyDown(KeyCode.C)) {
            collisionDisabled = !collisionDisabled; // toggle disable
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (isTransitioning || collisionDisabled) return;


        switch (other.gameObject.tag)
        {
            case "Friendly":
                // Debug.Log("Collided with Friendly");
                break;

            case "Fuel":
                // Debug.Log("Collided with Fuel");
                break;

            case "Finish":
                // Debug.Log("You Won!");
                StartNextLevelSequence();
                break;

            default:
                // Debug.Log("Collided with something else");
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence(){
        isTransitioning = true;
        GetComponent<Movement>().enabled = false; //disable movement component
        myAudio.Stop(); // audio
        myAudio.PlayOneShot(crash); // audio
        crashParticles.Play(); // particles
        Invoke("ReloadLevel", invokeDelay); //Reload level
        
    }

    void StartNextLevelSequence(){
        isTransitioning = true;
        GetComponent<Movement>().enabled = false; //disable movement component
        myAudio.Stop(); // audio
        myAudio.PlayOneShot(win); // audio
        winParticles.Play(); // particles
        Invoke("LoadNextLevel", invokeDelay); //load next level
    }

    void ReloadLevel(){
        // SceneManager.LoadScene("Sandbox");

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // current scene index for the 'Level'
        SceneManager.LoadScene(currentSceneIndex);
        isTransitioning = false;
    }

    void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // current scene index for the 'Level'
        int nextSceneIndex = currentSceneIndex + 1;

        // If last level, reset game
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        isTransitioning = false;
    }
}
