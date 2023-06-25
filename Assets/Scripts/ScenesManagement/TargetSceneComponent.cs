using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSceneComponent : MonoBehaviour
{
    [SerializeField] SceneType targetScene;
    public enum SceneType { menuScene, loadingBarScene, galleryScene, viewScene }

    public SceneType TargetScene { get { return targetScene; } }

    public void ChangeScene()
    {
        SceneChanger.ChangeScene(this);
    }
}
