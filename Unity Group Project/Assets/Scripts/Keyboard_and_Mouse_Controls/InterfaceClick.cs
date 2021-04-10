using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClick
{
    void onClickAction();

    void onHoldAction(float xPos, float yPos, bool isPressedDown);

    void onDropAction(float xPos, float yPos);

}
