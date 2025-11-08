using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerGaugeScript : MonoBehaviour
{
    [SerializeField] private Image GreenGauge;      //体力ゲージ
    [SerializeField] private Image RedGauge;        //赤ゲージ

    [SerializeField] private GameObject candle;

    private Player player;
    private Tween redGaugeTween;

    private void Start()
    {
        player = candle.GetComponent<Player>();
    }
    /// <summary>
    /// 体力ゲージが減った後ゆっくり赤ゲージも減っていく
    /// </summary>
    /// <param name="reducationValue"></param>
    /// <param name="time"></param>
    public void GaugeReduction(float reducationValue, float time = 1f)
    {
        var valueFrom = player.life / player.maxLife;
        var valueTo = (player.life - reducationValue) / player.maxLife;

        // 体力ゲージ減少
        GreenGauge.fillAmount = valueTo;

        if (redGaugeTween != null)
        {
            redGaugeTween.Kill();
        }

        // 赤ゲージ減少
        redGaugeTween = DOTween.To(
            () => valueFrom,
            x => {
                RedGauge.fillAmount = x;
            },
            valueTo,
            time
        );
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }
}
