using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewSceneBehaviior : MonoBehaviour
{
    [SerializeField] CanvasGroup galeryCanvasGroup;
    [SerializeField] UIOrientationHandler uIOrientationHandler;

    // Start is called before the first frame update
    void Awake()
    {
        galeryCanvasGroup.alpha = 0;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSceneUnloaded(Scene current)
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        if (current.name == "ProgressBarScene" && galeryCanvasGroup.alpha != 1)
        {
            galeryCanvasGroup.alpha = 1;
        } else if (current.name == "ViewScene" && galeryCanvasGroup.alpha != 1)
        {
            galeryCanvasGroup.alpha = 1;
        }
        uIOrientationHandler.UpdateOrientation();
        Debug.Log("OnSceneUnloaded: " + current.name);
    }

}
