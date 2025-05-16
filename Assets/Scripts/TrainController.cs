using UnityEngine;

public class TrainController : MonoBehaviour
{
    public float speed = 5f;
    private bool isMoving = false;
    public float resetPositionX = 20f; // Arka planın sıfırlanacağı X noktası
    public float startPositionX = -20f; // Arka planın başa döneceği X noktası

    public void StartTrain()
    {
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x >= resetPositionX)
            {
                transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);
            }
        }
    }
}