using UnityEngine;

/// <summary>
/// 甲羅で敵を倒す処理
/// </summary>
public class Shell : MonoBehaviour
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
    /// 押す力
    /// </summary>
    [SerializeField]
    float PushPower = 20.0f;

    /// <summary>
    /// 他のオブジェクトが入ってきたら、そのオブジェクトに応じて倒すか動き出すかのどちらかを行う
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Player_Tag)) {
            var pushVector = Vector3.Normalize(transform.position - other.transform.position);
            pushVector.y = 0;
            GetComponent<Rigidbody>().AddForce(pushVector * PushPower, ForceMode.Impulse);
        } 

        if(other.gameObject.CompareTag(Enemy_Tag)) {
            ScoreCounter.AddEnemyScore();
            Destroy(other.gameObject);
        }
    }
}
