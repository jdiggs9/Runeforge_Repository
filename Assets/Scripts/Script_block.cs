using UnityEngine;
using System.Collections;

public class Script_block : MonoBehaviour
{
    public bool heat;
    public bool cool;
    public bool increaseG;
    public bool decreaseG;
    public bool push;
    public bool pull;

    Rigidbody rb;
    public float weightMultiplier = 2f;
    public float sizeMultiplier = 0.5f;
    private Vector3 originalScale;
    private float originalMass;
    private Renderer rend;
    private Material materialInstance;

    public Texture heatOverlay;
    public Texture coldOverlay;
    public Texture increaseOverlay;
    public Texture decreaseOverlay;
    public Texture pushOverlay;
    public Texture pullOverlay;

    private Coroutine stateResetCoroutine;
    private float cooldown;

    public Animator animator;
    public bool isSmallBlock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        heat = false;
        cool = false;
        increaseG = false;
        decreaseG = false;
        push = false;
        pull = false;
        cooldown = 0f;
        rb = GetComponent<Rigidbody>();
        originalMass = rb.mass;
        originalScale = transform.localScale;
        rend = GetComponent<Renderer>();
        materialInstance = rend.material;
        animator = GetComponent<Animator>();
        if (isSmallBlock)
        {
            animator.SetBool("Small", true);
            animator.SetBool("Large", false);
        }
        else
        {
            animator.SetBool("Small", false);
            animator.SetBool("Large", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (heat && cool)
        {
            heat = false;
            cool = false;
        }
        else if (increaseG && decreaseG)
        {
            increaseG = false;
            decreaseG = false;
        }
        else
        {
            if (increaseG)
            {
                ApplyWeightChange(originalMass * weightMultiplier);
                ApplyOverlayTexture(increaseOverlay);
                
            }
            else if (decreaseG)
            {
                ApplyWeightChange(originalMass / weightMultiplier);
                ApplyOverlayTexture(decreaseOverlay);
                
            }

            if (heat)
            {
                ApplyOverlayTexture(heatOverlay);
                if (decreaseG)
                    AdjustSize(-1);
                if (increaseG)
                    AdjustSize(1);
            }
            else if (cool)
            {
                ApplyOverlayTexture(coldOverlay);
                
            }
            if (push)
            {
                ApplyOverlayTexture(pushOverlay);
            }
            else if (pull)
            {
                ApplyOverlayTexture(pullOverlay);
            }
            if (!push && !pull && !cool && !heat && !decreaseG && !increaseG)
            {
                ClearOverlayTexture();
            }

            cooldown++;
            if (cooldown > 3000f)
            {
                cooldown = 0;
                ClearOverlayTexture();
                heat = false;
                cool = false;
                increaseG = false;
                decreaseG = false;
                rb.mass = originalMass;
            }

            
        }
    }

    private void ApplyWeightChange(float newMass)
    {
        rb.mass = newMass;
    }

    private void AdjustSize(int direction)
    {
        Vector3 targetScale = transform.localScale;

        float scaleFactor = 1f + (sizeMultiplier * direction);
        targetScale *= scaleFactor;

        if (direction == 1)
        {
            animator.SetTrigger("Inc");
            if (animator.GetBool("Small"))
            {
                StartCoroutine(ResetTrigger("Small"));
                animator.SetBool("Large", true);
            }
            else if (animator.GetBool("XS"))
            {
                StartCoroutine(ResetTrigger("XS"));
                animator.SetBool("Small", true);
            }
            else
            {
                StartCoroutine(ResetTrigger("Large"));
                animator.SetBool("XL", true);
            }
        }
        else
        {
            animator.SetTrigger("Dec");
            if (animator.GetBool("Large"))
            {
                StartCoroutine(ResetTrigger("Large"));
                animator.SetBool("Small", true);
            }
            else if (animator.GetBool("XL"))
            {
                StartCoroutine(ResetTrigger("XL"));
                animator.SetBool("Large", true);
            }
            else
            {
                StartCoroutine(ResetTrigger("Small"));
                animator.SetBool("XS", true);
            }
        }

        //Vector3 targetScale = transform.localScale;

        //float scaleFactor = 1f + (sizeMultiplier * direction);
        //targetScale *= scaleFactor;

        //Vector3 minScale = originalScale / sizeMultiplier;
        //Vector3 maxScale = originalScale * sizeMultiplier;

        //targetScale.x = Mathf.Clamp(targetScale.x, minScale.x, maxScale.x);
        //targetScale.y = Mathf.Clamp(targetScale.y, minScale.y, maxScale.y);
        //targetScale.z = Mathf.Clamp(targetScale.z, minScale.z, maxScale.z);

        //float currentHeight = transform.localScale.y;
        //float newHeight = targetScale.y;
        //float heightDifference = newHeight - currentHeight;

        //transform.position += new Vector3(0f, heightDifference / 2f, 0f);

        transform.localScale = targetScale;

    }

    private IEnumerator ResetTrigger(string param)
    {
        yield return null;
        animator.SetBool(param, false);
    }

    private void ApplyOverlayTexture(Texture overlayTexture)
    {
        if (overlayTexture != null)
        {
            this.materialInstance.SetTexture("_DetailAlbedoMap", overlayTexture);
            
            this.materialInstance.EnableKeyword("_DETAIL_MULX2");

            if (stateResetCoroutine != null)
                StopCoroutine(stateResetCoroutine);

            stateResetCoroutine = StartCoroutine(ResetStateAfterDelay(10f));
        }
    }

    private void ClearOverlayTexture()
    {
        materialInstance.SetTexture("_DetailAlbedoMap", null);
        materialInstance.DisableKeyword("_DETAIL_MULX2");
    }

    private IEnumerator ResetStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        heat = false;
        cool = false;
        increaseG = false;
        decreaseG = false;
        rb.mass = originalMass;

        ClearOverlayTexture();

        stateResetCoroutine = null;
    }


    public void AddHeat()
    {
        heat = true;
    }

    public void AddCold()
    {
        cool = true;
    }
    public void IncG()
    {
        increaseG = true;
    }
    public void DecG()
    {
        decreaseG = true;
    }
    public void Push(bool b)
    {
        push = b;
    }
    public void Pull(bool b)
    {
        pull = b;
    }


}
