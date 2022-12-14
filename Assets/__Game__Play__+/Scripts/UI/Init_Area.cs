using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init_Area : Singleton<Init_Area>
{
    public enum AnimName { attack, idle, hit, die, lose, run, victory, skill_1, skill_2, skill_3, walk }

    public UserData userData;

    public PlayerArena player;
    public EnemyArena enemy;

    private void Awake()
    {
        userData?.OnInitData();
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Ins.OpenUI(UIID.UICArea);
        OnInit();
    }

    public void OnInit()
    {
        StopAllCoroutines();
        player.OnInit();
        enemy.OnInit();
        //Q
        ((CanvasArea)UIManager.Ins.GetUI(UIID.UICArea)).SetResetFight();
    }

    public void Fighting()
    {
        StartCoroutine(IEFighting());
        player.SetAciveDMGMess(false);
    }

    private IEnumerator IEFighting()
    {
        //doan nay cho 2 ben chay vao nhau
        float time = 0;
        float timeToRun = 0.8f;
        //turn 1
        ChangeAnim(AnimName.run.ToString(), true, AnimName.run.ToString(), true);
        SoundManager.Ins.PlayFx(FxID.run_arena);
        while (time < timeToRun)
        {
            time += Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(player.transform.position, player.targetPoint.position, Time.deltaTime * 5f);
            enemy.EnemyGroup.transform.position = Vector3.MoveTowards(enemy.EnemyGroup.transform.position, enemy.targetPoint.position, Time.deltaTime * 5f);
            yield return null; 
        }
        
        SoundManager.Ins.PlayFx(FxID.attack_Arena);
        ChangeAnim(AnimName.attack.ToString(), true, AnimName.attack.ToString(), true);

        yield return new WaitForSeconds(3f);

        if (player.Damage > enemy.Damage)
        {
            ChangeAnim(AnimName.victory.ToString(), true, AnimName.die.ToString(), false);
        }
        else
        {
            ChangeAnim(AnimName.die.ToString(), false, AnimName.victory.ToString(), true);

            yield return new WaitForSeconds(1.5f);
            player.gameObject.SetActive(false);
            //TODO: show ui lose
            UIManager.Ins.OpenUI(UIID.UICFail);
            ((CanvasArea)UIManager.Ins.GetUI(UIID.UICArea)).SetSave_Gold();
            UIManager.Ins.CloseUI(UIID.UICArea);
            //
            yield break;
        }

        yield return new WaitForSeconds(1.5f);

        //turn 2
        enemy.NextStage();

        yield return new WaitForSeconds(1.5f);

        //loop lai cho enemy moi chay vao den khi het

        while (enemy.ContainStage())
        {
            time = 0;

            ChangeAnim(AnimName.idle.ToString(), true, AnimName.run.ToString(), true);
            ChangeAnim(AnimName.idle.ToString(), true, AnimName.walk.ToString(), true);

            SoundManager.Ins.PlayFx(FxID.run_arena);
            while (time < timeToRun)
            {
                time += Time.deltaTime;
                enemy.EnemyGroup.transform.position = Vector3.MoveTowards(enemy.EnemyGroup.transform.position, enemy.targetPoint.position, Time.deltaTime * 5f);
                yield return null;
            }

            ChangeAnim(AnimName.attack.ToString(), true, AnimName.attack.ToString(), true);

            
            SoundManager.Ins.PlayFx(FxID.attack_Arena);
            yield return new WaitForSeconds(3f);

            if (player.Damage > enemy.Damage)
            {
                ChangeAnim(AnimName.victory.ToString(), true, AnimName.die.ToString(), false);

                
                SoundManager.Ins.PlayFx(FxID.giantDeath);
            }
            else
            {
                ChangeAnim(AnimName.die.ToString(), false, AnimName.victory.ToString(), true);

                yield return new WaitForSeconds(1.5f);
                player.gameObject.SetActive(false);
                //TODO: show ui lose
                UIManager.Ins.OpenUI(UIID.UICFail);
                ((CanvasArea)UIManager.Ins.GetUI(UIID.UICArea)).SetSave_Gold();
                UIManager.Ins.CloseUI(UIID.UICArea);

                yield break;
            }

            yield return new WaitForSeconds(1.5f);

        //turn 3
            enemy.NextStage();

            yield return new WaitForSeconds(1.5f);
        }

        {
            ChangeAnim(AnimName.victory.ToString(), true, AnimName.die.ToString(), false);
            //save level
            userData.SetIntData(UserData.Key_LevelArena, ref userData.levelArena, userData.levelArena + 1);
            //TODO: show ui victory
            UIManager.Ins.OpenUI(UIID.UICWin_Level);
            ((CanvasArea)UIManager.Ins.GetUI(UIID.UICArea)).SetSave_Gold();
            UIManager.Ins.CloseUI(UIID.UICArea);
            //
           
        }

        //TODO: test sau xoa di
        //{
        //    yield return new WaitForSeconds(3f);
        //    OnInit();
        //}


    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void ChangeAnim(string heroAnim, bool heroLoop, string enemyAnim, bool enemyLoop)
    {
        player.ChangeAnim(heroAnim, heroLoop);
        enemy.ChangeAnim(enemyAnim, enemyLoop);
    }
}
