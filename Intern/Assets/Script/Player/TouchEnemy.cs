using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

/// <summary>
/// 敵に触れたらゲームオーバーにする
/// </summary>
public class TouchEnemy : MonoBehaviour
{
    /// <summary>
    /// 敵のタグ
    /// </summary>
    const string Enemy_Tag = "Enemy";

    /// <summary>
    /// ギミックのタグ
    /// </summary>
    const string Gimmick_Tag = "Gimmick";

    /// <summary>
    /// リザルトシーン名
    /// </summary>
    const string Game_Over_Scene_Name = "GameOver";

    /// <summary>
    /// シーン遷移までのディレイ
    /// </summary>
    const float Scene_Trandition_Delay = 2.0f;

    /// <summary>
    /// 敵に当たった時のふっとび量
    /// </summary>
    [SerializeField]
    float Enemy_Push_Power = 20.0f;

    /// <summary>
    /// 上方向へのふっとび量
    /// </summary>
    [SerializeField]
    float BurstDistanceY = 20.0f;

    /// <summary>
    /// 衝突エフェクト生成位置をどれだけずらすか
    /// </summary>
    [SerializeField]
    Vector3 EffectPosition;

    /// <summary>
    /// ボリュームコンポーネント
    /// </summary>
    [SerializeField]
    Volume PostProcessVolume;

    /// <summary>
    /// 被ダメージ時のカラーフィルター
    /// </summary>
    ColorAdjustments colorAdjustments;

    /// <summary>
    /// PlayerMovementコンポーネント
    /// </summary>
    [SerializeField]
    PlayerMovement Movement;

    /// <summary>
    /// TreadEnemyコンポーネント
    /// </summary>
    [SerializeField]
    TreadEnemy Tread;

    /// <summary>
    /// RigidBodyコンポーネント
    /// </summary>
    [SerializeField]
    Rigidbody Rb;

    /// <summary>
    /// 衝突エフェクト
    /// </summary>
    [SerializeField]
    GameObject HitEffect;

    /// <summary>
    /// ChangeSceneコンポーネント
    /// </summary>
    [SerializeField]
    ChangeScene ChangeSceneComponent;

    /// <summary>
    /// 吹き飛び時のパーティクル
    /// </summary>
    [SerializeField]
    ParticleSystem Particle;

    /// <summary>
    /// ヒットストップ時間
    /// </summary>
    [SerializeField]
    float HitStopTime = 0.2f;

    /// <summary>
    /// ヒットストップ速度
    /// </summary>
    [SerializeField]
    float HitStopSpeed = 0.2f;

    void Start()
    {
        // 赤フィルター無効化
        PostProcessVolume.profile.TryGet(out colorAdjustments);
        colorAdjustments.active = false;
    }

    /// <summary>
    /// Enemyのタグを持つコライダーが入ってきたらゲームオーバー画面に遷移する
    /// </summary>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == Enemy_Tag || other.gameObject.tag == Gimmick_Tag) {
            // 敵とプレイヤーの動きを止める
            Movement.enabled = false;
            Tread.enabled = false;

            StartCoroutine(DoHitStop());

            // 敵だったら動きを止める
            if (other.gameObject.tag == Enemy_Tag) {
                other.gameObject.GetComponent<MoveObject>().enabled = false;
            }

            // プレイヤーを吹き飛ばす
            Rb.AddForce(Vector3.Normalize(transform.position - other.gameObject.transform.position) * Enemy_Push_Power + new Vector3(0, BurstDistanceY, 0), ForceMode.Impulse);

            // 敵とプレイヤーの中間にエフェクトを生成
            Instantiate(HitEffect, EffectPosition　+ (transform.position + other.gameObject.transform.position) / 2.0f, Quaternion.identity);

            // 吹き飛びパーティクル再生
            if (!Particle.isPlaying) {
                Particle.Play();
            }

            ChangeSceneComponent.Change(Game_Over_Scene_Name, Scene_Trandition_Delay);
        }
    }

    /// <summary>
    /// やられ時のヒットストップ
    /// </summary>
    IEnumerator DoHitStop()
    {
        colorAdjustments.active = true;
        Time.timeScale = HitStopSpeed;
        yield return new WaitForSecondsRealtime(HitStopTime);
        Time.timeScale = 1.0f;
        colorAdjustments.active = false;
    }
}
