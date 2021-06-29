using UnityEngine;

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
                break;
        }
    }
}
