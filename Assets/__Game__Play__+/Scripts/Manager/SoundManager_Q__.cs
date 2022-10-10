using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(1)]
public class SoundManager_Q__ : Singleton<SoundManager_Q__>
{
    //public AudioSource audioSourceBG;
    public AudioSource audioSource_Player;
    public AudioSource audioSource_Enemy;
    public AudioSource audio_Loop_Source_BG;
    public AudioSource audio_Loop_Source_BG_arena;

    public AudioClip bgSounds_Menu;
    public AudioClip bgSounds_GamPlay;
    //
    public AudioClip attack;
    public AudioClip Get_Hit_Player;
    //radom 4 sound
    public AudioClip Get_Hit_Enemy;
    public AudioClip Get_Hit_Enemy1;
    public AudioClip Get_Hit_Enemy2;
    public AudioClip Get_Hit_Enemy3;
    //
    public AudioClip Open_Reward;
    public AudioClip btn_Select;
    public AudioClip fireWork;
    public AudioClip yesss_Victory;
    public AudioClip get_Buff;
    public AudioClip win_Canvas;
    public AudioClip lose_Canvas;
    public AudioClip Hit_Reward;//đâm vào rương
    public AudioClip gold_Fly;
    public AudioClip take_1_Key_On_3_Ky_On_Top;
    public AudioClip Boss_WIn;
    public AudioClip Boss_Die;
    public AudioClip Bomm;
    //
    public AudioClip BG_arena;
    public AudioClip arena_run;
    public AudioClip arena_attack;
    private void Start()
    {
        Play_Loop_BG_Menu_Clip();
    }
    
    public void Play_Boss_die()
    {
        audioSource_Player.PlayOneShot(Boss_Die, 5);
    }
    public void Play_arena_attack()
    {
        audioSource_Player.PlayOneShot(arena_attack, 5);
    }
    public void Play_arena_run()
    {
        audioSource_Player.PlayOneShot(arena_run, 5);
    }
    public void Play_take_1_Key_On_3_Ky_On_Top()
    {
        audioSource_Player.PlayOneShot(take_1_Key_On_3_Ky_On_Top, 5);
    }
    public void Play_gold_Fly()
    {
        audioSource_Player.PlayOneShot(gold_Fly, 5);
    }
    public void Play_Hit_Reward()
    {
        audioSource_Player.PlayOneShot(Hit_Reward, 5);
    }
    public void Play_lose_Canvas()
    {
        audioSource_Player.PlayOneShot(lose_Canvas, 5);
    }
    public void Play_win_Canvas()
    {
        audioSource_Player.PlayOneShot(win_Canvas, 5);
    }
    public void Play_get_Buff()
    {
        audioSource_Player.PlayOneShot(get_Buff, 5);
    }
    public void Play_yesss_Victory()
    {
        audioSource_Player.PlayOneShot(yesss_Victory, 5);
    }
    public void Play_fireWork()
    {
        audioSource_Player.PlayOneShot(fireWork, 5);
    }
    public void Play_btn_Select()
    {
        audioSource_Player.PlayOneShot(btn_Select, 5);
    }
    public void Play_Open_Reward()
    {
        audioSource_Player.PlayOneShot(Open_Reward, 5);
    }
    public void Play_Get_Hit_Enemy()
    {
        int ii = Random.Range(1, 5);
        if (ii == 1)
        {
            audioSource_Player.PlayOneShot(Get_Hit_Enemy, 5);

        }
        else if (ii == 2)
        {
            audioSource_Player.PlayOneShot(Get_Hit_Enemy1, 5);

        }
        else if (ii == 3)
        {
            audioSource_Player.PlayOneShot(Get_Hit_Enemy2, 5);

        }
        else if (ii == 4)
        {
            audioSource_Player.PlayOneShot(Get_Hit_Enemy3, 5);

        }
    }
    public void Play_Get_Hit_Player()
    {
        audioSource_Player.PlayOneShot(Get_Hit_Player, 5);
    }
    public void Play_Attack()
    {
        
        audioSource_Player.PlayOneShot(attack, 5);
    }
    public void Play_Loop_BG_Arena()
    {
        audio_Loop_Source_BG_arena.clip = BG_arena;
        audio_Loop_Source_BG_arena.loop = true;
        audio_Loop_Source_BG_arena.Play();
    }
    public void Play_Loop_BG_Menu_Clip()
    {
        audio_Loop_Source_BG.clip = bgSounds_Menu;
        audio_Loop_Source_BG.loop = true;
        audio_Loop_Source_BG.Play();
    }
    public void Play_Loop_BG_GamePlay_Clip()
    {
        audio_Loop_Source_BG.clip = bgSounds_GamPlay;
        audio_Loop_Source_BG.loop = true;
        audio_Loop_Source_BG.Play();
    }
   
    
}
