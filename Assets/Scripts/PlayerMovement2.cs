using UnityEngine;
using Spine.Unity;

public class PlayerMovement2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SkeletonAnimation skeletonAnimation;
    private Vector3 initialScale;
    private AudioSource stepAudio;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        initialScale = transform.localScale;
        initialScale.x = -Mathf.Abs(initialScale.x);
        stepAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        bool isWalking = Mathf.Abs(moveInput) > 0;

        if (stepAudio != null)
        {
            if (isWalking && !stepAudio.isPlaying)
                stepAudio.Play();
            else if (!isWalking && stepAudio.isPlaying)
                stepAudio.Stop();
        }

        if (isWalking)
        {
            skeletonAnimation.AnimationName = "Walk2";
        }
        else
        {
            skeletonAnimation.AnimationName = "Idle1";
        }
    }

    private void LateUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0)
        {
            float direction = Mathf.Sign(moveInput);
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x) * direction, initialScale.y, initialScale.z);

            Transform textChild = transform.Find("textçocuk");
            if (textChild != null)
            {
                textChild.localScale = new Vector3(
                    Mathf.Abs(textChild.localScale.x) * direction * 1,
                    textChild.localScale.y,
                    textChild.localScale.z
                );
            }
        }
    }
}