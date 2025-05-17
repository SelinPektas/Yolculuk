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

    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        initialScale = transform.localScale;
        stepAudio = GetComponent<AudioSource>(); // AudioSource'u al
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.position, followSpeed * Time.deltaTime);

            // Flip işlemi
            if (direction.x != 0)
            {
                transform.localScale = new Vector3(
                    Mathf.Abs(initialScale.x) * Mathf.Sign(direction.x),
                    initialScale.y,
                    initialScale.z
                );
            }

            // Takip edenin animasyonu
            if (skeletonAnimation != null)
                skeletonAnimation.AnimationName = "Walk2";

            // Adım sesi kontrolü
            if (stepAudio != null && !stepAudio.isPlaying)
                stepAudio.Play();
        }
        else
        {
            // Durma animasyonu
            if (skeletonAnimation != null)
                skeletonAnimation.AnimationName = "Idle1";

            // Adım sesini durdur
            if (stepAudio != null && stepAudio.isPlaying)
                stepAudio.Stop();
        }
    }
}