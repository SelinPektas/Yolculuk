using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 4f;
    public float stopDistance = 1.5f;
    private Animator animator;

    private Vector3 initialScale;

    void Start()
    {
        animator = GetComponent<Animator>();
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.position, followSpeed * Time.deltaTime);

            if (direction.x != 0)
                transform.localScale = new Vector3(initialScale.x * Mathf.Sign(direction.x), initialScale.y, initialScale.z);

            animator.SetFloat("Speed", Mathf.Abs(direction.x));
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }
}