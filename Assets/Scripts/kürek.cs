using UnityEngine;

public class kürek : MonoBehaviour
{
    public GameObject promptText;
    public GameObject animPanel;
    public string animName = "Draw";
    public Sprite kürekSprite; // Inspector'dan veya koddan ata
    public AudioSource audioSource; // Inspector'dan ata

    private bool isPlayerNear = false;
    private bool isPanelActive = false;

    void Start()
    {
        promptText.SetActive(false);
        if (animPanel != null)
            animPanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && !isPanelActive && Input.GetKeyDown(KeyCode.E))
        {
            if (animPanel != null)
                animPanel.SetActive(true);
            promptText.SetActive(false);
            isPanelActive = true;
        }
    }

    public void OnDrawButtonClick()
    {
        if (!isPanelActive) return;
        if (audioSource != null)
            audioSource.Play();
        StartCoroutine(PlayAnimAndFinish());
        isPanelActive = false;
    }

    private System.Collections.IEnumerator PlayAnimAndFinish()
    {
        var skeletonGraphic = animPanel != null ? animPanel.GetComponentInChildren<Spine.Unity.SkeletonGraphic>() : null;
        float animDuration = 1f;
        if (skeletonGraphic != null)
        {
            skeletonGraphic.AnimationState.SetAnimation(0, animName, false);
            var animData = skeletonGraphic.Skeleton.Data.FindAnimation(animName);
            animDuration = animData != null ? animData.Duration : animDuration;
        }

        yield return new WaitForSeconds(animDuration);

        if (animPanel != null)
            animPanel.SetActive(false);


        // ENVANTERE KÜREK EKLE
        Inventory inv = FindObjectOfType<Inventory>();
        if (inv != null && kürekSprite != null)
        {
            inv.AddItem("Kürek", kürekSprite);
        }

        // SES ÇAL


        Destroy(gameObject);
    }

    public GameObject aktifOlacakObje; // Inspector'dan ata

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            promptText.SetActive(true);
            isPlayerNear = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (aktifOlacakObje != null)
                aktifOlacakObje.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            promptText.SetActive(false);
            isPlayerNear = false;
        }
    }
}