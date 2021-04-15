using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public InputManager inputManager;

    private GameObject selectReticle;

    [SerializeField] public bool makeVisible;
    [SerializeField] public bool makeInvisible;

    [SerializeField] public bool makeActive;
    [SerializeField] public bool makeInactive;

    private bool isVisible;
    private bool isActive;


    public GameObject GetReticle() { return selectReticle; }

    public void SetInvisible()
    {
        isVisible = false;
        selectReticle.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void SetVisible()
    {
        isVisible = true;
        selectReticle.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void SetInactive()
    {
        isActive = false;
        selectReticle.active = false;
    }

    public void SetActive()
    {
        isActive = true;
        selectReticle.active = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.instance;

        selectReticle = GameObject.FindWithTag("Reticle");
        selectReticle.active = false;

        isVisible = false;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (makeInvisible) { SetInvisible(); }
        else if (makeVisible) { SetVisible(); }

        if (makeActive) { SetActive(); }
        else if (makeInactive) { SetInactive(); }


        if (isActive && (inputManager.GetKeyDown(KeyBindingActions.Left1)
                         || inputManager.GetKeyDown(KeyBindingActions.Left2)))
        {
            GameObject currentSpace = selectReticle.GetComponent<ReticleScript>().GetCurrentMemorySpace();
            GameObject leftSpace = currentSpace.GetComponent<BoxScript>().GetLeftNeighborBox();
            selectReticle.transform.position = leftSpace.transform.position;
        }
        else if (isActive && (inputManager.GetKeyDown(KeyBindingActions.Right1)
                              || inputManager.GetKeyDown(KeyBindingActions.Right2)))
        {
            GameObject currentSpace = selectReticle.GetComponent<ReticleScript>().GetCurrentMemorySpace();
            GameObject rightSpace = currentSpace.GetComponent<BoxScript>().GetRightNeighborBox();
            selectReticle.transform.position = rightSpace.transform.position;
        }
        else if (isActive && (inputManager.GetKeyDown(KeyBindingActions.Select1)
                              || inputManager.GetKeyDown(KeyBindingActions.Select2)))
        {
            selectReticle.GetComponent<ReticleScript>().SetFinalChoice(true);
        }
    }
}
