using UnityEngine;

/// <summary>
/// 敵に触れたらゲームオーバーにする
/// </summary>
public class TouchEnemy : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのタグ
    /// </summary>
    const string Enemy_Tag = "Enemy";

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
    const float Enemy_Push_Power = 5.0f;

    /// <summary>
    /// 衝突エフェクト生成位置をどれだけずらすか
    /// </summary>
    [SerializeField]
    Vector3 EffectPosition;

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
    /// Enemyのタグを持つコライダーが入ってきたらゲームオーバー画面に遷移する
    /// </summary>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == Enemy_Tag) {
            // 敵とプレイヤーの動きを止める
            Movement.enabled = false;
            Tread.enabled = false;
            other.gameObject.GetComponent<MoveObject>().enabled = false;

            // プレイヤーを吹き飛ばす
            Rb.AddForce(Vector3.Normalize(transform.position - other.gameObject.transform.position) * Enemy_Push_Power, ForceMode.Impulse);

            // 敵とプレイヤーの中間にエフェクトを生成
            Instantiate(HitEffect, EffectPosition　+ (transform.position + other.gameObject.transform.position) / 2.0f, Quaternion.identity);

            ChangeSceneComponent.Change(Game_Over_Scene_Name, Scene_Trandition_Delay);
        }
    }
}
