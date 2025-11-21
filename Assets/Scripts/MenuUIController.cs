using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [Header("MenuRef")]
    public Scrollbar Audio;
    public AudioMixer AudioMixer;
    public Scrollbar Brightness;
    public Image BrightnessImage;
    public Scrollbar Sensibility;
    public Canvas DDOLcanvas;

    private float lastAudio, lastBrightness, lastSensibility;

    public static MenuUIController Istance;
    // Start is called before the first frame update
    void Awake()
    {
        if (Istance == null)
        {
            Istance = this;
            DontDestroyOnLoad(gameObject);
            DDOLcanvas.transform.SetParent(transform);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        AudioMixer = Resources.Load<AudioMixer>("MasterVolume");
    }
    void Start()
    {
        GetData();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSens();
        ChangeBrightness();
        ChangeAudio();
        if (Audio.value != lastAudio || Brightness.value != lastBrightness || Sensibility.value != lastSensibility)
        {
            SaveData();
            lastAudio = Audio.value;
            lastBrightness = Brightness.value;
            lastSensibility = Sensibility.value;
        }
    }
    private void ChangeSens()
    {
        float MaxMouseSens = 2000f;
        float MinMouseSens = 1000f;
        playerController.mouseSensitivity = Mathf.Lerp(MinMouseSens, MaxMouseSens, Sensibility.value);
    }
    private void ChangeBrightness()
    {
        Color c = BrightnessImage.color;
        c.a = MathF.Min(Brightness.value, 0.9f);
        BrightnessImage.color = c;
    }

    private void ChangeAudio()
    {
        float volume = Mathf.Lerp(-80f, 0f, Audio.value);
        AudioMixer.SetFloat("MasterVolume", volume);
    }
    public void SaveData()
    {
        PlayerPrefs.SetFloat("Audio", Audio.value);
        PlayerPrefs.SetFloat("Brightness", Brightness.value);
        PlayerPrefs.SetFloat("Sensibility", Sensibility.value);
        PlayerPrefs.Save();
    }
    public void GetData()
    {
        Audio.value = PlayerPrefs.GetFloat("Audio", 0.05f);
        Brightness.value = PlayerPrefs.GetFloat("Brightness", 0.05f);
        Sensibility.value = PlayerPrefs.GetFloat("Sensibility", 0.05f);
    }
}