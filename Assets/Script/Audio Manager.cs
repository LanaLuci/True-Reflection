using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private GameObject BGMSpeaker, SFXSpeaker;
    private AudioSource bgmSource, sfxSource;
    [SerializeField] private AudioClip[] sfxList;

    void Start()
    {
        instance = this;
        this.bgmSource = BGMSpeaker.GetComponent<AudioSource>();
        this.sfxSource = SFXSpeaker.GetComponent<AudioSource>();

        bgmSource.Play();
        SetBGM(PlayerPrefs.GetFloat("BGMVolume"));
        SetSFX(PlayerPrefs.GetFloat("SFXVolume"));
    }

    public void PlaySFX(int id)
    {
        if (sfxList[id] != null)
        {
            this.sfxSource.PlayOneShot(sfxList[id]);
        }

    }

    public void SetBGM(float value)
    {
        mixer.SetFloat("bgmVolume", value);
    }

    public void SetSFX(float value)
    {
        mixer.SetFloat("SFXVolume", value);
    }
}
