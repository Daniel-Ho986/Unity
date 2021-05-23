using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPortalScript : MonoBehaviour
{
    public GameObject playerCharacter;

    public Animator startPortal_animator;

    public bool isVisible;
    public bool hasOpened;
    public bool hasClosed;

    public bool sceneStarted;

    // Start is called before the first frame update
    void Start()
    {
        //Initializing the StartPortal's animator and various animator parameters:
        if (startPortal_animator == null) { startPortal_animator = gameObject.GetComponent<Animator>(); }
        startPortal_animator.SetBool("isVisible", false);
        startPortal_animator.SetBool("isOpening", false);
        startPortal_animator.SetBool("hasOpened", false);
        startPortal_animator.SetBool("isClosing", false);
        startPortal_animator.SetBool("hasClosed", false);

        //Initializing various bool flags:
        isVisible = false;
        hasOpened = false;
        hasClosed = false;

        sceneStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneStarted == false)
        {
            sceneStarted = true;
            StartCoroutine(OpeningSceneCoroutine());
        }
    }

    IEnumerator OpeningSceneCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        startPortal_animator.SetBool("isVisible", true);
        startPortal_animator.SetBool("isOpening", true);
        yield return new WaitForSeconds(1.0f);
        startPortal_animator.SetBool("hasOpened", true);
        startPortal_animator.SetBool("isOpening", false);
        yield return new WaitForSeconds(3.5f);
        startPortal_animator.SetBool("isClosing", true);
        yield return new WaitForSeconds(0.8f);
        startPortal_animator.SetBool("hasClosed", true);
        startPortal_animator.SetBool("isClosing", false);
        startPortal_animator.SetBool("isVisible", false);
        playerCharacter.GetComponent<PlayerController_Overworld>().isWaiting = false; 
        gameObject.SetActive(false);
    }
}
