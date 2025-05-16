using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    public GameObject characterA;
    public GameObject characterB;
    private bool isAActive = true;

    void Start()
    {
        characterA.SetActive(true);
        characterB.SetActive(true);
        SwitchToA();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isAActive = !isAActive;

            if (isAActive)
                SwitchToA();
            else
                SwitchToB();
        }
    }

    void SwitchToA()
    {
        characterA.GetComponent<PlayerMovement>().enabled = true;
        characterB.GetComponent<PlayerMovement>().enabled = false;
    }

    void SwitchToB()
    {
        characterA.GetComponent<PlayerMovement>().enabled = false;
        characterB.GetComponent<PlayerMovement>().enabled = true;
    }
}
