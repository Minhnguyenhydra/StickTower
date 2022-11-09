using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum SoundID
{//kéo đúng thứ tự ở List Audio tương ứng với List Enum này
    arenaMusic, menu, gamePlay
}

public enum FxID
{
    //kéo đúng thứ tự ở List Audio tương ứng với List Enum này
    attack_Arena,
    attack_GamePlay,
    BattleSound,
    character_slash,
    click,
    collect_coin,
    diamondCollect,
    die_Player,
    fireWork,
    giantDeath,
    hit_Enemy_1,
    hit_Enemy_2,
    hit_Enemy_3,
    hit_Enemy_4,
    hit_Player,
    level_up,
    lose,
    run_arena,
    treasure_open,
    win,
    yes,
    sfx_char_skill_1,
    sfx_char_skill_2,
    sfx_char_skill_3,
    sfx_char_skill_4,
    sfx_char_skill_5,
    sfx_char_skill_6,
    sfx_char_skill_7,
    sfx_char_skill_8,
    sfx_char_skill_9,
    sfx_char_skill_10,
    sfx_char_skill_11,

}


public class SoundManager : Singleton<SoundManager>
{
    private static SoundManager Instance;

    [SerializeField] UserData userData;

    private AudioSource soundSource;
    private AudioSource fxSource;

    [SerializeField] private AudioClip[] soundAus;
    [SerializeField] private AudioClip[] fxAus;

    private bool isLoaded = false;
    private int indexSound;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        soundSource = gameObject.AddComponent<AudioSource>();
        soundSource.loop = true;
        fxSource = gameObject.AddComponent<AudioSource>();
        fxSource.loop = false;
    }

    private void Start()
    {
        StartCoroutine(IELoad());
    }
    //Quan ADD:
    public bool Get_isLoaded()
    {
        return isLoaded;
    }
    public AudioClip Get_AudioClip(FxID ID)
    {
        //Debug.Log((int)ID);
        return fxAus[(int)ID];
    }
    //
    private IEnumerator IELoad()
    {
        if (soundAus.Length > 0)
        {
            yield return Cache.GetWFS(1f);
            isLoaded = true;

            //indexSound = Random.Range(0, soundAus.Length);
            //PlaySound((SoundID)indexSound);
            //PlaySound(SoundID.menu);
            //Debug.Log((SoundID)indexSound);
        }
    }


    public void PlaySound(SoundID ID)
    {
        soundSource.clip = soundAus[(int)ID];

        if (userData.musicIsOn && isLoaded)
        {
            soundSource.Play();
            //Debug.Log(ID);
        }
    }

    public void NextSound()
    {
        indexSound = indexSound >= soundAus.Length - 1 ? 0 : indexSound + 1;
        PlaySound((SoundID)indexSound);
    }

    public void PlayMusic(bool play)
    {
        if (play)
        {
            soundSource.Play();
        }
        else
        {
            soundSource.Stop();
        }
    }


    public void PlayFx(FxID ID)
    {
        if (userData.fxIsOn && isLoaded)
        {
            fxSource.PlayOneShot(fxAus[(int)ID]);

            //Debug.Log(ID);
        }
    }

    public void PlayFxWithName(string soundName)
    {
        if (userData.fxIsOn && isLoaded)
        {
            for (int i = 0; i < fxAus.Length; i++)
            {
                if (fxAus[i].name.Equals(soundName))
                {
                    fxSource.PlayOneShot(fxAus[i]);
                    return;
                }
            }

        }
    }

    public void Play_FX_Hit_Enemy_Random()
    {
        int ii = Random.Range(1, 5);
        if (ii == 1)
        {
            PlayFx(FxID.hit_Enemy_1);
        }
        else if (ii == 2)
        {
            PlayFx(FxID.hit_Enemy_2);
        }
        else if (ii == 3)
        {
            PlayFx(FxID.hit_Enemy_3);
        }
        else if (ii == 4)
        {
            PlayFx(FxID.hit_Enemy_4);
        }
    }
    public void ChangeSound(SoundID ID, float time)
    {
        if (userData.musicIsOn && isLoaded)
        {
            float spacetime = time / 2;

            ChangeVol(.1f, spacetime,
                () =>
                {
                    PlaySound(ID);
                    ChangeVol(1, spacetime, null);
                });
        }
    }

    public void ChangeVol(float vol, float time, UnityAction callBack)
    {
        StartCoroutine(ChangeVolume(vol, time, callBack));
    }

    private IEnumerator ChangeVolume(float vol, float time, UnityAction callBack)
    {
        float stepVol = (vol - soundSource.volume) / 10;
        float stepTime = time / 10;

        for (int i = 0; i < 10; i++)
        {
            soundSource.volume += stepVol;
            yield return Cache.GetWFS(stepTime);
        }

        callBack?.Invoke();
    }


}
/*
public enum FxID
{

    CoinDrop,
    Failed,
    MeleeAtk,
    MeleeDeath_1,
    MeleeDeath_2,
    PopupNewCharacter,
    Popup_NewCharacter,
    RangeAtk,
    RangeDeath_1,
    RangeDeath_2,
    PopupSpinEnd,
    PopupSpinTick,
    Click,
    Victory,

} 
 */