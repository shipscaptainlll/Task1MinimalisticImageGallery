using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedImageUpdater : MonoBehaviour
{

    public void UpdateSelectedDate(Image chosenImage)
    {
        SelectedImage.Image = chosenImage.sprite;
    }
}
