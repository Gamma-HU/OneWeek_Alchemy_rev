using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectViewContentGenerator : MonoBehaviour
{
    [SerializeField] private GameObject StageContent;
    [SerializeField] private GameObject GameManager;

    public List<DungeonData> unlockedDungeon = new List<DungeonData>();//解放されている未クリアのダンジョン

    private Sprite background;
    private string dungeonName;
    private int difficulty;
    private string dungeonInfo;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < unlockedDungeon.Count; i++)
        {
            GenerateContent(unlockedDungeon[i]);
        }
    }

    private void GenerateContent(DungeonData dungeonData)
    {
        GameObject content = Instantiate(StageContent, transform.position, Quaternion.identity);
        content.transform.SetParent(this.transform, false);

        background = dungeonData.background;
        dungeonName = dungeonData.dungeonName;
        difficulty = dungeonData.difficulty;
        dungeonInfo = dungeonData.dungeonInfo;

        content.GetComponent<SetContentElement>().setElement(background, dungeonName, difficulty, dungeonInfo);
    }
}
