using UnityEngine;

public class Kapı : MonoBehaviour
{
    public GameObject promptTextA; // PlayerA için
    public GameObject promptTextB; // PlayerB için

    private bool isPlayerANear = false;
    private bool isPlayerBNear = false;

    void Start()
    {
        if (promptTextA != null)
            promptTextA.SetActive(false);
        if (promptTextB != null)
            promptTextB.SetActive(false);
    }

    void Update()
    {
        // Sadece PlayerA'nın movement'ı aktifse ve yakınsa
        if (isPlayerANear)
        {
            var playerA = GameObject.FindGameObjectWithTag("Player");
            if (playerA != null)
            {
                var movementA = playerA.GetComponent<PlayerMovement>();
                if (movementA != null && movementA.enabled && Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(gameObject);
                }
            }
        }
        // Sadece PlayerB'nin movement'ı aktifse ve yakınsa
        if (isPlayerBNear)
        {
            var playerB = GameObject.FindGameObjectWithTag("Player2");
            if (playerB != null)
            {
                var movementB = playerB.GetComponent<PlayerMovement>();
                if (movementB != null && movementB.enabled && Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var movement = collision.gameObject.GetComponent<PlayerMovement>();
            if (movement != null && movement.enabled)
            {
                if (promptTextA != null)
                    promptTextA.SetActive(true);
                isPlayerANear = true;
            }
        }
        else if (collision.gameObject.CompareTag("Player2"))
        {
            var movement = collision.gameObject.GetComponent<PlayerMovement>();
            if (movement != null && movement.enabled)
            {
                if (promptTextB != null)
                    promptTextB.SetActive(true);
                isPlayerBNear = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (promptTextA != null)
                promptTextA.SetActive(false);
            isPlayerANear = false;
        }
        else if (collision.gameObject.CompareTag("Player2"))
        {
            if (promptTextB != null)
                promptTextB.SetActive(false);
            isPlayerBNear = false;
        }
    }
}