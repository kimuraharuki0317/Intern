using UnityEngine;

/// <summary>
/// プレイヤーを追いかける敵の処理
/// </summary>
public class Following : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのタグ
    /// </summary>
    const string Player_Tag = "Player";

    /// <summary>
    /// 敵オブジェクト
    /// </summary>
    [SerializeField]
    GameObject EnemyObject;

    /// <summary>
    /// 徘徊機能
    /// </summary>
    [SerializeField]
    MoveObject EnemyMovement;

    /// <summary>
    /// 追跡速度
    /// </summary>
    [SerializeField]
    float ChaseSpeed = 1.0f;

    /// <summary>
    /// 見つかっているか
    /// </summary>
    bool find = false;

    /// <summary>
    /// 追跡対象
    /// </summary>
    GameObject targetObject;

    void Update()
    {
        // 見つかっていたら追跡を行う
        if(find) {
            var moveVector = Vector3.Normalize(targetObject.transform.position - EnemyObject.transform.position);
            moveVector.y = 0;
            EnemyObject.transform.forward = -moveVector;
            EnemyObject.transform.position += moveVector * Time.deltaTime * ChaseSpeed;
        }
    }

    /// <summary>
    /// プレイヤーのタグを持つコライダーが入ってきたら徘徊を止め、ターゲットを設定する
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Player_Tag) {
            EnemyMovement.enabled = false;
            find = true;
            targetObject = other.gameObject;
        }
    }

    /// <summary>
    /// プレイヤーのタグを持つコライダーが出たらターゲットを消去し、徘徊を再開する
    /// </summary>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Player_Tag) {
            targetObject = null;
            find = false;
            EnemyMovement.enabled = true;
        }
    }
}
