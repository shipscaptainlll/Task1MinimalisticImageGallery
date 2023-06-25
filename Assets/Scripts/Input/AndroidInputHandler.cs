using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidInputHandler : MonoBehaviour
{
    [SerializeField] TargetSceneComponent targetSceneComponent;

    Vector2 startTouchPosition;
    Vector2 currentTouchPosition;
    Vector2 endTouchPosition;


    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                // Insert Code Here (I.E. Load Scene, Etc)
                // OR Application.Quit();
                SoundManager.instance.Play("Whoosh");
                OrientationLocker.ForcePortraitOrientation();
                targetSceneComponent.ChangeScene();
                return;
            }
        }

        DetectTouches();
    }

    void DetectTouches()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                currentTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    private void DetectSwipe()
    {
        // You can tweak this distance to suit your game
        float minimumSwipeDistance = 200f;

        if (endTouchPosition.x - startTouchPosition.x > minimumSwipeDistance)
        {
            // If the swipe distance is greater than minimumSwipeDistance
            // we have a swipe. You can add more complex swipe detection here if you need.
            OnSwipe();
        }
    }

    private void OnSwipe()
    {
        // This is where you can add your functionality for when a swipe is detected.
        SoundManager.instance.Play("Whoosh");
        targetSceneComponent.ChangeScene();
    }
}
