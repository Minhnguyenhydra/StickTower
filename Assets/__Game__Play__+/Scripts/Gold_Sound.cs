using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold_Sound : MonoBehaviour
{
    public AudioSource fxSource;
    public AudioClip au;
    private void Awake()
    {
        //fxSource = gameObject.AddComponent<AudioSource>();
        //fxSource.loop = false;
    }
    private void OnEnable()
    {
        PlayFx(FxID.collect_coin);
    }
    public void PlayFx(FxID ID)
    {
        if (InitUserData_Gamplay.Ins.userData.fxIsOn && SoundManager.Ins.Get_isLoaded())
        {
            fxSource.PlayOneShot(au);

            Debug.Log(SoundManager.Ins.Get_AudioClip(ID));
        }
    }
}
