using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    DungeonData firstDangeon;//���̃_���W�����͂��̃f�[�^���Ɋi�[
    [SerializeField]//test
    List<DungeonData> unlockedDungeon = new List<DungeonData>();//�������Ă��関�N���A�̃_���W����
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<DungeonData> GetUnlockedDungeon() { return unlockedDungeon; }
}
