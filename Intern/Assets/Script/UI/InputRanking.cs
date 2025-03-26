using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

/// <summary>
/// ランキングにデータを入力する
/// </summary>
public class InputRanking : MonoBehaviour
{
    /// <summary>
    /// ポップアップのUIパネル
    /// </summary>
    [SerializeField]
    GameObject PopupPanel;

    /// <summary>
    /// 名前入力欄
    /// </summary>
    [SerializeField]
    TMP_InputField NameInputField;

    /// <summary>
    /// RankingDataコンポーネント
    /// </summary>
    [SerializeField]
    RankingData RankingDataConponent;

    /// <summary>
    /// DisplayRankingコンポーネント
    /// </summary>
    [SerializeField]
    DisplayRanking DisplayRankingComponent;

    /// <summary>
    /// 空白時の名前
    /// </summary>
    const string Empty_Name = "Unknown";

    /// <summary>
    /// ランキングが更新されていたら入力UIを呼び出す
    /// </summary>
    void Start()
    {
        // 入力用のUIを非表示する
        PopupPanel.SetActive(false);

        // ランキングが更新されているか判断する
        var score = RankingDataConponent.LoadScores();

        for (var i = 0; i < score.Count; i++) {
            // ランキングが更新されていたら入力用のUIを表示する
            if (score[i].score < ScoreCounter.GetScore()) {
                PopupPanel.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 登録ボタンが押されたら呼び出す。入力された名前とデータをランキングに挿入し、ランキングを更新
    /// </summary>
    public void EnterButton()
    {
        var playerName = NameInputField.text.Trim();

        if (string.IsNullOrEmpty(playerName)) {
            playerName = Empty_Name;
        }

        RankingDataConponent.SaveScore(playerName, ScoreCounter.GetScore());

        // 入力UIを消す
        PopupPanel.SetActive(false);
        DisplayRankingComponent.UpdateRanking();
    }
}
