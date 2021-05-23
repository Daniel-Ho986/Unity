using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_1Script : MonoBehaviour
{
    public GameObject playerCharacter;
    public GameObject portal2;

    public Animator portal1_animator;

    public bool isVisible;
    public bool hasOpened;
    public bool hasClosed;

    public bool sceneStarted;

    // Start is called before the first frame update
    void Start()
    {
        //Initializing the Portal_1's animator and various animator parameters:
        //if (portal1_animator == null) { portal1_animator = gameObject.GetComponent<Animator>(); }
        //portal1_animator.SetBool("isVisible", false);
        //portal1_animator.SetBool("isOpening", false);
        //portal1_animator.SetBool("hasOpened", false);
        //portal1_animator.SetBool("isClosing", false);
        //portal1_animator.SetBool("hasClosed", false);

        //Initializing various bool flags:
        isVisible = false;
        hasOpened = false;
        hasClosed = false;

        sceneStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {

    }

    void OnTriggerExit2D(Collider2D other)
    {

    }
}
