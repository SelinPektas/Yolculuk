using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject popupPanel;         // Açılacak panel
    public GameObject interactText;       // "E'ye bas" yazısı
    private bool isNearObject = false;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (isNearObject && Input.GetKeyDown(KeyCode.E))
        {
            if (!popupPanel.activeSelf)
            {
                popupPanel.SetActive(true);
                interactText.SetActive(false);
                if (playerMovement != null)
                    playerMovement.enabled = false;
            }
            else
            {
                popupPanel.SetActive(false);
                if (playerMovement != null)
                    playerMovement.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            isNearObject = true;
            interactText.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            isNearObject = false;
            interactText.SetActive(false);
        }
    }
}