using System.Collections.Generic;
using UnityEngine;

public class Script_Button : MonoBehaviour
{
    public float weightThreshold;
    public GameObject door;

    public Animator animator;
    private float totalWeight = 0f;
    private HashSet<Rigidbody> objectsOnPlate = new HashSet<Rigidbody>();
    private bool isPressed = false;

    private void OnTriggerEnter(Collider col)
    {
        Rigidbody rb = col.attachedRigidbody;

        if (rb != null && !objectsOnPlate.Contains(rb))
        {
            
            objectsOnPlate.Add(rb);
            totalWeight += rb.mass;
            
            CheckWeight();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        Rigidbody rb = col.attachedRigidbody;

        if (rb != null && objectsOnPlate.Contains(rb))
        {
            
            objectsOnPlate.Remove(rb);
            totalWeight -= rb.mass;
            
            CheckWeight();
        }
    }

    private void CheckWeight()
    {
        

        if (totalWeight >= weightThreshold && !isPressed)
        {
            
            TogglePress(true);
        }
        else if (totalWeight < weightThreshold && isPressed)
        {
            
            TogglePress(false);
        }
    }

    private void TogglePress(bool press)
    {
        isPressed = press;
        animator.SetBool("On", press);
        door.GetComponent<Script_Door>().ToggleDoor();
        
    }
}
