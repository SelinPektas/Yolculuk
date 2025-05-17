using UnityEngine;

using Spine.Unity; // Eklemeyi unutma

public class Follower : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 4f;
    public float stopDistance = 1.5f;
    private Animator animator;
    private SkeletonAnimation skeletonAnimation; // Takip edenin animasyonu için

    private Vector3 initialScale;

    void Start()
    {
        animator = GetComponent<Animator>();
        skeletonAnimation = GetComponent<SkeletonAnimation>(); // Takip edenin animasyonu
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        // Hedefte hangi movement scripti var?
        var movement1 = target.GetComponent<PlayerMovement>();
        var movement2 = target.GetComponent<PlayerMovement2>();

        if (distance > stopDistance)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.position, followSpeed * Time.deltaTime);

            // Flip işlemi (scale ile)
            if (direction.x != 0)
            {
                transform.localScale = new Vector3(
                    Mathf.Abs(initialScale.x) * Mathf.Sign(direction.x),
                    initialScale.y,
                    initialScale.z
                );
            }

            // Takip edenin animasyonunu güncelle
            if (skeletonAnimation != null)
            {
                if (movement2 != null)
                    skeletonAnimation.AnimationName = "Walk";
                else if (movement1 != null)
                    skeletonAnimation.AnimationName = "Walk2";
            }

            if (animator != null)
                animator.SetFloat("Speed", Mathf.Abs(direction.x));
        }
        else
        {
            // Takip edenin animasyonunu güncelle (durma animasyonu)
            if (skeletonAnimation != null)
            {
                if (movement2 != null)
                    skeletonAnimation.AnimationName = "NoCrayon"; // Player2 için durma animasyonu
                else if (movement1 != null)
                    skeletonAnimation.AnimationName = "Idle1";    // Player1 için durma animasyonu
            }

            if (animator != null)
                animator.SetFloat("Speed", 0);
        }
    }
}