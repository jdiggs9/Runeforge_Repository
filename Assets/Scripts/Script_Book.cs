using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Script_Book : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
