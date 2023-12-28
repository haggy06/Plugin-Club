using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : Singleton<Audio>
{
    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume = 0.2f;
    AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClips = new AudioClip[20];
    public float sfxVolume = 0.5f;
    public int channels = 8;
    AudioSource[] sfxPlayers;
    int channelIndex;

    //효과음 목록
    public enum Sfx {Start,Jump1,Shoot1, W_Ground, ATK1, Crack, Portal}


    public void InputAudioClip(Sfx sfx)
    {
        sfxClips[(int)sfx] = Resources.Load<AudioClip>(sfx.ToString());
    }
    private new void Awake()
    {
        base.Awake();

        bgmClip = Resources.Load<AudioClip>("Title");

        InputAudioClip(Sfx.Start);
        InputAudioClip(Sfx.Jump1);
        InputAudioClip(Sfx.Shoot1);
        InputAudioClip(Sfx.W_Ground);
        InputAudioClip(Sfx.ATK1);
        InputAudioClip(Sfx.Crack);
        InputAudioClip(Sfx.Portal);

        Init();
    }
    void Init()
    {
        //배경음 초기설정
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        //게임 실행시 배경음 재생
        bgmPlayer.playOnAwake = false;
        //루프
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        //효과음 초기설정
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index=0; index <sfxPlayers.Length; index++){
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }

    }
    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++) { 
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
