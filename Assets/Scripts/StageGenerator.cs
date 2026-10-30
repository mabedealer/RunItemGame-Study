using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    const int StageChipSize = 30;
    int currentChipIndex;

    Transform character; // ①ターゲットキャラクターの指定
    public GameObject[] stageChips; // ②ステージチッププレハブ配列
    public int startChipIndex = 1; // ③自動生成開始インデックス
    public int preInstantiate = 5; // ④生成先読み個数

    // 生成済みステージチップ保持リスト
    public List<GameObject> generatedStageList = new List<GameObject>(); // ⑤生成済みステージチップ保持リスト

    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").transform; // Playerを検索し、そのTransformを取得
        currentChipIndex = startChipIndex - 1; // 現在基準のフロアを-1にする 
        UpdateStage(preInstantiate);
    }

    void Update()
    {
        if (character == null) return;

        // キャラクター位置から現在のステージチップのインデックスを計算
        int charaPositionIndex = (int)(character.position.z / StageChipSize);

        // もし（Playerの現在フロア番号 + 予備）が生成したフロア番号を上回ったら生成
        if (charaPositionIndex + preInstantiate > currentChipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }

    void UpdateStage(int toChipIndex)
    {
        for (int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);
            generatedStageList.Add(stageObject); // 生成したフロア情報を管理リストに記帳
        }

        // 古いフロアの削除
        while (generatedStageList.Count > preInstantiate + 2)
        {
            DestroyOldestStage();
        }

        currentChipIndex = toChipIndex; // 生成したフロア番号の更新
    }

    GameObject GenerateStage(int chipIndex)
    {
        if (stageChips.Length == 0) return null;

        int nextStageChip = Random.Range(0, stageChips.Length);

        GameObject stageObject = (GameObject)Instantiate(
            stageChips[nextStageChip],
            new Vector3(0, 0, chipIndex * StageChipSize),
            Quaternion.identity
        );

        return stageObject;
    }

    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}