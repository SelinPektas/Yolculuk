using UnityEngine;

public class BridgeDraw : MonoBehaviour
{
    public GameObject promptText;
    public GameObject animPanel; // Panel (Canvas) Inspector'dan ata
    public string animName = "Draw"; // Oynatılacak animasyon adı
    public GameObject aktifOlacakObje; // Animasyon bitince aktif olacak obje

    private bool isPlayerNear = false;
    private bool isPanelActive = false;

    void Start()
    {
        promptText.SetActive(false);
        if (aktifOlacakObje != null)
            aktifOlacakObje.SetActive(false);
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
        StartCoroutine(PlayAnimAndFinish());
        isPanelActive = false;
    }

    private System.Collections.IEnumerator PlayAnimAndFinish()
    {
        // Paneldeki SkeletonGraphic'i bul
        var skeletonGraphic = animPanel != null ? animPanel.GetComponentInChildren<Spine.Unity.SkeletonGraphic>() : null;
        float animDuration = 1f;
        if (skeletonGraphic != null)
        {
            skeletonGraphic.AnimationState.SetAnimation(0, animName, false);
            var animData = skeletonGraphic.Skeleton.Data.FindAnimation(animName);
            animDuration = animData != null ? animData.Duration : animDuration;
        }

        yield return new WaitForSeconds(animDuration);

        // Paneli kapat
        if (animPanel != null)
            animPanel.SetActive(false);

        // Köprü aktif et
        if (aktifOlacakObje != null)
            aktifOlacakObje.SetActive(true);

        // Bu scriptin bağlı olduğu objeyi yok et
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            promptText.SetActive(true);
            isPlayerNear = true;
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