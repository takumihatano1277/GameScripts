using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スコアの％表示　クリア後は隠し要素も含める
/// </summary>
public class AchievementScript : MonoBehaviour
{
    [SerializeField] private int stage;
    private float score;
    private TextFx.TextFxTextMeshProUGUI stageAchievement;
    private float before_Clearing = 33.3f;
    private float after_Clearing = 25.0f;
    // Start is called before the first frame update
    void Start()
    {
        stageAchievement = GetComponent<TextFx.TextFxTextMeshProUGUI>();
        score = 0;
        if(SaveManager.sd.clearStage<7)
        {
            if (SaveManager.sd.stageData[stage * 3])
            {
                score += before_Clearing;
            }
            if (SaveManager.sd.stageData[stage * 3 - 1])
            {
                score += before_Clearing;
            }
            if (SaveManager.sd.stageData[stage * 3 - 2])
            {
                score += before_Clearing;
            }
            stageAchievement.text = Mathf.Ceil(score) + "%";
        }
        else
        {
            if (SaveManager.sd.stageData[stage * 3])
            {
                score += after_Clearing;
            }
            if (SaveManager.sd.stageData[stage * 3 - 1])
            {
                score += after_Clearing;
            }
            if (SaveManager.sd.stageData[stage * 3 - 2])
            {
                score += after_Clearing;
            }
            if (SaveManager.sd.stageStar[stage - 4])
            {
                score += after_Clearing;
            }
            stageAchievement.text = Mathf.Ceil(score) + "%";
        }
    }
}
