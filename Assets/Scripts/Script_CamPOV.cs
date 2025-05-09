using UnityEngine;

public class Script_CamPOV : MonoBehaviour
{

    public Transform camPosition;

    // Update is called once per frame
    void Update()
    {
        transform.position = camPosition.position;
    }
}
