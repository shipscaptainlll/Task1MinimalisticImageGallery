using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewedImage : MonoBehaviour
{
    [SerializeField] Image viewedImage;

    // Start is called before the first frame update
    void Awake()
    {
        UpdateImage();
    }

    void UpdateImage()
    {
        viewedImage.sprite = SelectedImage.Image;
    }


}
