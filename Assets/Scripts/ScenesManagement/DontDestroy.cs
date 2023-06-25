using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] GameObject galeryCanvas;
    [SerializeField] CanvasGroup galeryCanvasGroup;
    public static DontDestroy instance;
    ImageLoader imageLoader;
    // Start is called before the first frame update
    void Awake()
    {
        
        if (instance != null)
        {
            DestroyImmediate(gameObject);
        } else
        {
            instance = this;
        }
        
    }

    void Start()
    {
        imageLoader = transform.GetChild(0).GetComponent<ImageLoader>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        galeryCanvasGroup.alpha = 0;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GalleryScene")
        {
            Debug.Log("entered gallery scene");
            galeryCanvas.SetActive(true);
            imageLoader.RestartUploading();
        } else
        {
            if (galeryCanvas.activeSelf)
            {
                imageLoader.StopAllCoroutines();
                galeryCanvas.SetActive(false);
            }
            
        }
        Debug.Log("just loaded a new scene");
    }

    private void OnSceneUnloaded(Scene current)
    {
        if (current.name == "ProgressBarScene" && galeryCanvasGroup.alpha != 1)
        {
            galeryCanvasGroup.alpha = 1;
        }
        Debug.Log("OnSceneUnloaded: " + current);
    }


}
