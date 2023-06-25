using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppStartup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Screen.orientation = ScreenOrientation.Portrait;
        OrientationLocker.UpdateOrientationLock();
        Debug.Log("current orientation is " + Screen.orientation);
    }

    
}
