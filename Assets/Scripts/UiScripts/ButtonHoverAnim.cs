using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonHoverAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Hover Settings")]
    public Vector2 hoverOffset = new Vector2(30f, 0f);
    public float animationDuration = 0.2f;
    public Ease animationEase = Ease.OutQuad;

    private RectTransform buttonRectTransform;
    private Vector2 originalPosition;
    private bool originalPositionCaptured = false;

    void Awake()
    {
        buttonRectTransform = GetComponent<RectTransform>();
        if (buttonRectTransform == null)
        {
            Debug.LogError("ButtonHoverAnim: RectTransform not found on " + gameObject.name + ". Script will be disabled.");
            enabled = false;
        }
    }

    void Start()
    {
        if (gameObject.activeInHierarchy && !originalPositionCaptured)
        {
            CaptureAndSetInitialPosition();
        }
    }

    void OnEnable()
    {
        if (buttonRectTransform == null)
        {
            buttonRectTransform = GetComponent<RectTransform>();
            if (buttonRectTransform == null)
            {
                if (enabled) Debug.LogError("ButtonHoverAnim: RectTransform became null on " + gameObject.name + " in OnEnable. Disabling script.");
                enabled = false;
                return;
            }
        }

        if (!originalPositionCaptured)
        {
            CaptureAndSetInitialPosition();
        }
        else
        {
            buttonRectTransform.DOKill();
            buttonRectTransform.anchoredPosition = originalPosition;
        }
    }

    void CaptureAndSetInitialPosition()
    {
        if (buttonRectTransform != null)
        {
            originalPosition = buttonRectTransform.anchoredPosition;
            originalPositionCaptured = true;
            buttonRectTransform.DOKill();
            buttonRectTransform.anchoredPosition = originalPosition;
        }
        else if (enabled)
        {
            Debug.LogError("ButtonHoverAnim: Attempted to capture position but RectTransform is null on " + gameObject.name);
            enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!originalPositionCaptured || buttonRectTransform == null) return;

        buttonRectTransform.DOKill();
        buttonRectTransform.DOAnchorPos(originalPosition + hoverOffset, animationDuration)
            .SetEase(animationEase)
            .SetUpdate(true); // Animasyonu Time.timeScale'den baðýmsýz yap
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!originalPositionCaptured || buttonRectTransform == null) return;

        buttonRectTransform.DOKill();
        buttonRectTransform.DOAnchorPos(originalPosition, animationDuration)
            .SetEase(animationEase)
            .SetUpdate(true); // Animasyonu Time.timeScale'den baðýmsýz yap
    }

    void OnDisable()
    {
        if (originalPositionCaptured && buttonRectTransform != null)
        {
            buttonRectTransform.DOKill();
            buttonRectTransform.anchoredPosition = originalPosition;
        }
    }

    void OnDestroy()
    {
        if (buttonRectTransform != null)
        {
            buttonRectTransform.DOKill();
        }
    }
}