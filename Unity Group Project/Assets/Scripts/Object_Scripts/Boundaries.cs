using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<Collider2D>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<Collider2D>().bounds.extents.y; //etents = size of height / 2
    }

    // LateUpdate is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, (screenBounds.x * -1 + objectWidth), screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, (screenBounds.y * -1 + objectHeight), screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
}
