using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberScript : MonoBehaviour, IClick
{
    public void onClickAction()
    {
        //Do nothing on click
    }

    public void onHoldAction(float xPos, float yPos, bool isPressedDown)
    {
        if (isPressedDown)
        {
            transform.position = new Vector2(xPos, yPos);
        }
    }

    public void onDropAction(float xPos, float yPos)
    {
        transform.position = new Vector2(xPos, yPos);
    }

}
