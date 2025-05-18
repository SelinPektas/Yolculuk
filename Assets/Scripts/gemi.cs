using UnityEngine;

public class gemi : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject yolcu1;
    public GameObject yolcu2;
    public Vector3 yolcu1Offset = new Vector3(-1f, 1f, 0f);
    public Vector3 yolcu2Offset = new Vector3(1f, 1f, 0f);

    public GameObject varisObjesi; // Varınca aktif olacak obje

    private bool hareketEt = true;

    void Start()
    {
        // ...diğer kodların...
        var skeleton = GetComponent<Spine.Unity.SkeletonAnimation>();
        if (skeleton != null)
            skeleton.AnimationState.SetAnimation(0, "Row", true); // true: sürekli tekrar etsin
    }
    void Update()
    {
        if (!hareketEt) return;

        float moveInput = 0f;
        if (Input.GetKey(KeyCode.D))
            moveInput = 1f;
        else if (Input.GetKey(KeyCode.A))
            moveInput = -1f;

        transform.position += Vector3.right * moveInput * moveSpeed * Time.deltaTime;

        if (yolcu1 != null)
            yolcu1.transform.position = transform.position + yolcu1Offset;
        if (yolcu2 != null)
            yolcu2.transform.position = transform.position + yolcu2Offset;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Eğer tag kontrolü yapmak istiyorsan:
        // if (!other.CompareTag("Finish")) return;

        hareketEt = false;

        if (yolcu1 != null)
            yolcu1.SetActive(true);
        if (yolcu2 != null)
            yolcu2.SetActive(true);

        if (varisObjesi != null)
            varisObjesi.SetActive(true);

        Destroy(gameObject);
    }
}