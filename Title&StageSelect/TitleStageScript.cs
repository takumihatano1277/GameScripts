using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class TitleStageScript : MonoBehaviour
{
    //int型を変数StageTipSizeで宣言
    private const int StageTipSize =200;
    //int型を変数currentTipIndexで宣言
    private int currentTipIndex;
    //ターゲットキャラクターの指定が出来る様に
    public Transform character;
    //ステージチップの配列
    public GameObject[] stageTips;
    //自動生成する時に使う変数startTipIndex
    public int startTipIndex;
    //ステージ生成の先読み個数
    public int preInstantiate;
    //作ったステージチップの保持リスト
    public List<GameObject> generatedStageList = new List<GameObject>();

    private Rewired.Player player0;


    private void Awake()
    {
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;
        ReInput.ControllerPreDisconnectEvent += OnControllerPreDisconnect;
    }
    void Start()
    {
        player0 = ReInput.players.GetPlayer(0);
        
        player0.controllers.maps.SetMapsEnabled(false, "Game");
        player0.controllers.maps.SetMapsEnabled(true, "Default");
        //初期化処理
        currentTipIndex = startTipIndex - 1;
        UpdateStage(preInstantiate);
    }
    void OnControllerConnected(ControllerStatusChangedEventArgs args)
    {
        LogoSceneScript.controller = true;
    }
    void OnControllerDisconnected(ControllerStatusChangedEventArgs args)
    {
        LogoSceneScript.controller = false;
    }
    void OnControllerPreDisconnect(ControllerStatusChangedEventArgs args)
    {
        LogoSceneScript.controller = false;
    }

    void Update()
    {
        //キャラクターの位置から現在のステージチップのインデックスを計算
        int charaPositionIndex = (int)(character.position.x / StageTipSize);
        //次のステージチップに入ったらステージの更新処理
        if (charaPositionIndex + preInstantiate > currentTipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }

    }
    //指定のインデックスまでのステージチップを生成
    void UpdateStage(int toTipIndex)
    {
        if (toTipIndex <= currentTipIndex) return;
        //指定のステージチップまで生成
        for (int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);
            //生成したステージチップを管理リストに追加
            generatedStageList.Add(stageObject);
        }
        //ステージ保持上限になるまで古いステージを削除
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();

        currentTipIndex = toTipIndex;
    }
    //指定のインデックス位置にstageオブジェクトを生成
    GameObject GenerateStage(int tipIndex)
    {
        int nextStageTip = Random.Range(0, stageTips.Length);

        GameObject stageObject = (GameObject)Instantiate(
            stageTips[nextStageTip],
            new Vector3(tipIndex * StageTipSize, 0, 0),
            Quaternion.identity);
        return stageObject;
    }
    //古いステージを削除
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }

}