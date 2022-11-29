using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollBG : MonoBehaviour
{
    public Sprite[] spsNormal,spsArea,spChallenge;
    public SpriteRenderer[] sp;
    int currentBG;

    public void SetUp(int type)
    {
        if (type == 1)
        {
            currentBG = Random.Range(0, spsNormal.Length);
            for (int i = 0; i < sp.Length; i++)
            {
                sp[i].sprite = spsNormal[currentBG];
            }
        }
        else if(type == 2)
        {
            currentBG = Random.Range(0, spsArea.Length);
            for (int i = 0; i < sp.Length; i++)
            {
                sp[i].sprite = spsArea[currentBG];
            }
        }   
        else
        {
            currentBG = Random.Range(0, spChallenge.Length);
            for (int i = 0; i < sp.Length; i++)
            {
                sp[i].sprite = spChallenge[currentBG];
            }
        }    



        transform.localScale = new Vector3(1, 1, 1);

        var width = sp[0].sprite.bounds.size.x;
        var height = sp[0].sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3((worldScreenWidth / width) * 1.1f, (worldScreenHeight / height) * 1.1f, 1f);
    }

}
