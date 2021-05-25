using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerspective : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject playerCharacter;

    public SpriteRenderer bkgd;
    public float bkgd_rightBounds;
    public float bkgd_leftBounds;
    public float bkgd_topBounds;
    public float bkgd_bottomBounds;

    public float rightBounds;
    public float leftBounds;
    public float topBounds;
    public float bottomBounds;

    public float timeOffset = 3.0f;
    public Vector2 posOffset = new Vector2(0.0f, 3.2f);


    public Vector3 cameraPos;

    // Start is called before the first frame update
    void Start()
    {
        bkgd_rightBounds = bkgd.bounds.extents.x;
        bkgd_leftBounds = -bkgd.bounds.extents.x;
        bkgd_topBounds = bkgd.bounds.extents.y;
        bkgd_bottomBounds = -bkgd.bounds.extents.y;

        rightBounds = 10.4573f;
        leftBounds = -10.46121f;
        topBounds = 13.8f;
        bottomBounds = -14.8f;
    }

    // Runs right after Update
    void FixedUpdate()
    {
        cameraPos = mainCamera.GetComponent<Transform>().position;
        Vector3 startPos = cameraPos;

        Vector3 endPos = playerCharacter.GetComponent<Transform>().position;

        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -12;

        //Smooth camera movement when adjusting to playerCharacter position:
        mainCamera.GetComponent<Transform>().position =
                                        Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);

        mainCamera.GetComponent<Transform>().position = new Vector3
            (
                Mathf.Clamp(mainCamera.GetComponent<Transform>().position.x, leftBounds, rightBounds),
                Mathf.Clamp(mainCamera.GetComponent<Transform>().position.y, bottomBounds, topBounds),
                mainCamera.GetComponent<Transform>().position.z
            );

    }

    private void OnDrawGizmos()
    {
        //Draw a box around the camera boundary:
        Gizmos.color = Color.red;

        //Draw a top boundary line in editor--
        Gizmos.DrawLine(new Vector2(leftBounds, topBounds), new Vector2(rightBounds, topBounds));
        //Draw a right boundary line in editor--
        Gizmos.DrawLine(new Vector2(rightBounds, topBounds), new Vector2(rightBounds, bottomBounds));
        //Draw a left bounday line in editor--
        Gizmos.DrawLine(new Vector2(leftBounds, topBounds), new Vector2(leftBounds, bottomBounds));
        //Draw a bottom boundary line in editor--
        Gizmos.DrawLine(new Vector2(leftBounds, bottomBounds), new Vector2(rightBounds, bottomBounds));
    }
}
