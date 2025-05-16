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

        // Takipçi hedefini güncelle
        var follower = characterB.GetComponent<Follower>();
        if (follower != null)
        {
            follower.target = characterA.transform;
            follower.enabled = true;
        }
        var followerA = characterA.GetComponent<Follower>();
        if (followerA != null)
        {
            followerA.enabled = false;
        }
    }

    void SwitchToB()
    {
        characterA.GetComponent<PlayerMovement>().enabled = false;
        characterB.GetComponent<PlayerMovement>().enabled = true;

        // Takipçi hedefini güncelle
        var follower = characterA.GetComponent<Follower>();
        if (follower != null)
        {
            follower.target = characterB.transform;
            follower.enabled = true;
        }
        var followerB = characterB.GetComponent<Follower>();
        if (followerB != null)
        {
            followerB.enabled = false;
        }
    }
}
