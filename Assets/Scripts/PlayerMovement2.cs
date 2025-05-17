using UnityEngine;
using Spine.Unity;

public class PlayerMovement2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SkeletonAnimation skeletonAnimation;
    private Vector3 initialScale;

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
            transform.localScale = new Vector3(initialScale.x * direction * -1, initialScale.y, initialScale.z);

            // Text child'ının scale'ini karakterin yönüne göre düz veya ters yap
            Transform textChild = transform.Find("text");
            if (textChild != null)
            {
                Vector3 textScale = textChild.localScale;
                textScale.x = Mathf.Abs(textScale.x) * Mathf.Sign(initialScale.x) * direction;
                textChild.localScale = textScale;
            }
        }
        // ----------- Normal Animasyon Kontrolü -----------
        if (Mathf.Abs(moveInput) > 0)
        {
            skeletonAnimation.AnimationName = "Walk2";
        }
        else
        {
            skeletonAnimation.AnimationName = "Idle1";
        }
    }
}