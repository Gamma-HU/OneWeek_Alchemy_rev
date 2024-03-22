using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpeditionManager : MonoBehaviour
{
    [SerializeField]//test
    DungeonData currentDungeon;
    [SerializeField]//test
    Character player;//test
    BattleManager battleManager;
    [SerializeField]
    float battleInterval;

    int layer;
    void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();
        player.Init(battleManager);
        battleManager.StartBattle(currentDungeon.enemies[0]);//test
        Debug.Log("探索開始");
    }

    public void StartExpedition(DungeonData dungeon)
    {
        currentDungeon = dungeon;
    }
    public void NextLayer()
    {
        layer++;
        if (currentDungeon.enemies.Count == layer)
        {
            Debug.Log("ダンジョンクリア");
        }
        else { StartCoroutine(BattleInterval()); }
    }
    IEnumerator BattleInterval()
    {
        yield return new WaitForSeconds(battleInterval);
        battleManager.StartBattle(currentDungeon.enemies[layer]);
    }
}
