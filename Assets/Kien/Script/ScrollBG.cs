using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollBG : MonoBehaviour
{
    public Sprite[] sps;
    public SpriteRenderer[] sp;
    int currentBG;

    public void SetUp()
    {

        currentBG = Random.Range(0, sps.Length);
        for (int i = 0; i < sp.Length; i++)
        {
            sp[i].sprite = sps[currentBG];
        }


        transform.localScale = new Vector3(1, 1, 1);

        var width = sp[0].sprite.bounds.size.x;
        var height = sp[0].sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3((worldScreenWidth / width) * 1.5f, (worldScreenHeight / height) * 1.1f, 1f);
    }

}
