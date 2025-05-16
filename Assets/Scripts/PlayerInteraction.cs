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
        if (isNearObject && Input.GetKeyDown(KeyCode.E) && !popupPanel.activeSelf)
        {
            popupPanel.SetActive(true);
            interactText.SetActive(false);
            if (playerMovement != null)
                playerMovement.enabled = false;
        }
        // Panel açıkken ESC ile kapat
        else if (popupPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            popupPanel.SetActive(false);
            if (playerMovement != null)
                playerMovement.enabled = true;
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
}