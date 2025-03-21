using UnityEngine;

/// <summary>
/// 敵を踏みつける処理
/// </summary>
public class TreadEnemy : MonoBehaviour
{   /// <summary>
    /// プレイヤーののRigidbody
    /// </summary>
    [SerializeField]
    Rigidbody PlayerRigidbody;

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
            Destroy(other.gameObject);
            PlayerRigidbody.AddForce(Tread_Power * Vector3.up, ForceMode.Impulse);
        }
    }
}
