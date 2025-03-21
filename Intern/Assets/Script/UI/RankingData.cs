using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// ランキングのデータを保有する
/// </summary>
public class RankingData : MonoBehaviour
{
    /// <summary>
    /// スコアのキー
    /// </summary>
    const string ScoreKey = "HighScore";

    /// <summary>
    /// 名前のキー
    /// </summary>
    const string NameKey = "HighScoreName";

    /// <summary>
    /// ランキングの最大数
    /// </summary>
    const int MaxRankings = 5;

    /// <summary>
    /// 最新データをもとに、ランキング最大数分のスコアをセーブする
    /// </summary>
    /// <param name="playerName">ユーザー名</param>
    /// <param name="newScore">スコア</param>
    public void SaveScore(string playerName, int newScore)
    {
        var newScores = LoadScores();
        newScores.Add((playerName, newScore));
        // 高スコア順に並べ替え
        newScores = newScores.OrderByDescending(s => s.score).Take(MaxRankings).ToList();

        // PlayerPrefsに保存
        for (var i = 0; i < newScores.Count; i++) {
            PlayerPrefs.SetString(NameKey + i, newScores[i].name);
            PlayerPrefs.SetInt(ScoreKey + i, newScores[i].score);
        }
        PlayerPrefs.Save();
    }

    /// <summary>
    /// スコアとユーザー名を読み込む
    /// </summary>
    /// <returns>スコアとユーザー名のリスト(List<(string name, int score)>)</returns>
    public List<(string name, int score)> LoadScores()
    {
        var scores = new List<(string, int)>();

        for (var i = 0; i < MaxRankings; i++) {
            // リストに追加
            var name = PlayerPrefs.GetString(NameKey + i, "");
            var score = PlayerPrefs.GetInt(ScoreKey + i, 0);
            scores.Add((name, score));
        }
        return scores;
    }
}
