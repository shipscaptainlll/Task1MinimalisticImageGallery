using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgresBarBehavior : MonoBehaviour
{
    [SerializeField] Image LoadingBarFill;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneAsync((int) SceneChanger.TargetScene));

        Debug.Log("from progress bar " + " current scene " + SceneChanger.CurrentScene + " target scene " + SceneChanger.TargetScene);
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);

        //operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBarFill.fillAmount = progressValue;

            
            yield return new WaitForEndOfFrame();
        }

        if (SceneChanger.TargetScene == SceneChanger.SceneType.galleryScene)
        {
            yield return new WaitForSeconds(3);
            SceneChanger.CurrentScene = SceneChanger.SceneType.galleryScene;
        } else if (SceneChanger.TargetScene == SceneChanger.SceneType.viewScene)
        {
            yield return new WaitForSeconds(0.5f);
            SceneChanger.CurrentScene = SceneChanger.SceneType.viewScene;
        }
        

        SceneManager.UnloadSceneAsync(1);
    }
}
