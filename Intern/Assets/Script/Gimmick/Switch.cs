using UnityEngine;

/// <summary>
/// 敵かプレイヤーによってスイッチが押されている間、指定したオブジェクトを無効にする
/// </summary>
public class Switch : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのタグ
    /// </summary>
    const string Player_Tag = "Player";

    /// <summary>
    /// 敵オブジェクトのタグ
    /// </summary>
    const string Enemy_Tag = "Enemy";

    /// <summary>
    /// スイッチの影響を受けるオブジェクト
    /// </summary>
    [SerializeField]
    GameObject Object;

    /// <summary>
    /// 敵かプレイヤーが入っていたら押下状態にし、オブジェクトを無効にする
    /// </summary>
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == Enemy_Tag || other.gameObject.tag == Player_Tag) {
            Object.SetActive(false);
        }
    }

    /// <summary>
    /// 敵かプレイヤーが出たら初期状態にし、オブジェクトを有効にする
    /// </summary>
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == Enemy_Tag || other.gameObject.tag == Player_Tag) {
            Object.SetActive(true);
        }
    }
}
