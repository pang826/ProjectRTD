using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("BGM")]
    [SerializeField] private AudioClip[] bgmClips;
    private AudioSource[] bgmSources;
    public float BgmVolume;
    [SerializeField] private int bgmChannels;

    [Header("SFX")]
    [SerializeField] private AudioClip[] sfxClips;
    private AudioSource[] sfxSources;
    public float SfxVolume;
    [SerializeField] private int sfxChannels;

    [HideInInspector] public bool IsFirstPlay = true;

    public enum E_SFX
    {
        Archer, Magic, Canon, Explosion
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
        bgmSources[0].Play();
    }

    private void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmSources = new AudioSource[bgmChannels];
        for(int i = 0; i < bgmSources.Length; i++) 
        {
            bgmSources[i] = bgmObject.AddComponent<AudioSource>();
            bgmSources[i].loop = true;
            bgmSources[i].volume = BgmVolume;
            bgmSources[i].clip = bgmClips[i];
        }

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxSources = new AudioSource[sfxChannels];
        for(int i = 0; i < sfxSources.Length; i++)
        {
            sfxSources[i] = sfxObject.AddComponent<AudioSource>();
            sfxSources[i].loop = false;
            sfxSources[i].volume = SfxVolume;
            sfxSources[i].clip = sfxClips[i];
        }
    }

    public void StartStageBGM()
    {
        bgmSources[0].Stop();
        bgmSources[1].Play();
    }

    public void StartMainBGM()
    {
        bgmSources[1].Stop();

        if (IsFirstPlay == false)
            bgmSources[0].Play();
    }

    public void StartSFX(E_SFX sfx)
    {
        sfxSources[(int)sfx].Play();
    }
}
