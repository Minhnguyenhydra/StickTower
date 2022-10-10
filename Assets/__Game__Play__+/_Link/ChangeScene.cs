using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : Singleton<ChangeScene>
{
    public GameObject animFade;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Fade()
    {
        animFade.SetActive(true);

        Invoke(nameof(Close), 1.5f);
    }

    public void Close()
    {
        animFade.SetActive(false);
    }

}
