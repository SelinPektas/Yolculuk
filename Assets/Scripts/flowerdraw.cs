using UnityEngine;

public class flowerdraw : MonoBehaviour
{
    public GameObject promptText;
    public GameObject animPanel;
    public GameObject aktifOlacakObje;
    public string animName = "Draw";
    public AudioSource audioSource; // Inspector'dan ata

    private bool isPlayer2Near = false;
    private bool isPanelActive = false;

    void Start()
    {
        if (promptText != null)
            promptText.SetActive(false);
        if (animPanel != null)
            animPanel.SetActive(false);
        if (aktifOlacakObje != null)
            aktifOlacakObje.SetActive(false);
    }

    void Update()
    {
        if (isPlayer2Near && !isPanelActive && Input.GetKeyDown(KeyCode.E))
        {
            if (animPanel != null)
                animPanel.SetActive(true);
            if (promptText != null)
                promptText.SetActive(false);
            isPanelActive = true;
        }
    }

    public void OnDrawButtonClick()
    {
        if (!isPanelActive) return;
        // SES ÇAL
        if (audioSource != null)
            audioSource.Play();
        StartCoroutine(PlayAnimAndFinish());
        isPanelActive = false;
    }

    public GameObject playerObjesi; // Inspector'dan ata (disable edilecek obje)
    public GameObject idlePlayerPrefab; // Inspector'dan ata (idle animasyonlu prefab)

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

        if (aktifOlacakObje != null)
            aktifOlacakObje.SetActive(true);

        // --- PLAYER'I DEVRE DIŞI BIRAK ---
        if (playerObjesi != null)
        {
            // Pozisyonunu kaydet
            Vector3 pos = playerObjesi.transform.position;
            Quaternion rot = playerObjesi.transform.rotation;
            playerObjesi.SetActive(false);

            // Aynı yere idle animasyonlu prefab instantiate et
            if (idlePlayerPrefab != null)
            {
                var idleObj = Instantiate(idlePlayerPrefab, pos, rot);
                // İstersen sorting layer veya parent ayarı yapabilirsin
            }
        }

        Destroy(gameObject);
    } // Inspector'dan ata

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            if (promptText != null)
                promptText.SetActive(true);
            isPlayer2Near = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerObjesi != null)
                playerObjesi.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            if (promptText != null)
                promptText.SetActive(false);
            isPlayer2Near = false;
        }
    }
}