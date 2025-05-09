using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_Powers : MonoBehaviour
{

    public float magForce;
    public float magRange;
    public Transform camOrientation;
    Rigidbody rb;
    Rigidbody objectRB;
    public LayerMask whatIsMetal;
    public LayerMask whatIsGround;
    public LayerMask whatIsBlock;

    private float powerSelected;
    private GameObject obj;
    private Ray ray;
    private RaycastHit hit;
    RaycastHit[] hits = new RaycastHit[2];

    private bool isMetal = false;
    public GameObject magSelect;
    public GameObject gavSelect;
    public GameObject tempSelect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        powerSelected = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ////metalObject = Physics.Raycast(ray, magRange, whatIsMetal);
        //if (Physics.Raycast(ray, out hit, magRange, whatIsMetal))
        //{
        //    if (hit.)
        //}
        //RaycastHit.transform.gameObject : GameObject

        obj = GetGameObject();
        MyInput();
    }

    GameObject GetGameObject()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, magRange, whatIsMetal)) 
        {
            isMetal = true;
            objectRB = hit.transform.gameObject.GetComponent<Rigidbody>();
            return hit.transform.gameObject;
        }
        else if (Physics.Raycast(ray, out hit, magRange, whatIsBlock))
        {
            isMetal = false;
            objectRB = hit.transform.gameObject.GetComponent<Rigidbody>();
            return hit.transform.gameObject;
        }
        else
        {
            isMetal = false;
            objectRB = null;
            return null;
        }

    }

    private void MyInput()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (powerSelected == 1f)
        {
            Magnitism();
        } else if (powerSelected == 2f)
        {
            Gravity();
        } else if (powerSelected == 3f)
        {
            Temperature();
        }
        if (Input.GetButtonDown("Mag") && (sceneIndex >= 1)) {
            powerSelected = 1f;
            magSelect.SetActive(true);
            gavSelect.SetActive(false);
            tempSelect.SetActive(false);
        }
        if (Input.GetButtonDown("Grav") && (sceneIndex >= 2)) {
            powerSelected = 2f;
            magSelect.SetActive(false);
            gavSelect.SetActive(true);
            tempSelect.SetActive(false);
        }
        if (Input.GetButtonDown("Temp") && (sceneIndex >= 3)) {
            powerSelected = 3f;
            magSelect.SetActive(false);
            gavSelect.SetActive(false);
            tempSelect.SetActive(true);
        }
    }

    private void Magnitism()
    {
        if (Input.GetMouseButton(0))
        {
            if ((obj != null) && (objectRB.mass < rb.mass) && isMetal)
            {
                //if (Physics.Raycast(GetGameObject().transform.position, ray, 1.05f, whatIsGround))
                if (Physics.RaycastNonAlloc(ray, hits, Vector3.Distance(transform.position, GetGameObject().transform.position) + 0.7f) == 2)
                {
                    obj.GetComponent<Script_block>().Push(true);
                    rb.AddForce(-1 * ray.direction * magForce * 0.5f, ForceMode.Impulse);
                }
                else
                {
                    obj.GetComponent<Script_block>().Push(true);
                    objectRB.AddForce(ray.direction * magForce * 0.5f, ForceMode.Impulse);
                }
            }
            else if (obj != null && isMetal)
            {
                obj.GetComponent<Script_block>().Push(true);
                rb.AddForce(-1 * ray.direction * magForce * 0.5f, ForceMode.Impulse);
            }
        }
        else if (Input.GetMouseButton(1))
        {
            if ((obj != null) && (objectRB.mass < rb.mass) && isMetal)
            {
                obj.GetComponent<Script_block>().Pull(true);
                objectRB.AddForce(-1 * ray.direction * magForce * 0.5f, ForceMode.Impulse);
            }
            else if (obj != null && isMetal)
            {
                obj.GetComponent<Script_block>().Pull(true);
                rb.AddForce(ray.direction * magForce * 2f, ForceMode.Impulse);
            }
        }
        else if (obj != null)
        {
            obj.GetComponent<Script_block>().Pull(false);
            obj.GetComponent<Script_block>().Push(false);
        }
    }

    private void Gravity()
    {
        if (Input.GetMouseButton(0) && (obj != null))
        {
            obj.GetComponent<Script_block>().IncG();
        }
        if (Input.GetMouseButton(1) && (obj != null))
        {
            obj.GetComponent<Script_block>().DecG();
        }
    }
    private void Temperature()
    {
        if (Input.GetMouseButton(0) && (obj != null))
        {
            obj.GetComponent<Script_block>().AddHeat();
        }
        if (Input.GetMouseButton(1) && (obj != null))
        {
            obj.GetComponent<Script_block>().AddCold();
        }
    }

}
