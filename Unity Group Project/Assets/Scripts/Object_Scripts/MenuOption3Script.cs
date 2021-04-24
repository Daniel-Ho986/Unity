using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOption3Script : MonoBehaviour, MenuOptionScript
{
    public GameObject leftNeighborOption;
    public GameObject rightNeighborOption;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Get methods that return a GameObject (the left or right MenuOptions)--
    public GameObject GetLeftNeighbor()
    {
        if (leftNeighborOption != null)
        {
            return leftNeighborOption;
        }
        else { return null; }
    }

    public GameObject GetRightNeighbor()
    {
        if (rightNeighborOption != null)
        {
            return rightNeighborOption;
        }
        else { return null; }
    }
}
