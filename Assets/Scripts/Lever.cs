using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject promptUI;
    public TrainController trainController;

    public SpriteRenderer slotSpriteRenderer; // Slotun SpriteRenderer'ı
    public Sprite insertedSprite; // Baston takılı sprite
    public Sprite pushedSprite;   // Baston ileri itilmiş sprite

    private bool isCaneInserted = false;
    private bool isLeverPushed = false;
    private bool isPlayerNear = false;
    public AudioSource leverAudio; // Inspector'dan ata

    void Update()
    {

        // Baston envanterde mi kontrol et
        bool hasCaneInInventory = false;
        Inventory inv = FindObjectOfType<Inventory>();
        if (inv != null)
            hasCaneInInventory = inv.items.Contains("Baston"); // Bastonun ismini doğru yaz

        if (isPlayerNear && !isCaneInserted && hasCaneInInventory && Input.GetKeyDown(KeyCode.E))
        {
            isCaneInserted = true;
            promptUI.SetActive(true);
            if (inv != null)
                inv.RemoveItem("Baston");
            if (slotSpriteRenderer != null && insertedSprite != null)
                slotSpriteRenderer.sprite = insertedSprite;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var movement = player.GetComponent<PlayerMovement>();
                if (movement != null)
                {
                    movement.SetHasCane(false);

                }
                var skeleton = player.GetComponent<Spine.Unity.SkeletonAnimation>();
                if (skeleton != null)
                    skeleton.AnimationState.SetAnimation(0, "Idle2", false); // false: loop olmasın, bir kez oynasın
            }
        }
        else if (isPlayerNear && isCaneInserted && !isLeverPushed && Input.GetKeyDown(KeyCode.E))
        {
            isLeverPushed = true;
            promptUI.SetActive(false);

            if (slotSpriteRenderer != null && pushedSprite != null)
                slotSpriteRenderer.sprite = pushedSprite;
            if (leverAudio != null)
                leverAudio.Play();
            if (trainController != null)
                trainController.StartTrain();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isLeverPushed)
        {
            isPlayerNear = true;
            promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            promptUI.SetActive(false);
        }
    }
}