using UnityEngine;

public class Script_Kill : MonoBehaviour
{
    public GameObject player;
    public GameObject obj;
    public Transform playerRespawnPoint;
    public Transform objectRespawnPoint;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.position = playerRespawnPoint.position;
        }
        else
        {
            obj.transform.position = objectRespawnPoint.position;
        }
    }
}
