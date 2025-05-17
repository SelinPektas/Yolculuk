using UnityEngine;
using Spine.Unity;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SkeletonAnimation skeletonAnimation;
    private bool hasCane = false;
    private Vector3 initialScale;

    private bool wasWalking = false;
    private bool waitingForIdle = false;

    public void SetHasCane(bool value)
    {
        hasCane = value;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        initialScale = transform.localScale;
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip sadece karakterin görseline uygulansın, text etkilenmesin
        if (moveInput != 0)
        {
            float direction = Mathf.Sign(moveInput);
            transform.localScale = new Vector3(initialScale.x * direction, initialScale.y, initialScale.z);

            // Text child'ının scale'ini karakterin yönüne göre düz veya ters yap
            Transform textChild = transform.Find("text"); // Text objenin adını gir
            if (textChild != null)
            {
                Vector3 textScale = textChild.localScale;
                // Karakter sağa bakarken +, sola bakarken - olacak şekilde ayarla
                textScale.x = Mathf.Abs(textScale.x) * Mathf.Sign(initialScale.x) * direction;
                textChild.localScale = textScale;
            }
            Transform text1Child = transform.Find("text1");
            if (text1Child != null)
            {
                Vector3 text1Scale = text1Child.localScale;
                text1Scale.x = Mathf.Abs(text1Scale.x) * Mathf.Sign(initialScale.x) * direction;
                text1Child.localScale = text1Scale;
            }
        }

        if (hasCane)
        {
            if (Mathf.Abs(moveInput) > 0)
            {
                // Walk2 animasyonu loop'lu oynasın
                if (skeletonAnimation.AnimationName != "Walk2")
                    skeletonAnimation.AnimationState.SetAnimation(0, "Walk2", true);
                wasWalking = true;
                waitingForIdle = false;
            }
            else
            {
                if (wasWalking)
                {
                    // Walk2'den çıkınca önce No, sonra Idle1
                    skeletonAnimation.AnimationState.SetAnimation(0, "No", false);
                    skeletonAnimation.AnimationState.AddAnimation(0, "Idle1", true, 0);
                    wasWalking = false;
                    waitingForIdle = true;
                }
                else if (!waitingForIdle)
                {
                    skeletonAnimation.AnimationName = "Idle1";
                }
            }
        }
        else
        {
            if (Mathf.Abs(moveInput) > 0)
            {
                // Walk1 animasyonu loop'lu oynasın
                if (skeletonAnimation.AnimationName != "Walk1")
                    skeletonAnimation.AnimationState.SetAnimation(0, "Walk1", true);
                wasWalking = true;
                waitingForIdle = false;
            }
            else
            {
                if (wasWalking)
                {
                    // Walk1'den çıkınca direkt Idle2
                    skeletonAnimation.AnimationState.SetAnimation(0, "Idle2", true);
                    wasWalking = false;
                    waitingForIdle = true;
                }
                else if (!waitingForIdle)
                {
                    skeletonAnimation.AnimationName = "Idle2";
                }
            }
        }
    }
}