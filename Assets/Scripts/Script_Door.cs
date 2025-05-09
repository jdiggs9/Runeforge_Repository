using UnityEngine;

public class Script_Door : MonoBehaviour
{
    public Animator animator;
    private bool open;

    void Start()
    {
        
        open = false;
    }

    public void ToggleDoor()
    {
        open = !open;
        animator.SetBool("Open", open);
    }
}
