using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    [SerializeField] AudioSource myAudioSource;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }
        myAudioSource = GetComponent<AudioSource>();
        SetVolume();
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetVolume()
    {
        myAudioSource.volume = PlayerPrefsManager.MusicVolume;
    }

    public void ChangeVolume(float value)
    {
        myAudioSource.volume = value;
        PlayerPrefsManager.MusicVolume = value;
    }
}
