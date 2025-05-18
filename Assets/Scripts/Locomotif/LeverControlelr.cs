using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LeverController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Lever Ayarlarý")]
    public Image leverImage;
    public Sprite brokenLeverSprite;
    public Sprite fixedLeverSprite;

    [Header("Highlight Ayarlarý")]
    public Image highlightImage;
    public Sprite brokenLeverHighlightSprite; // Bozuk lever için highlight sprite'ý
    public Sprite fixedLeverHighlightSprite;  // Tamir edilmiþ lever için highlight sprite'ý
    public Color highlightHoverColor = new Color(1f, 1f, 0.5f, 1f);
    private Color _highlightOriginalColor;

    [Header("Baðlantýlar")]
    public GameObject parentCanvasToDeactivate;

    private bool _isLeverFixed = false;

    void Start()
    {
        if (leverImage == null)
        {
            leverImage = GetComponent<Image>();
            if (leverImage == null)
            {
                Debug.LogError("LeverController: Lever Image atanmamýþ veya bulunamadý.");
                enabled = false; return;
            }
        }

        if (parentCanvasToDeactivate == null)
        {
            if (transform.parent != null)
            {
                parentCanvasToDeactivate = transform.parent.gameObject;
                Debug.LogWarning("LeverController: Parent Canvas To Deactivate otomatik olarak " + parentCanvasToDeactivate.name + " olarak ayarlandý. Doðruluðundan emin olun.");
            }
            else
            {
                Debug.LogError("LeverController: Parent Canvas To Deactivate atanmamýþ ve parent bulunamadý!");
                enabled = false; return;
            }
        }

        if (highlightImage != null)
        {
            _highlightOriginalColor = highlightImage.color;
        }

        SetInitialState();
    }

    private void SetInitialState()
    {
        _isLeverFixed = false;
        if (leverImage != null && brokenLeverSprite != null)
        {
            leverImage.sprite = brokenLeverSprite;
        }
        else if (leverImage != null)
        {
            Debug.LogWarning("LeverController: Broken Lever Sprite atanmamýþ!");
        }

        if (highlightImage != null && brokenLeverHighlightSprite != null) // Highlight için baþlangýç sprite'ý
        {
            highlightImage.sprite = brokenLeverHighlightSprite;
        }
        else if (highlightImage != null)
        {
            Debug.LogWarning("LeverController: Broken Lever Highlight Sprite atanmamýþ!");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (highlightImage != null)
        {
            highlightImage.color = highlightHoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (highlightImage != null)
        {
            highlightImage.color = _highlightOriginalColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isLeverFixed) // Eðer lever henüz tamir edilmemiþse (ilk týklama)
        {
            _isLeverFixed = true;

            if (leverImage != null && fixedLeverSprite != null)
            {
                leverImage.sprite = fixedLeverSprite;
                Debug.Log("LeverController: UI Lever sprite'ý 'fixedLeverSprite' olarak deðiþtirildi.");
            }
            else if (leverImage != null)
            {
                Debug.LogWarning("LeverController: Fixed Lever Sprite atanmamýþ!");
            }

            // Highlight image'ýn sprite'ýný da fixed durumuna uygun olanla deðiþtir
            if (highlightImage != null && fixedLeverHighlightSprite != null)
            {
                highlightImage.sprite = fixedLeverHighlightSprite;
                Debug.Log("LeverController: Highlight Image sprite'ý 'fixedLeverHighlightSprite' olarak deðiþtirildi.");
            }
            else if (highlightImage != null)
            {
                Debug.LogWarning("LeverController: Fixed Lever Highlight Sprite atanmamýþ!");
            }
        }
        else // Eðer lever zaten tamir edilmiþse (ikinci týklama)
        {
            if (parentCanvasToDeactivate != null)
            {
                parentCanvasToDeactivate.SetActive(false);
                Debug.Log("LeverController: " + parentCanvasToDeactivate.name + " deaktifleþtirildi.");
            }
        }
    }

    void OnEnable()
    {
        SetInitialState(); // Lever ve highlight sprite'larýný baþlangýç durumuna ayarla
        if (highlightImage != null)
        {
            highlightImage.color = _highlightOriginalColor; // Vurgu rengini de sýfýrla
        }
    }
}