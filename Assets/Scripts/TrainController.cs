using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainController : MonoBehaviour
{
    public float speed = 5f;
    private bool isMoving = false;
    private float timer = 0f;

    public void StartTrain()
    {
        isMoving = true;
        timer = 0f;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            timer += Time.deltaTime;
            if (timer >= 5f)
            {
                SceneManager.LoadScene("Scene2");
            }
        }
    }
}