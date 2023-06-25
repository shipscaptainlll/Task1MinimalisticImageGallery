using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChanger
{
    public enum SceneType { menuScene, loadingBarScene, galleryScene, viewScene }

    static SceneType currentScene;

    public static SceneType CurrentScene { get { return currentScene; }  set { currentScene = value; OrientationLocker.UpdateOrientationLock(); }  }

    public static SceneType TargetScene { get; set; }


    // Start is called before the first frame update
    public static void ChangeScene(TargetSceneComponent targetSceneComponent)
    {
        SceneType targetType = (SceneType) targetSceneComponent.TargetScene;

        if (currentScene == SceneType.menuScene && targetType == SceneType.galleryScene
            || currentScene == SceneType.galleryScene && targetType == SceneType.viewScene)
        {
            currentScene = (SceneType)SceneType.loadingBarScene;
            TargetScene = (SceneType)targetSceneComponent.TargetScene;
            SceneManager.LoadScene((int)SceneType.loadingBarScene);
            
        } else
        {
            currentScene = (SceneType)targetSceneComponent.TargetScene;
            SceneManager.LoadScene((int)targetSceneComponent.TargetScene);
            

        }

        Debug.Log("current scene " + currentScene);
        Debug.Log("target scene " + TargetScene);
        OrientationLocker.UpdateOrientationLock();
    }

    
}
