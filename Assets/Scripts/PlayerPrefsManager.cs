using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsManager {

    public static float MusicVolume {
        get { return PlayerPrefs.GetFloat("MusicVolume", 0.5f); }
        set { PlayerPrefs.SetFloat("MusicVolume", value);
        } }
    public static float SoundVolume
    {
        get { return PlayerPrefs.GetFloat("SoundVolume", 0.5f); }
        set
        {
            PlayerPrefs.SetFloat("SoundVolume", value);
        }
    }


}
