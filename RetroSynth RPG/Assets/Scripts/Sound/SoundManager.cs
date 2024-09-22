using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip musicLoop;
    public float bpm = 128;

    private AudioMixerSnapshot mixer;
    private float m_TransitionOut;
    private float m_QuarterNote;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(musicLoop);

        m_TransitionOut = m_QuarterNote * 32;
        m_QuarterNote = 60 / bpm;
    }

    public void FadeOut()
    {
        StartCoroutine(StartFade(audioSource, 2, 0));
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }


}
