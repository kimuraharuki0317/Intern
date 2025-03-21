using UnityEngine;

/// <summary>
/// オブジェクトが地面に接触しているかを検出するためのクラスです。
/// </summary>
public class CheckGround : MonoBehaviour
{
    /// <summary>
    /// 接地判定用コライダー
    /// </summary>
    [SerializeField]
    BoxCollider Collider;

    /// <summary>
    /// 接地しているか
    /// </summary>
    bool hitGround = false;

    /// <summary>
    /// 地面オブジェクトのタグ
    /// </summary>
    const string Ground_Tag = "Ground";

    /// <summary>
    /// 敵オブジェクトのタグ
    /// </summary>
    const string Enemy_Tag = "Enemy";

    /// <summary>
    /// 地面に接触しているかを返す
    /// </summary>
    /// <returns>地面に接触しているか(bool)</returns>
    public bool GetHitGround()
        => hitGround;

    /// <summary>
    /// 地面のタグを持つコライダーが入ってきたら hitGround を true にする
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Ground_Tag || other.gameObject.tag == Enemy_Tag) {
            hitGround = true;
        }
    }

    /// <summary>
    /// 地面のタグを持つコライダーに接触していたら hitGround を true にする
    /// </summary>
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == Ground_Tag || other.gameObject.tag == Enemy_Tag){
            hitGround = true;
        }
    }

    /// <summary>
    /// 地面のタグを持つコライダーが出たら hitGround を false にする
    /// </summary>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Ground_Tag || other.gameObject.tag == Enemy_Tag) {
            hitGround = false;
        }
    }
}
