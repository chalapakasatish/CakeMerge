using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public TextMeshProUGUI soundText,musicText;
    //public Sprite musicOff, musicOn,soundOff,soundOn;
    //public GameObject MusicImage,soundImage,musicBlueImage,musicRedImage, soundBlueImage, soundRedImage;
    [Serializable]
    public struct SoundSource
    {
        public string sourceName;
        public AudioClip clip;
    }
    
    public List<SoundSource> soundSource = new List<SoundSource>();
    public List<AudioClip> musicSourceClips;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {

        if (PlayerPrefs.GetInt("Music") == 1)
        {
            musicText.text = "OFF";
            //musicBlueImage.SetActive(false);
            //musicRedImage.SetActive(true);
            musicSource.volume = 0;
        }
        else
        {
            musicText.text = "ON";
            //musicBlueImage.SetActive(true);
            //musicRedImage.SetActive(false);
            musicSource.volume = 1;
        }

        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            soundText.text = "OFF";
            //soundBlueImage.SetActive(false);
            //soundRedImage.SetActive(true);
            sfxSource.volume = 0;
        }
        else
        {
            soundText.text = "ON";
            //soundBlueImage.SetActive(true);
            //soundRedImage.SetActive(false);
            sfxSource.volume = 1;
        }

        musicSource.clip = musicSourceClips[UnityEngine.Random.Range(0, musicSourceClips.Count)];
        musicSource.Play();
    }
    public void PlayMusic(AudioClip audioClip)
    {
        musicSource.clip = audioClip;
        musicSource.Play();
    }


    public void PlaySFX(AudioClip audioClip)
    {
        sfxSource.clip = audioClip;
        sfxSource.PlayOneShot(audioClip);
    }

    AudioClip mClip;
    public AudioClip GetAudioClip(string audioName)
    {

        for(int i = 0; i < soundSource.Count; i++)
        {
            if(audioName == soundSource[i].sourceName)
            {
                mClip = soundSource[i].clip;
            }
        }

        return mClip;
    }
    public void MusicButton()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            musicText.text = "ON";
            //musicBlueImage.SetActive(true);
            //musicRedImage.SetActive(false);
            musicSource.volume = 1;
            PlayerPrefs.SetInt("Music", 0);
        }
        else
        {
            musicText.text = "OFF";
            //musicBlueImage.SetActive(false);
            //musicRedImage.SetActive(true);
            musicSource.volume = 0;
            PlayerPrefs.SetInt("Music", 1);
        }
    }
    public void SoundButton()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            soundText.text = "ON";
            //soundBlueImage.SetActive(true);
            //soundRedImage.SetActive(false);
            sfxSource.volume = 1;
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            soundText.text = "OFF";
            //soundBlueImage.SetActive(false);
            //soundRedImage.SetActive(true);
            sfxSource.volume = 0;
            PlayerPrefs.SetInt("Sound", 1);
        }
    }
}
