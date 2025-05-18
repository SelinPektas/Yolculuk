using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LeverController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Lever Ayarlar�")]
    public Image leverImage;
    public Sprite brokenLeverSprite;
    public Sprite fixedLeverSprite;

    [Header("Highlight Ayarlar�")]
    public Image highlightImage;
    public Sprite brokenLeverHighlightSprite; // Bozuk lever i�in highlight sprite'�
    public Sprite fixedLeverHighlightSprite;  // Tamir edilmi� lever i�in highlight sprite'�
    public Color highlightHoverColor = new Color(1f, 1f, 0.5f, 1f);
    private Color _highlightOriginalColor;

    [Header("Ba�lant�lar")]
    public GameObject parentCanvasToDeactivate;

    private bool _isLeverFixed = false;

    void Start()
    {
        if (leverImage == null)
        {
            leverImage = GetComponent<Image>();
            if (leverImage == null)
            {
                Debug.LogError("LeverController: Lever Image atanmam�� veya bulunamad�.");
                enabled = false; return;
            }
        }

        if (parentCanvasToDeactivate == null)
        {
            if (transform.parent != null)
            {
                parentCanvasToDeactivate = transform.parent.gameObject;
                Debug.LogWarning("LeverController: Parent Canvas To Deactivate otomatik olarak " + parentCanvasToDeactivate.name + " olarak ayarland�. Do�rulu�undan emin olun.");
            }
            else
            {
                Debug.LogError("LeverController: Parent Canvas To Deactivate atanmam�� ve parent bulunamad�!");
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
            Debug.LogWarning("LeverController: Broken Lever Sprite atanmam��!");
        }

        if (highlightImage != null && brokenLeverHighlightSprite != null) // Highlight i�in ba�lang�� sprite'�
        {
            highlightImage.sprite = brokenLeverHighlightSprite;
        }
        else if (highlightImage != null)
        {
            Debug.LogWarning("LeverController: Broken Lever Highlight Sprite atanmam��!");
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
        if (!_isLeverFixed) // E�er lever hen�z tamir edilmemi�se (ilk t�klama)
        {
            _isLeverFixed = true;

            if (leverImage != null && fixedLeverSprite != null)
            {
                leverImage.sprite = fixedLeverSprite;
                Debug.Log("LeverController: UI Lever sprite'� 'fixedLeverSprite' olarak de�i�tirildi.");
            }
            else if (leverImage != null)
            {
                Debug.LogWarning("LeverController: Fixed Lever Sprite atanmam��!");
            }

            // Highlight image'�n sprite'�n� da fixed durumuna uygun olanla de�i�tir
            if (highlightImage != null && fixedLeverHighlightSprite != null)
            {
                highlightImage.sprite = fixedLeverHighlightSprite;
                Debug.Log("LeverController: Highlight Image sprite'� 'fixedLeverHighlightSprite' olarak de�i�tirildi.");
            }
            else if (highlightImage != null)
            {
                Debug.LogWarning("LeverController: Fixed Lever Highlight Sprite atanmam��!");
            }
        }
        else // E�er lever zaten tamir edilmi�se (ikinci t�klama)
        {
            if (parentCanvasToDeactivate != null)
            {
                parentCanvasToDeactivate.SetActive(false);
                Debug.Log("LeverController: " + parentCanvasToDeactivate.name + " deaktifle�tirildi.");
            }
        }
    }

    void OnEnable()
    {
        SetInitialState(); // Lever ve highlight sprite'lar�n� ba�lang�� durumuna ayarla
        if (highlightImage != null)
        {
            highlightImage.color = _highlightOriginalColor; // Vurgu rengini de s�f�rla
        }
    }
}