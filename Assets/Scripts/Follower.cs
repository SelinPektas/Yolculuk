using UnityEngine;
using Spine.Unity;

public class Follower : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 4f;
    public float stopDistance = 1.5f;
    private SkeletonAnimation skeletonAnimation;
    private Vector3 initialScale;
    private AudioSource stepAudio; // Adım sesi için

    private Rigidbody2D rb;

    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        initialScale = transform.localScale;
        stepAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D'yi al
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            Vector2 direction = (target.position - transform.position).normalized;

            // Flip işlemi
            if (direction.x != 0)
            {
                transform.localScale = new Vector3(
                    Mathf.Abs(initialScale.x) * Mathf.Sign(direction.x),
                    initialScale.y,
                    initialScale.z
                );
            }

            // Rigidbody2D ile hareket
            if (rb != null)
                rb.velocity = new Vector2(direction.x * followSpeed, rb.velocity.y);

            if (skeletonAnimation != null)
                skeletonAnimation.AnimationName = "Walk2";

            if (stepAudio != null && !stepAudio.isPlaying)
                stepAudio.Play();
        }
        else
        {
            if (rb != null)
                rb.velocity = new Vector2(0, rb.velocity.y);

            if (skeletonAnimation != null)
                skeletonAnimation.AnimationName = "Idle1";

            if (stepAudio != null && stepAudio.isPlaying)
                stepAudio.Stop();
        }
    }
}