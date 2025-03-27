using UnityEngine;
using System.Collections;

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
    /// ヒットストップ時間
    /// </summary>
    [SerializeField]
    float HitStopTime = 0.2f;

    /// <summary>
    /// ヒットストップ速度
    /// </summary>
    [SerializeField]
    float HitStopSpeed = 0.2f;

    /// <summary>
    /// エフェクト生成位置をどれだけずらすか
    /// </summary>
    [SerializeField]
    Vector3 EffectPosition;

    /// <summary>
    /// エフェクト
    /// </summary>
    [SerializeField]
    GameObject HitEffect;

    /// <summary>
    /// 他のオブジェクトが入ってきたら、そのオブジェクトに応じて倒すか動き出すかのどちらかを行う
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Player_Tag)) {
            var pushVector = Vector3.Normalize(transform.position - other.transform.position);
            pushVector.y = 0;
            GetComponent<Rigidbody>().AddForce((pushVector * PushPower + other.gameObject.transform.forward) / 2.0f, ForceMode.Impulse);
        } 

        if(other.gameObject.CompareTag(Enemy_Tag)) {
            Instantiate(HitEffect, other.gameObject.transform.position + EffectPosition, Quaternion.identity);
            StartCoroutine(DoHitStop());
            ScoreCounter.AddEnemyScore();
            Destroy(other.gameObject);
        }
    }

    //ヒットストップ
    IEnumerator DoHitStop()
    {
        Time.timeScale = HitStopSpeed;
        yield return new WaitForSecondsRealtime(HitStopTime);
        Time.timeScale = 1.0f;

    }
}
