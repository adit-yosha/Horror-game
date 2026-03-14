using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingDoor : MonoBehaviour
{
    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            SceneManager.LoadScene("WinScreen");
        }
    }
}
