using System.Collections;
using UnityEngine;
public interface IWateable
{
    /// <summary>
    /// 水に触れた時に呼び出し
    /// </summary>
    void WaterHit();
    /// <summary>
    /// 水に蝋が当たった時に爆発する関数呼び出し
    /// </summary>
    void WaterExplosion();
}