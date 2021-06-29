using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float invokeDelay = 1.0f;
    private void OnCollisionEnter(Collision other) {
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
        //disable movement component
        GetComponent<Movement>().enabled = false;
        // TODO: add SFX for crash
        // TODO: add particle effects for crash
        //Reload level
        Invoke("ReloadLevel", invokeDelay);
    }

    void StartNextLevelSequence(){
        //disable movement component
        GetComponent<Movement>().enabled = false;

        // TODO: add SFX for win        
        // TODO: add particle effects for crash

        //load next level level
        Invoke("LoadNextLevel", invokeDelay);
    }

    void ReloadLevel(){
        // SceneManager.LoadScene("Sandbox");

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // current scene index for the 'Level'
        SceneManager.LoadScene(currentSceneIndex);
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
    }
}
