using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class TitlePlayerScript : MonoBehaviour
{

    private Vector3 velocity;              // 移動方向
    private float moveSpeed = 20.0f;        // 移動速度
    private float applySpeed = 0.2f;       // 回転の適用速度

    [SerializeField] private List<int> animID;

    private Rigidbody rb;
    private Animator anim;                          // キャラにアタッチされるアニメーターへの参照
    
    //Light
    private float fireOnRange = 50.0f;
    private float fireOnIntensity = 10.0f;

    private float lightRangePower = 25.0f;
    private float lightIntensityPower = 10.0f;

    private float lightRangeChangeSpeed;    //蝋燭の光の範囲の切り替え時の速度
    private float lightIntensityChangeSpeed;//蝋燭の光の強さの切り替え時の速度

    [SerializeField] private GameObject fireEffect;
    [SerializeField] private Light point;
    ParticleSystem particle;

    [SerializeField] private InputManager inputManager;

    public void Awake()
    {
        inputManager = inputManager.GetComponent<InputManager>();
    }
    private void Start()
    {
        particle = fireEffect.GetComponent<ParticleSystem>();
        // Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        point = point.GetComponent<Light>();
        LightOn();
        
        animID.Add(Animator.StringToHash("Jump"));
        animID.Add(Animator.StringToHash("Crouching"));
        animID.Add(Animator.StringToHash("Fire"));
        animID.Add(Animator.StringToHash("Shot"));
        animID.Add(Animator.StringToHash("Death"));
    }
    void Update()
    {
        lightRangeChangeSpeed = lightRangePower * Time.deltaTime;        //光の範囲変更速度計算
        lightIntensityChangeSpeed = lightIntensityPower * Time.deltaTime;    //光の強さ変更速度計算
        pointLight();
        velocity = Vector3.zero;
        velocity.x -= 1;
        // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;

        // いずれかの方向に移動している場合
        if (velocity.magnitude > 0)
        {
            anim.SetBool("Walk", true);
            // プレイヤーの回転(transform.rotation)の更新
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(velocity),
                                                  applySpeed);
        }
        rb.MovePosition(rb.position - velocity);         //プレイヤーの位置の更新
    }

    private void pointLight()
    {
        if (point.range < fireOnRange)
            point.range += lightRangeChangeSpeed;
        if (point.intensity > fireOnIntensity)
            point.intensity -= lightIntensityChangeSpeed;
    }
    public void AnimEnd()
    {
        AnimAllOff();
    }
    void AnimAllOff()
    {
        for (int i = 0; i < animID.Count; i++)
        {
            anim.SetBool(animID[i], false);
        }
    }
    public void LightOn()
    {
        particle.Play();    //火のアニメーションスタート
    }
}