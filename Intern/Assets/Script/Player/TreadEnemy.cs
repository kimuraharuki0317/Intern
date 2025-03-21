using UnityEngine;

/// <summary>
/// 敵を踏みつける処理
/// </summary>
public class TreadEnemy : MonoBehaviour
{
    /// <summary>
    /// プレイヤーののRigidbody
    /// </summary>
    [SerializeField]
    Rigidbody PlayerRigidbody;

    /// <summary>
    /// 敵を踏んだ時のエフェクト
    /// </summary>
    [SerializeField]
    GameObject TreadEffect;

    /// <summary>
    /// 踏みつけエフェクト生成位置をどれだけずらすか
    /// </summary>
    [SerializeField]
    Vector3 EffectPosition;

    /// <summary>
    /// 敵オブジェクトのタグ
    /// </summary>
    const string Enemy_Tag = "Enemy";

    /// <summary>
    /// 敵を踏みつける力の強さ
    /// </summary>
    const float Tread_Power = 7.0f;

    /// <summary>
    /// 敵がコライダーにはいったら踏みつけてジャンプし、敵を消す
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Enemy_Tag) {
            // 敵とプレイヤーの中間にエフェクトを生成
            Instantiate(TreadEffect, EffectPosition + (transform.position + other.gameObject.transform.position) / 2.0f, Quaternion.identity);
            Destroy(other.gameObject);
            PlayerRigidbody.AddForce(Tread_Power * Vector3.up, ForceMode.Impulse);
            // 敵を倒した分のスコア加算
            ScoreCounter.AddEnemyScore();
        }
    }
}
