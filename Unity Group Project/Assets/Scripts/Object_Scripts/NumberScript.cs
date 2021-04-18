using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface NumberScript
{ 
    int GetNumber();
    bool GetLeftMouseDown();
    bool GetRightMouseDown();
    bool GetMiddleMouseDown();
    void SetLeftMouseDown(bool truthValue);
    void SetRightMouseDown(bool truthValue);
    void SetMiddleMouseDown(bool truthValue);

    void onClickAction();

    void onHoldAction(float xPos, float yPos);

    void onDropAction(float xPos, float yPos);

}
