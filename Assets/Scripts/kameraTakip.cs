using UnityEngine;

public class kameraTakip : MonoBehaviour
{
    public Transform playerA;
    public Transform playerB;
    public float ySabiti = 0f;
    public float zSabiti = -10f;
    public float minX = -10f; // Inspector'dan ayarla
    public float maxX = 50f; // Inspector'dan ayarla

    void LateUpdate()
    {
        Transform target = null;

        // Hangi karakterde PlayerMovement aktifse onu takip et
        if (playerA != null)
        {
            var movementA = playerA.GetComponent<PlayerMovement>();
            if (movementA != null && movementA.enabled)
                target = playerA;
        }
        if (playerB != null)
        {
            var movementB = playerB.GetComponent<PlayerMovement>();
            if (movementB != null && movementB.enabled)
                target = playerB;
        }

        if (target != null)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(target.position.x, minX, maxX); // X'i sınırla
            pos.y = ySabiti;
            pos.z = zSabiti;
            transform.position = pos;
        }
    }
}