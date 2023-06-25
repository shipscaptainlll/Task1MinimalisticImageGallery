using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OrientationLocker
{

    public static void UpdateOrientationLock()
    {
        if (SceneChanger.CurrentScene == SceneChanger.SceneType.viewScene)
        {
            UnlockBothOrientations();
        } else
        {
            LockPortraitMode();
        }
    }

    static void LockPortraitMode()
    {
        // Allow both landscape left and landscape right
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;

        // Disallow both portrait orientations
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;

        // Set the orientation to AutoRotation, so the screen will rotate based on the device orientation
        //Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.orientation = ScreenOrientation.AutoRotation;

        Debug.Log("portrait orientation only enabled");
    }

    static void UnlockBothOrientations()
    {
        // Allow both landscape left and landscape right
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;

        // Disallow both portrait orientations
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;

        // Set the orientation to AutoRotation, so the screen will rotate based on the device orientation
        Screen.orientation = ScreenOrientation.AutoRotation;
        Debug.Log("any orientation enabled");
    }

    public static void ForcePortraitOrientation()
    {
        //Screen.orientation = ScreenOrientation.Portrait;
    }
}
