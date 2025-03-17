using UnityEngine;

public class CheckGround : MonoBehaviour
{
    /// <summary>
    /// 接地判定用コライダー
    /// </summary>
    [SerializeField]
    BoxCollider groundCheckDistance;

    /// <summary>
    /// 接地しているか
    /// </summary>
    bool hitGround = false;

    /// <summary>
    /// 地面オブジェクトのタグ
    /// </summary>
    const string groundTag = "Ground";

    /// <summary>
    /// 地面に接触しているかを返す
    /// </summary>
    /// <returns>地面に接触しているか(bool)</returns>
    public bool HitGround()
    {
        return hitGround;
    }

    /// <summary>
    /// 地面のタグを持つコライダーが入ってきたらhitGroundをtrueにする
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == groundTag){
            hitGround = true;
        }
    }

    /// <summary>
    /// 地面のタグを持つコライダーが出たらhitGroundをtrueにする
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == groundTag){
            hitGround = false;
        }
    }
}
