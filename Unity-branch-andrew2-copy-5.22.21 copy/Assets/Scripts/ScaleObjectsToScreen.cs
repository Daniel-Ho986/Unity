using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObjectsToScreen : MonoBehaviour
{
    public GameObject bkgdImage;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        ScaleObjectsFitScreenSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ScaleObjectsFitScreenSize()
    {
        //Step1: Get the device's screen aspect ratio--
        Vector2 deviceScreenResolution = new Vector2(Screen.width, Screen.height);
        print(deviceScreenResolution);

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float DEVICE_SCREEN_ASCPECT = screenWidth / screenHeight;
        print("DEVICE_SCREEN_ASPECT: " + DEVICE_SCREEN_ASCPECT.ToString());


        //Step2: Set the Main Camera's aspect ratio = Device's aspect ratio--
        mainCamera.aspect = DEVICE_SCREEN_ASCPECT;


        //Step3: Scale all objects to fit the Main Camera's screen size--
        float cameraHeight = 100.0f * mainCamera.orthographicSize * 2.0f;
        float cameraWidth = cameraHeight * DEVICE_SCREEN_ASCPECT;
        print("cameraHeight: " + cameraHeight.ToString());
        print("cameraWidth: " + cameraWidth.ToString());


        //Get object size--
        SpriteRenderer bkgdImageSR = bkgdImage.GetComponent<SpriteRenderer>();
        float bkgdImageHeight = bkgdImageSR.sprite.rect.height;
        float bkgdImageWidth = bkgdImageSR.sprite.rect.width;

        print("bkgdImageHeight: " + bkgdImageHeight.ToString());
        print("bkgdImageWidth: " + bkgdImageWidth.ToString());


        //Calculate ratio for scaling object--
        float bgImg_scale_ratio_height = cameraHeight / bkgdImageHeight;
        float bgImg_scale_ratio_width = cameraWidth / bkgdImageWidth;

        bkgdImage.transform.localScale = new Vector3(bgImg_scale_ratio_width, bgImg_scale_ratio_height, 1);
    }
}
