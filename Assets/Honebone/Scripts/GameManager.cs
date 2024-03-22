using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    DungeonData firstDangeon;//次のダンジョンはこのデータ内に格納
    [SerializeField]//test
    List<DungeonData> unlockedDungeon = new List<DungeonData>();//解放されている未クリアのダンジョン
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<DungeonData> GetUnlockedDungeon() { return unlockedDungeon; }
}
