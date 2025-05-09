using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered: " + other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited: " + other.name);
    }
}
