using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EfxManager : SingletonMonoBehaviour<EfxManager>
{
    public Image gold;
    public Image gem;
    public Transform efxHolder;
    public RectTransform rect_Start;
    public RectTransform rect_Gold;
    public RectTransform rect_Gem;

    public RectTransform rect_Bank_start;
    public RectTransform rect_Bank_end;
    public RectTransform rect_Piggy;




    [ContextMenu("Set_GoldTop_FX")]
    public void Set_GoldTop_FX()
    {
        GetGoldFx(rect_Start.position, rect_Gold.position);
    }
    [ContextMenu("Set_GemTop_FX")]
    public void Set_GemTop_FX()
    {
        GetGemFx(rect_Start.position, rect_Gem.position);
    }
    [ContextMenu("Set_Piggy_FX")]
    public void Set_Piggy_FX()
    {
        GetGoldFx(rect_Start.position, rect_Piggy.position);
    }
    IEnumerator IE_Set_GoldTop_FX()
    {
        yield return Cache.GetWFS(1);
        GetGoldFx(rect_Start.position, rect_Gold.position);

    }
    IEnumerator IE_Set_GemTop_FX()
    {

        yield return Cache.GetWFS(1);
        GetGemFx(rect_Start.position, rect_Gem.position);
    }
    IEnumerator IE_Set_Piggy_FX()
    {

        yield return Cache.GetWFS(1);
        GetGoldFx(rect_Start.position, rect_Piggy.position);
    }

    public void Set_Step_By_Step_Inscrease(int _score, int target,Text txt_Inscrease, float transitionTime)
    {
        Tween t = DOTween.To(() => _score, x => _score = x, target, transitionTime).OnUpdate(() => txt_Inscrease.text = _score.ToString("N0"));
    }
    public void GetGoldFx(Vector3 startPos, Vector3 endPos)
    {
        for (int i = 0; i < Random.Range(15, 25); i++)
        {

            SoundManager.Ins.PlayFx(FxID.collect_coin);
            //float randomX = Random.Range(-300f, 300f);
            //float randomY = Random.Range(-100f, 100f);
            float randomX = Random.Range(-3, 3);
            float randomY = Random.Range(-3, 3);
            //Vector3 firstDesPos = new Vector3(startPos.x + randomX, startPos.y + randomY, 0);
            var tempImg = SimplePool.Spawn(gold.gameObject, startPos, Quaternion.identity).GetComponent<RectTransform>();
            tempImg.transform.SetParent(efxHolder);
            tempImg.position = startPos;
            tempImg.transform.localScale = Vector3.one * 0.65f;
            Vector3 firstDesPos = new Vector3(tempImg.position.x + randomX, tempImg.position.y + randomY, 0);
            tempImg.transform.DOMove(firstDesPos, Random.Range(0.3f, 0.8f)).SetEase(Ease.InQuad).OnComplete(() => {
                tempImg.transform.DOMove(new Vector3(endPos.x, endPos.y, 0), Random.Range(0.8f, 1.2f)).SetEase(Ease.InQuad).OnComplete(() => {
                    //++++++++SoundController.PlaySoundOneShot(SoundController.ins.gem_collect);
                    SoundManager.Ins.PlayFx(FxID.collect_coin);
                    SimplePool.Despawn(tempImg.gameObject);
                });
            });
        }
    }
    public void Set_Gold_Fly_Pig_OK()
    {
        StartCoroutine(IE_Set_Gold_Fly_Pig_OK());
    }
    IEnumerator IE_Set_Gold_Fly_Pig_OK()
    {
        yield return Cache.GetWFS(3);
        Set_Gold_Fly_Pig(rect_Bank_start.position, rect_Bank_end.position);
        yield return Cache.GetWFS(1);
        Set_Gold_Fly_Pig(rect_Bank_start.position, rect_Bank_end.position);
        yield return Cache.GetWFS(1);
        Set_Gold_Fly_Pig(rect_Bank_start.position, rect_Bank_end.position);
        yield return Cache.GetWFS(1);
        Set_Gold_Fly_Pig(rect_Bank_start.position, rect_Bank_end.position);
        yield return Cache.GetWFS(1);
        Set_Gold_Fly_Pig(rect_Bank_start.position, rect_Bank_end.position);
    }
    public void Set_Gold_Fly_Pig(Vector3 startPos, Vector3 endPos)
    {
        var tempImg = SimplePool.Spawn(gold.gameObject, startPos, Quaternion.identity).GetComponent<RectTransform>();
        tempImg.transform.SetParent(efxHolder);
        tempImg.position = startPos;
        tempImg.transform.localScale = Vector3.one * 0.95f;


        tempImg.transform.DOMove(endPos, 1f);
        tempImg.transform.DOScale(0, 1f).OnComplete(() => {
            //++++++++SoundController.PlaySoundOneShot(SoundController.ins.gem_collect);
            SoundManager.Ins.PlayFx(FxID.collect_coin);
            SimplePool.Despawn(tempImg.gameObject);
        }); ;
    }
    public void GetGemFx(Vector3 startPos, Vector3 endPos)
    {
        for (int i = 0; i < Random.Range(15, 25); i++)
        {
            //float randomX = Random.Range(-300f, 300f);
            //float randomY = Random.Range(-100f, 100f);
            float randomX = Random.Range(-3, 3);
            float randomY = Random.Range(-3, 3);
            //Vector3 firstDesPos = new Vector3(startPos.x + randomX, startPos.y + randomY, 0);
            var tempImg = SimplePool.Spawn(gem.gameObject, startPos, Quaternion.identity).GetComponent<RectTransform>();
            tempImg.transform.SetParent(efxHolder);
            tempImg.position = startPos;
            tempImg.transform.localScale = Vector3.one * 0.65f;
            Vector3 firstDesPos = new Vector3(tempImg.position.x + randomX, tempImg.position.y + randomY, 0);
            tempImg.transform.DOMove(firstDesPos, Random.Range(0.3f, 0.8f)).SetEase(Ease.InQuad).OnComplete(() => {
                tempImg.transform.DOMove(new Vector3(endPos.x, endPos.y, 0), Random.Range(0.8f, 1.2f)).SetEase(Ease.InQuad).OnComplete(() => {
                    //++++++++SoundController.PlaySoundOneShot(SoundController.ins.gem_collect);
                    SoundManager.Ins.PlayFx(FxID.diamondCollect);
                    SimplePool.Despawn(tempImg.gameObject);
                });
            });
        }
    }

    public void GetGemFx_2(Vector3 startPos, Vector3 endPos)
    {
        for (int i = 0; i < Random.Range(5, 10); i++)
        {
            var ran = Random.insideUnitCircle * Random.Range(200, 300);
            //Vector3 firstDesPos = new Vector3(startPos.x + randomX, startPos.y + randomY, 0);
            var tempImg = SimplePool.Spawn(gold.gameObject, startPos, Quaternion.identity).GetComponent<RectTransform>();
            tempImg.transform.SetParent(efxHolder);
            tempImg.position = startPos;
            tempImg.transform.localScale = Vector3.one * 0.35f;
            Vector3 firstDesPos = new Vector3(tempImg.position.x + ran.x, tempImg.position.y + ran.y, 0);
            tempImg.transform.DOMove(firstDesPos, Random.Range(0.3f, 0.8f)).SetEase(Ease.InQuad).OnComplete(() => {
                tempImg.transform.DOMove(new Vector3(endPos.x, endPos.y, 0), Random.Range(1f, 1.5f)).SetEase(Ease.InQuad).OnComplete(() => {
                    //++++++SoundController.PlaySoundOneShot(SoundController.ins.gem_collect);
                    SimplePool.Despawn(tempImg.gameObject);
                });
            });
        }
    }

    public Transform startPos;
    public Transform endPos;
    [ContextMenu("test")]
    public void Play()
    {
        GetGemFx(startPos.position, endPos.position);
    }
    [ContextMenu("test_2")]
    public void Play2()
    {
        GetGemFx_2(startPos.position, endPos.position);
    }
}
