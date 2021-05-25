using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMenuOption1Script : MonoBehaviour, MenuOptionScript
{
    public GameObject leftNeighborOption;
    public GameObject rightNeighborOption;

    private Transform[] letterTransforms;

    public bool labelVisible;

    // Start is called before the first frame update
    void Start()
    {
        labelVisible = true;

        letterTransforms = new Transform[]
        {
            gameObject.transform.Find("OptionLetter_A"),
            gameObject.transform.Find("OptionLetter_I"),
            gameObject.transform.Find("OptionLetter_M"),
            gameObject.transform.Find("OptionLetter_E"),
            gameObject.transform.Find("OptionLetter_D"),
            gameObject.transform.Find("OptionLetter_S"),
            gameObject.transform.Find("OptionLetter_H"),
            gameObject.transform.Find("OptionLetter_O"),
            gameObject.transform.Find("OptionLetter_T")
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetLabelInvisible()
    {
        for (int i = 0; i < letterTransforms.Length; i++)
        {
            letterTransforms[i].gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        labelVisible = false;
    }

    public void SetLabelVisible()
    {
        for (int i = 0; i < letterTransforms.Length; i++)
        {
            letterTransforms[i].gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        labelVisible = true;
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
