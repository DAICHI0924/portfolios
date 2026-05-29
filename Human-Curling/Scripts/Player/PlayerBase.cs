using UnityEngine;
using System.Collections;
using tomochin.Resource;
using DG.Tweening;
using AudioManager.SE;

public abstract class PlayerBase: MonoBehaviour
{
    [SerializeField] private float firstSpeed; //速さ   //private変数にアンダーバーつけたい
    [SerializeField] private float firstScoreRate; //得点
    [SerializeField] private float bounciness; //弾性
    [SerializeField] private float dynamicFriction; //動摩擦力
    [SerializeField] private float staticFriction; //静止摩擦力
    [SerializeField] private float firstAttack; //障害物破壊力
    [SerializeField] private float firstPointRate = 1; //ポイント倍率
    private float speed;
    private float scoreRate;
    private float Attack;
    private float pointRate;
    public int bounceCount = 0;


    [Header("実行中PlayerのTransform参照")]
    [SerializeField] private RunTimeTransformSO _runTimePlayerTransformSO; //実行中TransformSO
    [SerializeField] private RunTimeRigidBodySO _runTimePlayerRigidbodySO; //実行中RigidbodySO
    [SerializeField] private RunTimePlayerBaseSO _runTimePlayerBaseSO; //実行中PlayerBaseSO
    [Header("エフェクト再生用SO")]
    [SerializeField] private EffectEventSO _effectEventSO; //エフェクト再生用SO

    [Header("PlayerのゴールエフェクトのMaterial取得用MeshRenderer")]
    [SerializeField] private SkinnedMeshRenderer _goalEffectMeshRenderer; //PlayerのゴールエフェクトのMaterial取得用MeshRenderer
    private Color currentEffectColor; //現在のエフェクトの色
    public float _speed => speed;   //public変数のアンダーバー外したい
    public float _scoreRate => scoreRate;
    public float _bounciness => bounciness;
    public float _dynamicFriction => dynamicFriction;
    public float _staticFriction => staticFriction;
    public float _Attack => Attack;
    public float _pointRate => pointRate;


    public void Awake()
    {
        var mat = new PhysicsMaterial();
        mat.bounciness = _bounciness;
        mat.dynamicFriction = _dynamicFriction;
        mat.staticFriction = _staticFriction;
        mat.bounceCombine = PhysicsMaterialCombine.Maximum;
        mat.frictionCombine = PhysicsMaterialCombine.Maximum;
        ResetSpeed();
        ResetScoreRate();
        ResetAttack();
        ResetPointRate();

        GetComponent<Collider>().material = mat;

        _runTimePlayerTransformSO.Register(this.gameObject);
        _runTimePlayerRigidbodySO.Register(this.gameObject);
        _runTimePlayerBaseSO.Register(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // エフェクト再生Requestを発行
            _effectEventSO?.OnRequestPlayEffect(EffectType.Hit, collision.contacts[0].point);
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {

        }
        else
        {
            // エフェクト再生Requestを発行
            _effectEventSO?.OnRequestPlayEffect(EffectType.Boing, collision.contacts[0].point);
            SEManager.Instance.Play(SEName.CharacterCollide);
        }

        //
        _runTimePlayerRigidbodySO.Get(gameObject).linearVelocity = new Vector3(_runTimePlayerRigidbodySO.Get(gameObject).linearVelocity.x, 0, _runTimePlayerRigidbodySO.Get(gameObject).linearVelocity.z);
    }

    public void SetGoalEffectMaterial(Color color)
    {
        if (currentEffectColor == color) return; //すでに同じ色のエフェクトが設定されている場合は何もしない
        _goalEffectMeshRenderer.materials[1].SetColor("_EffectColor", color);
        _goalEffectMeshRenderer.materials[1].SetFloat("_EffectPower", 0.5f);
        currentEffectColor = color;
    }

    public void SetNoneEffectMaterial()
    {
        if (currentEffectColor == Color.clear) return; //すでにエフェクトなしの場合は何もしない
        _goalEffectMeshRenderer.materials[1].SetFloat("_EffectPower", 3f);
        currentEffectColor = Color.clear;
    }

    public void SetAttack(float Attack)
    {
        this.Attack = Attack;
    }

    public void MultiplyPointRate(float rate)
    {
        pointRate *= rate;
    }

    public void ResetSpeed()
    {
        speed = firstSpeed;
    }
    public void ResetScoreRate()
    {
        scoreRate = firstScoreRate;
    }
    public void ResetAttack()
    {
        Attack = firstAttack;
    }
    public void ResetPointRate()
    {
        pointRate = firstPointRate;
    }

    public void SetScoreRate(float sr)
    {
        scoreRate = sr;
    }

    public void OnDestroy()
    {
        _runTimePlayerTransformSO.Unregister(this.gameObject);
        _runTimePlayerRigidbodySO.Unregister(this.gameObject);
        _runTimePlayerBaseSO.Unregister(this.gameObject);
    }
}
