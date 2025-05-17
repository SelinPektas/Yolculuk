using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject popupPanel;         // Açılacak panel
    public GameObject interactText;       // "E'ye bas" yazısı
    private bool isNearObject = false;
    private PlayerMovement playerMovement1;
    private PlayerMovement2 playerMovement2;

    void Start()
    {
        playerMovement1 = GetComponent<PlayerMovement>();
        playerMovement2 = GetComponent<PlayerMovement2>();
    }

    void Update()
    {
        if (isNearObject && Input.GetKeyDown(KeyCode.E))
        {
            if (!popupPanel.activeSelf)
            {
                popupPanel.SetActive(true);
                if (playerMovement1 != null) playerMovement1.enabled = false;
                if (playerMovement2 != null) playerMovement2.enabled = false;
            }
            else
            {
                popupPanel.SetActive(false);
                if (playerMovement1 != null) playerMovement1.enabled = true;
                if (playerMovement2 != null) playerMovement2.enabled = true;
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