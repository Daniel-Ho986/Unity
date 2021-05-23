using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BoxScript
{
    int GetNumber();
    GameObject GetDataValue();
    GameObject GetLeftNeighborBox();
    GameObject GetRightNeighborBox();

}
