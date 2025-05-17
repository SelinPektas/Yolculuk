using UnityEngine;

public class DoorDraw : MonoBehaviour
{
    public GameObject promptText;
    public GameObject doorPrefab;
    public GameObject aktifOlacakObje;
    public GameObject gosterilecekObje;
    public AudioSource audioSource;

    public GameObject animPanel; // Inspector'dan ata: açılacak panel
    public string animName = "Draw"; // Oynatılacak animasyon adı

    private bool isPlayer2Near = false;
    private bool isPanelActive = false;

    public void OnDrawButtonClick()
    {
        if (!isPanelActive) return; // Panel aktif değilse işlem yapma
        StartCoroutine(PlayAnimAndFinish());
        isPanelActive = false;
    }

    void Start()
    {
        promptText.SetActive(false);
        if (aktifOlacakObje != null)
            aktifOlacakObje.SetActive(false);
        if (gosterilecekObje != null)
            gosterilecekObje.SetActive(false);
        if (animPanel != null)
            animPanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayer2Near && !isPanelActive && Input.GetKeyDown(KeyCode.E))
        {
            // Paneli aç
            if (animPanel != null)
                animPanel.SetActive(true);
            promptText.SetActive(false);
            isPanelActive = true;
        }
        // else if (isPanelActive && Input.GetKeyDown(KeyCode.E)) // Bunu kaldır!
        // {
        //     StartCoroutine(PlayAnimAndFinish());
        //     isPanelActive = false;
        // }
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

        // Kapı instantiate, obje aktif et, ses çal
        Instantiate(doorPrefab, doorPrefab.transform.position, doorPrefab.transform.rotation);

        if (aktifOlacakObje != null)
            aktifOlacakObje.SetActive(true);

        if (audioSource != null)
            audioSource.Play();

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            promptText.SetActive(true);
            isPlayer2Near = true;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if (gosterilecekObje != null)
                gosterilecekObje.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            promptText.SetActive(false);
            isPlayer2Near = false;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if (gosterilecekObje != null)
                gosterilecekObje.SetActive(false);
        }
    }
}