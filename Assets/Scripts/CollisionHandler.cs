using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //  Parameters
    [SerializeField] float invokeDelay = 1.0f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip win;

    // CACHE
    AudioSource myAudio;

    // STATE
    bool isTransitioning = false;

    void Start()
    {
        myAudio = GetComponent<AudioSource>(); // audio
    }

    private void OnCollisionEnter(Collision other) {
        if (isTransitioning) return;
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with Friendly");
                break;

            case "Fuel":
                Debug.Log("Collided with Fuel");
                break;

            case "Finish":
                Debug.Log("You Won!");
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
        myAudio.Stop();
        myAudio.PlayOneShot(crash);
        // TODO: add particle effects for crash
        //Reload level
        Invoke("ReloadLevel", invokeDelay);
        
    }

    void StartNextLevelSequence(){
        isTransitioning = true;
        GetComponent<Movement>().enabled = false; //disable movement component
        myAudio.Stop();
        myAudio.PlayOneShot(win);

       // TODO: add particle effects for crash

        //load next level level
        Invoke("LoadNextLevel", invokeDelay);

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
