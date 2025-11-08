using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITorcable
{
    /// <summary>
    /// 松明に火を灯したときに呼び出し
    /// </summary>
    void TorchFire();
}