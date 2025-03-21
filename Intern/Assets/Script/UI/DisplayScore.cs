using UnityEngine;
using TMPro;

/// <summary>
/// スコアをTMProで表示するクラス
/// </summary>
public class DisplayScore : MonoBehaviour
{
    /// <summary>
    /// 表示させるTMProコンポーネント
    /// </summary>
    [SerializeField]
    TMP_Text ScoreText;

    /// <summary>
    /// スコアを取得、表示する
    /// </summary>
    void Update()
        => ScoreText.text = ScoreCounter.GetScore().ToString();
}
