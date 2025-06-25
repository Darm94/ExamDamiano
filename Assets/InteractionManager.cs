using UnityEngine;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private string interactInput = "e";
    [SerializeField] private LayerMask interactLayer;

    [SerializeField] private GameObject light1;
    [SerializeField] private GameObject light2;
    private bool lightOn = true;
    
    
    private Collider currentTarget;

    private bool hasTorch = false;
    private bool hasKey = false;

    private void Start()
    {
        if (interactionText != null)
            interactionText.enabled = false;
    }

    private void Update()
    {
        if (currentTarget != null && Input.GetKeyDown(interactInput))
        {
            HandleInteraction(currentTarget.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggher");
        if (IsInInteractLayer(other.gameObject))
        {
            Debug.Log("has collided");
            currentTarget = other;
            if (interactionText != null)
                interactionText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == currentTarget)
        {
            currentTarget = null;
            if (interactionText != null)
                interactionText.enabled = false;
        }
    }

    private bool IsInInteractLayer(GameObject obj)
    {
        return ((1 << obj.layer) & interactLayer) != 0;
    }

    private void HandleInteraction(GameObject obj)
    {
        string tag = obj.tag.ToLower();

        //interactable objects if have time TODO
        switch (tag)
        {
            case "torch":
                hasTorch = true;
                Destroy(obj);
                break;

            case "key":
                hasKey = true;
                Destroy(obj);
                break;
            
            case "switch":
                if (lightOn)
                {
                    light1.SetActive(false);
                    light2.SetActive(false);
                    lightOn = false;
                }
                else
                {
                    light1.SetActive(true);
                    light2.SetActive(true);
                    lightOn = true;
                }
                break;

            case "panel":
                if(hasKey)
                    obj.SendMessage("ActivatePanel", SendMessageOptions.DontRequireReceiver);
                break;

            default:
                Debug.Log("No interaction for tag: " + tag);
                break;
        }

        if (interactionText != null)
            interactionText.enabled = false;

        currentTarget = null;
    }

    // public properies
    public bool HasTorch() => hasTorch;
    public bool HasKey() => hasKey;
}
