using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamon_Sound : MonoBehaviour
{
    public AudioSource fxSource;
    
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
            fxSource.PlayOneShot(SoundManager.Ins.Get_AudioClip(ID));

            //Debug.Log(ID);
        }
    }
}
