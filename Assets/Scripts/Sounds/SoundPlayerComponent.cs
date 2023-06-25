using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerComponent : MonoBehaviour
{
    
    public void PlaySound(string soundName)
    {
        SoundManager.instance.Play(soundName);
    }
}
