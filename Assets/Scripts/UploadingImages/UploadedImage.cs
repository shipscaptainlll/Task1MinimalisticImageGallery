using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UploadedImage : MonoBehaviour
{
    [SerializeField] Transform text;
    [SerializeField] Button button;

    int imageIndex;
    bool uploaded;
    
    public int ImageIndex { get { return imageIndex; } set { imageIndex = value; } }
    public bool Uploaded { get { return uploaded; } set { uploaded = value; } }

    public void EnableLoaded()
    {
        uploaded = true;
    }

    public void EnableImage()
    {
        button.enabled = true;
        text.gameObject.SetActive(false);
    }
}
