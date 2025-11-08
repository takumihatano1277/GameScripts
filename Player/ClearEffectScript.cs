using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEffectScript : MonoBehaviour
{
    private int clearScene = 2;     //クリアシーン番号
    private void OnParticleSystemStopped()
    {
        FadeManager.FadeOut(clearScene);        //クリアパーティクル終了時にクリアシーンへ移動
    }
}
