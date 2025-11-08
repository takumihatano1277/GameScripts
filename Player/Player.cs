using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JoostenProductions;
using MBLDefine;
using KanKikuchi.AudioManager;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, ITrapable,IWateable
{
    #region 定義

    //プレイヤーステータス
    private Vector3 velocity;              //移動方向
    private float moveSpeed = 20.0f;        //移動速度
    private float applySpeed = 0.2f;       //回転の適用速度
    private float jumpPower = 50.0f;    //ジャンプ威力
    public float life = 60;
    public float maxLife = 60;
    private float speed = 2500f;        //弾のスピードx
    private float upSpeed = 2000f;      //弾のスピードy
    private float timeleft = 1.0f;      //火がついている間何秒ごとにダメージを受けるか
    private float damageTime;


    private float heatDamage = 1.0f;        //燃えている間の秒間ダメージ
    private float shotDamage = 5.0f;        //蝋発射ダメージ
    private float meltDamage = 10.0f;       //溶けたダメージ
    private float revivalDamage = 20.0f;    //中間地点から復活時のダメージ


    //ろうそくの火関連
    private float maxRange = 100.0f;
    private float maxintensity = 50.0f;
    static public bool isFireHeat;        //蝋燭の火がついているかどうか
    static public bool isFireMotion;      //蝋燭の火を飛ばすモーションをしているかどうか


    //bool関連
    public bool isKeep { private get; set; }                  //停止状態
    //static public bool ground;          //地面に触れているかどうか
    private bool isWalking;               //歩いているかどうか
    private bool isLight;                 //ライトがオンかどうか
    private bool isClear;                 //ゲームクリアしたかどうか


    //コンポーネント
    private Rigidbody rb;
    private Animator anim;
    private CapsuleCollider capsule;
    private ParticleSystem particle;                        //パーティクル

    [SerializeField] private InputManager inputManager;     //インプットマネージャーを入れる
    [SerializeField] private Light point;                   //蝋燭の火についているライト
    [SerializeField] private Light directionalLight;        //環境光

    protected PlayerGaugeScript playerGauge;


    //ゲームオブジェクト
    [SerializeField] GameObject bullet;         //飛ばす蝋
    [SerializeField] GameObject bulletPos;      //弾が生成されるポジションを保有するゲームオブジェクト
    [SerializeField] private GameObject candleRoad;     //キャンドルの溶けた道
    [SerializeField] private GameObject fireEffect;         //蝋燭の火のエフェクト
    [SerializeField] private GameObject clearEffect;        //クリア時に出てくるエフェクト
    [SerializeField] private GameObject gauge;      //プレイヤーの体力バー
    private SphereCollider heatCollider;


    [SerializeField] private List<int> animID;

    //Light
    private float fireOnRange = 50.0f;      //蝋燭の火がついている状態の光の範囲
    private float fireOffRange = 25.0f;     //蝋燭の火が消えている状態の光の範囲

    private float fireOnIntensity = 10.0f;  //蝋燭の火がついている状態の光の強さ
    private float fireOffIntensity = 20.0f; //蝋燭の火が消えている状態の光の強さ
    private float directionalIntensity = 0.75f;     //環境光の強さ

    private float lightRangeChangeSpeed;    //蝋燭の光の範囲の切り替え時の速度
    private float lightIntensityChangeSpeed;//蝋燭の光の強さの切り替え時の速度
    private float directionalIntensityChangeSpeed;//環境光の強さの切り替え時の速度

    private float lightRangePower = 25.0f;
    private float lightIntensityPower = 10.0f;
    private float directionalIntensityPower = 0.5f;


    //ステージ関連
    [SerializeField] private List<string> fireList;
    
    [SerializeField] private float half_x = 0;      //ステージのセーブポイントの場所x
    [SerializeField] private float half_y = 0;      //ステージのセーブポイントの場所y


    private int overScene = 3;          //ゲームオーバーのシーンの番号
    
    #endregion
    
    public void Awake()
    {
        inputManager = inputManager.GetComponent<InputManager>();
        Init();     //初期化
    }
    private void Start()
    {
        
    }
    void Update()
    {
        lightRangeChangeSpeed = lightRangePower * Time.deltaTime;        //光の範囲変更速度計算
        lightIntensityChangeSpeed = lightIntensityPower * Time.deltaTime;    //光の強さ変更速度計算
        directionalIntensityChangeSpeed = directionalIntensityPower * Time.deltaTime;    //光の強さ変更速度計算
        pointLight();       //ポイントライト調整
        velocity = Vector3.zero;
        if (life <= 0)          //体力が0になった時
            Death();            //死亡
        if (life >= maxLife)    //最大体力を超えないように調整
            life = maxLife;
        if(isFireHeat)            //蝋燭の火がついているとき
        {
            timeleft -= Time.deltaTime;
            if (timeleft <= 0.0f)        //1秒経っていたら
            {
                timeleft = damageTime;        //初期化
                Damage(heatDamage);              //1ダメージ
            }
        }
        if(!GameManagerScript.IsPaused())
        {
            if(isClear)       //クリア判定なら
            {
                if (point.range < maxRange)                         //光が最大範囲未満なら
                    point.range += lightRangeChangeSpeed;           //光を最大範囲まで上げる
                if (point.range > maxRange)                         //光が最大範囲を超えたなら
                    point.range = maxRange;                         //光を最大範囲に固定
                if (point.intensity < maxintensity)                 //光が最大の強さ未満なら
                    point.intensity += lightIntensityChangeSpeed;   //光を最大の強さまで上げる
                if (point.intensity > maxintensity)                 //光が最大の強さを超えたなら
                    point.intensity = maxintensity;                 //光を最大の強さに固定
                if (directionalLight.intensity < directionalIntensity)             //環境光の最大の強さ未満なら
                    directionalLight.intensity += directionalIntensityChangeSpeed;            //環境光を最大の強さまで上げる
                if (directionalLight.intensity > directionalIntensity)             //環境光が最大の強さを超えたなら
                    directionalLight.intensity = directionalIntensity;             //環境光の最大の強さに固定
            }
            if (!isKeep)      //固定されていないなら
            {
                PlayerControl();        //移動やモード切替などの関数呼び出し
            }
        }
        
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;    //速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整
        
        if (velocity.magnitude > 0)     //移動している場合
        {
            if (GroundScript.IsGround())     //地面に触れているなら
            {
                if (!isWalking)
                {
                    isWalking = true;
                    SEManager.Instance.Play(SEPath.WALK, 0.2f,0,1,true);     //足音
                }
            }
            else
            {
                isWalking = false;
                SEManager.Instance.Stop(SEPath.WALK);       //足音停止
                
            }
            anim.SetBool("Walk", true);
            // 無回転状態のプレイヤーのZ+方向(後頭部)を、移動の反対方向(-velocity)に回す回転に段々近づける
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(velocity),
                                                  applySpeed);      
        }
        else
        {
            isWalking = false;
            SEManager.Instance.Stop(SEPath.WALK);       //足音停止
        }
       
        rb.MovePosition(rb.position - velocity);         //プレイヤーの位置の更新
    }
    void Init()
    {
        isLight = isFireHeat = false;        //蝋燭の火を初期化
        
        playerGauge = gauge.GetComponent<PlayerGaugeScript>();
        playerGauge.SetPlayer(this);        //ゲージを自分に設定

        particle = fireEffect.GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        capsule = GetComponent<CapsuleCollider>();

        point = point.GetComponent<Light>();
        heatCollider = point.GetComponent<SphereCollider>();

        directionalLight = directionalLight.GetComponent<Light>();

        damageTime = timeleft;

        animID.Add(Animator.StringToHash("Walk"));
        animID.Add(Animator.StringToHash("Jump"));
        animID.Add(Animator.StringToHash("Crouching"));
        animID.Add(Animator.StringToHash("Fire"));
        animID.Add(Animator.StringToHash("Shot"));
        animID.Add(Animator.StringToHash("Death"));

        //セーブポイントに火を灯していたら
        if (GameManagerScript.IsHalfSave())
        {
            transform.position = new Vector3(half_x, half_y, 0);
            Damage(revivalDamage);         //20ダメージ受ける
        }
        HiddenTorchScript.hidden = false;       //隠し要素初期化
    }
    /// <summary>
    /// ライトがオンになった時に呼び出し
    /// </summary>
    private void pointLight()
    {
        if(!isClear)      //クリアしていない状態の時のみ
        {
            if (isLight)
            {
                if (point.range < fireOnRange)
                    point.range += lightRangeChangeSpeed;
                if (point.intensity > fireOnIntensity)
                    point.intensity -= lightIntensityChangeSpeed;
            }
            else
            {
                if (point.range > fireOffRange)
                    point.range -= lightRangeChangeSpeed;
                if (point.intensity < fireOffIntensity)
                    point.intensity += lightIntensityChangeSpeed;
            }
        }
    }
    /// <summary>
    /// 移動関数
    /// </summary>
    public void PlayerControl()
    {
        if (inputManager.GetKey(Key.LeftWalk1) || inputManager.GetKey(Key.LeftWalk2) ||
                    inputManager.GetAxes(Axes.L_Stick_H) <= -0.1f || inputManager.GetAxes(Axes.D_Pad_H) <= -0.1f)       //左移動
            velocity.x += 1;
        if (inputManager.GetKey(Key.RightWalk1) || inputManager.GetKey(Key.RightWalk2) ||
            inputManager.GetAxes(Axes.L_Stick_H) >= 0.1f || inputManager.GetAxes(Axes.D_Pad_H) >= 0.1f)     //右移動
            velocity.x -= 1;
        if ((inputManager.GetKey(Key.LeftWalk1) || inputManager.GetKey(Key.LeftWalk2)) &&
            (inputManager.GetKey(Key.RightWalk1) || inputManager.GetKey(Key.RightWalk2)))       //手前を向く
            velocity.z = 1;
        if (inputManager.GetAxes(Axes.L_Stick_V) <= -0.5f || inputManager.GetAxes(Axes.D_Pad_V) <= -0.5f)       //手前を向く
            velocity.z = 1;
        if (isFireHeat)
        {
            if (inputManager.GetKey(Key.Crouching1) || inputManager.GetKey(Key.Crouching2) || inputManager.GetKey(Key.Crouching3))      //しゃがみ
            {
                anim.SetBool("Crouching", true);
            }
            if (inputManager.GetKeyDown(Key.Shot1) || inputManager.GetKeyDown(Key.Shot2) || inputManager.GetKeyDown(Key.Shot3))     //発射
            {
                AnimAllOff();
                anim.SetBool("Shot", true);
                //anim.SetBool("Crouching", false);
                velocity.x = 0;
                velocity.z = 0;
                Damage(shotDamage);
                isKeep = true;
            }
            if (inputManager.GetKeyDown(Key.Fire1) || inputManager.GetKeyDown(Key.Fire2) || inputManager.GetKeyDown(Key.Fire3))     //着火
            {
                AnimAllOff();
                anim.SetBool("Fire", true);
                //anim.SetBool("Crouching", false);
                velocity.x = 0;
                velocity.z = 0;
                isKeep = true;
            }
        }
        if (inputManager.GetKeyDown(Key.Jump1) || inputManager.GetKeyDown(Key.Jump2) || inputManager.GetKeyDown(Key.Jump3))         //ジャンプ
        {
            if (GroundScript.IsGround())
            {
                anim.SetBool("Jump", true);
                rb.velocity = new Vector3(0, jumpPower, 0);
            }
        }
        if (inputManager.GetKeyDown(Key.ModeChange1) || inputManager.GetKeyDown(Key.ModeChange2) || inputManager.GetKeyDown(Key.ModeChange3))       //火の点火消火
        {
            if (!isFireHeat)
            {
                FireOn();
            }
            else
            {
                FireOff();
            }
        }
        if (inputManager.GetKeyUp(Key.Crouching1) || inputManager.GetKeyUp(Key.Crouching2) || inputManager.GetKeyUp(Key.Crouching3))        //しゃがみ解除
            anim.SetBool("Crouching", false);
        else
        {
            anim.SetBool("Walk", false);
        }
    }
    /// <summary>
    /// 蝋の発射関数
    /// </summary>
    public void Shot()
    {
        SEManager.Instance.Play(SEPath.SEFIRELITESHOT02, 1);
        GameObject createdBullet = Instantiate(bullet) as GameObject;       //インスタンス化して発射
        createdBullet.transform.position = bulletPos.transform.position;

        Vector3 force;      //発射ベクトル
        force = -bulletPos.transform.forward * speed;       //発射の向きと速度を決定
        force = force + bulletPos.transform.up * upSpeed;
        createdBullet.GetComponent<Rigidbody>().AddForce(force);        // Rigidbodyに力を加えて発射
    }
    /// <summary>
    /// アニメーション全て初期化
    /// </summary>
    public void AnimEnd()
    {
        isKeep = false;
        AnimAllOff();
    }
    void AnimAllOff()
    {
        for (int i = 0; i < animID.Count; i++)
        {
            anim.SetBool(animID[i], false);
        }
    }
    /// <summary>
    /// トラップに当たった時
    /// </summary>
    public void TrapAttack()
    {
        Damage(maxLife);        //即死ダメージ
    }
    /// <summary>
    /// 水に火のついている状態で当たった時
    /// </summary>
    public void WaterHit()
    {
        if(isFireHeat)
        {
            Damage(maxLife);        //即死ダメージ
        }
    }
    public void WaterExplosion()
    {

    }
    /// <summary>
    /// 死亡関数
    /// </summary>
    public void Death()
    {
        if(!isKeep)
        {
            SEManager.Instance.Play(SEPath.DEADSOUND, 1);       //死亡SE
        }
        GameManagerScript.CameraStop(true);  //カメラ停止

        AnimEnd();
        isKeep = true;        //行動不可
        anim.SetBool("Death", true);
    }
    /// <summary>
    /// 点火モーション開始
    /// </summary>
    public void FireMotionOn()
    {
        isFireMotion = true;
    }
    /// <summary>
    /// 点火モーション終了
    /// </summary>
    public void FireMotionOff()
    {
        isFireMotion = false;
    }
    /// <summary>
    /// 道生成
    /// </summary>
    public void Melt()
    {
        SEManager.Instance.Play(SEPath.MELT_SE);    //溶けるSE
        Instantiate(candleRoad, transform.position - new Vector3(0.0f, 7.5f, 0.0f), Quaternion.identity);
        Damage(meltDamage);     //10ダメージ
    }
    /// <summary>
    /// 火の点火
    /// </summary>
    public void FireOn()
    {
        particle.Play();    //火のアニメーションスタート
        //heatCollider.enabled = true;
        SEManager.Instance.Play(SEPath.IGNITION, 1,0,1,false,() =>      //点火SE
        {
            if (isFireHeat)       //火がついているなら
            {
                SEManager.Instance.Play(SEPath.CANDLE, 1, 0, 1, true);      //燃えてるSE
            }
        });

        isLight = true;
        isFireHeat = true;
    }
    /// <summary>
    /// 火の消火
    /// </summary>
    public void FireOff()
    {
        particle.Stop();    //火のアニメーション停止
        //heatCollider.enabled = false;
        SEManager.Instance.Stop(SEPath.CANDLE);     //燃えてるSE停止
        SEManager.Instance.Play(SEPath.FIRE_EXTINGUISHING, 1);      //消火SE
        SEManager.Instance.Stop(SEPath.ICE);
        isLight = false;
        isFireHeat = false;
    }
    /// <summary>
    /// アニメーション終了時にシーン移動
    /// </summary>
    public void FadeDeath()
    {
        FadeManager.FadeOut(overScene);
    }
    /// <summary>
    /// 松明の火を仮保存
    /// </summary>
    /// <param name="fire">灯された松明の名前</param>
    public void ClearFire(string fire)
    {
        fireList.Add(fire);
    }
    /// <summary>
    /// クリア判定がでたとき
    /// </summary>
    public void ClearFlg()
    {
        isKeep = true;
        SaveFireList();
        particle.Play();
        anim.Play("Stop");
        isClear = true;
    }
    /// <summary>
    /// ダメージ関数
    /// </summary>
    /// <param name="damage">ダメージ量 ‐をつけると回復</param>
    public void Damage(float damage)
    {
        playerGauge.GaugeReduction(damage);
        life -= damage;
    }
    public void OnCollisionStay(Collision collision)
    {
        if (life <= 0)
        {
            if (collision.gameObject.tag == "Road")
            {
                rb.isKinematic = true;
                capsule.enabled = false;
            }
        }
    }
    /// <summary>
    /// 松明の火保存
    /// </summary>
    void SaveFireList()
    {
        string blue = "fireblue";
        string green = "firegreen";
        string red = "firered";

        if (fireList.Contains(blue) == true)
        {
            GameManagerScript.blueCheck = true;
        }

        if (fireList.Contains(green) == true)
        {
            GameManagerScript.greenCheck = true;
        }

        if (fireList.Contains(red) == true)
        {
            GameManagerScript.redCheck = true;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (transform.parent == null && collision.gameObject.tag == "UpRoad")
        {
            transform.parent = collision.gameObject.transform;
        }

    }
    void OnCollisionExit(Collision collision)
    {
        if (transform.parent != null && collision.gameObject.tag == "UpRoad")
        {
            transform.parent = null;
        }
    }
}