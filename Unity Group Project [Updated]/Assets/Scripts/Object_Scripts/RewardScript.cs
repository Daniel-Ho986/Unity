using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardScript : MonoBehaviour
{
    public bool isVisible;
    public bool hasAppeared;
    public bool isOpen;
    public bool hasOpened;
    public bool hasClosed;

    public bool wasChosen;

    public Animator rewardAnimator;

    // Start is called before the first frame update
    void Start()
    {
        isVisible = false;
        hasAppeared = false;
        isOpen = false;
        hasOpened = false;
        hasClosed = false;

        wasChosen = false;
    }

    // Update is called once per frame
    void Update()
    {
        //When reward is made visible or DropReward() is called, make it drop from above:
        if (isVisible && (hasAppeared == false))
        {
            DropReward();
            ThumpReward();
            hasAppeared = true;
        }

        //When reward is made not visible after having already appeared, make it fly upward:
        if ((isVisible == false) && (hasAppeared == true))
        {
            HideReward();
            hasAppeared = false;
        }

        //When reward is being highlighted/clicked-on/selected, make the reward chest open:
        if ((isVisible == true) && (isOpen == true) && (hasOpened == false))
        {
            OpenReward();
            hasOpened = true;
            hasClosed = false;
        }

        //When reward is no longer being highlighted/another reward is clicked-on/selected,
        //  make the reward chest close:
        if ((isVisible == true) && (hasOpened == true) && (isOpen == false) && (hasClosed == false))
        {
            CloseReward();
            hasClosed = true;

            isOpen = false;
            hasOpened = false;
            hasAppeared = true;
        }

        //When another reward has been chosen finally, the remaining rewards are sent upward
		//  and disappear:
        if ((isVisible == false) && (hasAppeared == true))
        {
            HideReward();
            hasAppeared = false;
            isOpen = false;
        }

    }

    public void ThumpReward()
    {
        hasAppeared = true;
        rewardAnimator.SetBool("hasAppeared", true);
    }


    public void DropReward()
    {
        isVisible = true;
        rewardAnimator.SetBool("isVisible", true);
    }


    public void HideReward()
    {
        isVisible = false;
        isOpen = false;
        rewardAnimator.SetBool("isVisible", false);
        rewardAnimator.SetBool("hasAppeared", false);
    }

    public void OpenReward()
    {
        hasClosed = false;
        hasOpened = false;
        isOpen = true;
        rewardAnimator.SetBool("isOpen", true);
    }

    public void CloseReward()
    {
        hasClosed = false;
        isOpen = false;
        rewardAnimator.SetBool("isOpen", false);
        rewardAnimator.SetBool("hasClosed", true);
    }



    public bool GetChosenStatus() { return wasChosen; }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOpen = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOpen = false;
        }
    }
}
