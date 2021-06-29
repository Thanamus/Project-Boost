using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
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
                break;

            default:
                Debug.Log("Collided with something else");
                ReloadLevel();
                break;
        }
    }

    void ReloadLevel(){
        // SceneManager.LoadScene("Sandbox");

    
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // current scene index for the 'Level'
        SceneManager.LoadScene(currentSceneIndex);
    }
}
