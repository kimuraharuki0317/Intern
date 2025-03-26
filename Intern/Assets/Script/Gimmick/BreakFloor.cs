using UnityEngine;
using System.Collections;

/// <summary>
/// 一定時間乗っていたら消える床
/// </summary>
public class BreakFloor : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのタグ
    /// </summary>
    const string Player_Tag = "Player";

    /// <summary>
    /// プレイヤーが上に乗っているか
    /// </summary>
    [SerializeField]
    bool RidingPlayer = false;

    /// <summary>
    /// 崩壊フラグ
    /// </summary>
    [SerializeField]
    bool BreakFlag = false;

    /// <summary>
    /// 床コライダー
    /// </summary>
    [SerializeField]
    BoxCollider FloorCollider;

    /// <summary>
    /// 床レンダラー
    /// </summary>
    [SerializeField]
    MeshRenderer FloorRenderer;

    /// <summary>
    /// 床マテリアル(共有)
    /// </summary>
    [SerializeField]
    Material FloorMaterial;

    /// <summary>
    /// 床マテリアル(このオブジェクトの)
    /// </summary>
    Material floorMaterial;

    /// <summary>
    /// 崩壊までの秒数
    /// </summary>
    [SerializeField]
    float BreakLimit = 2.0f;

    /// <summary>
    /// 通常時の透過度
    /// </summary>
    [SerializeField]
    float DefaultAlpha = 1.0f;

    /// <summary>
    /// 崩壊中の透過度
    /// </summary>
    [SerializeField]
    float BreakAlpha = 0.2f;

    /// <summary>
    /// 再生までの秒数
    /// </summary>
    [SerializeField]
    float RespawnTime = 2.0f;

    /// <summary>
    /// 現在のカウント
    /// </summary>
    float breakCount = 0.0f;

    void Start()
    {
        // マテリアルをコピーし、割り当てる
        floorMaterial=new Material(FloorMaterial);
        FloorRenderer.material = floorMaterial;
    }

    void Update()
    {
        // 崩壊フラグが立っていたら、カウントを進める
        if (RidingPlayer) {
            breakCount += Time.deltaTime;
        } else {
            breakCount -= Time.deltaTime;
            breakCount=Mathf.Max(0, breakCount);
        }

        // 床をなくす
        if(!BreakFlag && BreakLimit < breakCount) {
            BreakFlag = true;
            StartCoroutine(Break());
        }

        // マテリアルの値を変える
        floorMaterial.SetFloat("_BreakCount", breakCount / BreakLimit);
        var floorAlpha= BreakFlag ? BreakAlpha : DefaultAlpha;
        floorMaterial.SetFloat("_Alpha", floorAlpha);
    }

    /// <summary>
    /// プレイヤーのタグを持つコライダーが入ってきたら崩壊フラグを立てる
    /// </summary>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == Player_Tag) {
            RidingPlayer = true;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == Player_Tag) {
            RidingPlayer = true;
        }
    }

    /// <summary>
    /// プレイヤーのタグを持つコライダーが出たら崩壊フラグをなくす
    /// </summary>
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == Player_Tag) {
            RidingPlayer = false;
        }
    }

    /// <summary>
    /// 床を壊し、一定時間後に再生する
    /// </summary>
    IEnumerator Break()
    {
        // 見えないようにし、当たり判定を消す
        FloorCollider.enabled = false;

        // 再生待ち
        yield return new WaitForSeconds(BreakLimit);

        // 見えるようにし、当たり判定を有効化
        breakCount = 0.0f;
        FloorCollider.enabled = true;
        BreakFlag = false;
        RidingPlayer = false;
    }
}
