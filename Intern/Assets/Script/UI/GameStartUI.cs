using System.Collections;
using UnityEngine;

/// <summary>
/// 開始時のUIを一定時間後に消す
/// </summary>
public class GameStartUI : MonoBehaviour
{
    /// <summary>
    /// UI非表示までの時間
    /// </summary>
    const float Destroy_Delay = 3.0f;

    /// <summary>
    /// 敵オブジェクトのタグ
    /// </summary>
    void Start()
    {
        StartCoroutine(DestroyObject());
    }

    /// <summary>
    // 呼び出されてからScene_Trandition_Delay秒後に消す
    /// </summary>
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(Destroy_Delay);
        Destroy(gameObject);
    }
}
