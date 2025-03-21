using UnityEngine;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// ランキング表示
/// </summary>
public class DisplayRanking : MonoBehaviour
{
    /// <summary>
    /// 表示させるTMProコンポーネント
    /// </summary>
    [SerializeField]
    TMP_Text RankingText;

    /// <summary>
    /// ScoreDataコンポーネント
    /// </summary>
    [SerializeField]
    RankingData ScoreDataComponent;

    void Start()
        => UpdateRanking();

    /// <summary>
    /// スコアを読み込み、ランキングをRankingTextに表示する
    /// </summary>
    public void UpdateRanking()
    {
        var scores = ScoreDataComponent.LoadScores();
        RankingText.text = "Ranking\n";
        for (var i = 0; i < scores.Count; i++) {
            RankingText.text += $"{i + 1} : {scores[i].name} - {scores[i].score}\n";
        }
    }
}
