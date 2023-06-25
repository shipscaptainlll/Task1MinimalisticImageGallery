using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOrientationHandler : MonoBehaviour
{
    [SerializeField] GameObject landscapeCanvas;
    [SerializeField] GameObject portraitCanvas;
    ScreenOrientation currentOrientation;
    Coroutine orientationCheckCoroutine;

    void Awake()
    {
        currentOrientation = Screen.orientation; // Initialize to the current screen orientation
        //UpdateOrientation();
    }

    void Start()
    {
        
        orientationCheckCoroutine = StartCoroutine(CheckForOrientationChange());
    }

    IEnumerator CheckForOrientationChange()
    {
        while (true) // Infinite loop
        {
            // If the current screen orientation doesn't match our stored orientation
            if (Screen.orientation != currentOrientation)
            {
                currentOrientation = Screen.orientation; // Update the current orientation
                UpdateOrientation(); // Run your orientation changed code
            }

            yield return new WaitForSeconds(0.5f); // Wait half a second before checking again
        }
    }

    public void UpdateOrientation()
    {
        if (currentOrientation == ScreenOrientation.LandscapeLeft
            || currentOrientation == ScreenOrientation.LandscapeRight)
        {
            portraitCanvas.SetActive(false);
            landscapeCanvas.SetActive(true);
        } else if (currentOrientation == ScreenOrientation.Portrait
            || currentOrientation == ScreenOrientation.PortraitUpsideDown)
        {
            landscapeCanvas.SetActive(false);
            portraitCanvas.SetActive(true);
        }
        // Add your code here for when the screen orientation changes
    }


    void OnDestroy()
    {
        StopCoroutine(orientationCheckCoroutine);
    }
}
