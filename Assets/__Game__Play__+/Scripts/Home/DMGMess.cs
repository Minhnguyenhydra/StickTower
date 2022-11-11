using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DMGMess : MonoBehaviour
{
    public Text textDmg;
    public Animation animation;
    int damage = 0;

    internal void SetDamage(int v)
    {
        if (v > 0 && damage != v)
        {
            AnimAction();
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(IELerp(damage, v));
            }
        }

            damage = v;
    }

    public void AnimAction()
    {
        animation?.Play();
    }

    private IEnumerator IELerp(int from, int to)
    {
        float time = 0;

        while (time < 0.5f)
        {
            time += Time.deltaTime;

            textDmg.text = Mathf.Lerp(from, to, time/0.5f).ToString("F0");

            yield return null;
        }
    }
}
