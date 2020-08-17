using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashPanel : MonoBehaviour {

    [SerializeField] Image myImage;
    [SerializeField] float fadeTime;
    [SerializeField] float pauseTime;
    [SerializeField] float opacityDelta;
    [SerializeField] float additiveOpacity;

    private Coroutine myCoroutine;

    private bool coroutineRunning = false;

    private void Awake()
    {
        var audio = GetComponent<AudioSource>();
        if (audio)
            audio.volume = PlayerPrefsManager.SoundVolume;
    }

    public void Flash()
    {

        if (!coroutineRunning)
        {
            coroutineRunning = true;
        }
        else
        {
            StopCoroutine(myCoroutine);
            ChangeOpacity(additiveOpacity);
        }

        myCoroutine = StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        while (myImage.color.a < opacityDelta)
        {
            ChangeOpacity((opacityDelta * Time.deltaTime) / fadeTime);
            yield return null;
        }
        yield return new WaitForSeconds(pauseTime);
        while (myImage.color.a > 0)
        {
            ChangeOpacity(-(opacityDelta * Time.deltaTime) / fadeTime);
            yield return null;
        }
        coroutineRunning = false;
        yield return null;
    }

    private void ChangeOpacity(float opacityThisFrame)
    {
        Color opacityChange = new Color(0, 0, 0, opacityThisFrame);
        myImage.color += opacityChange;
    }
}
