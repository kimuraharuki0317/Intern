using UnityEngine;

/// <summary>
/// スコアを計算、保有するクラス
/// </summary>
public class ScoreCounter : MonoBehaviour
{
    /// <summary>
    /// スコア
    /// </summary>
    static int gameScore;

    /// <summary>
    /// 敵を倒した時のスコア
    /// </summary>
    const int Enemy_Score = 5;

    /// <summary>
    /// スコアのリセット
    /// </summary>
    void Start()
        => gameScore = 0;

    /// <summary>
    /// スコアを追加する
    /// </summary>
    /// <param name="score">何点追加するか</param>
    public static void AddScore(int score)
        =>gameScore += score;

    /// <summary>
    /// 敵を倒した分のスコアを追加する
    /// </summary>
    public static void AddEnemyScore()
        => AddScore(Enemy_Score);

    /// <summary>
    /// スコアを取得する
    /// </summary>
    /// <returns>現地点でのスコア(int)</returns>

    public static int GetScore()
    {
        return gameScore;
    }
}
