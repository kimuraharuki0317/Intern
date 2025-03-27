using UnityEngine;

/// <summary>
/// 残り時間を計算、保有するクラス
/// </summary>
public class TimeCounter : MonoBehaviour
{
    /// <summary>
    /// ChangeSceneコンポーネント
    /// </summary>
    [SerializeField]
    ChangeScene ChangeSceneComponent;

    /// <summary>
    /// ゲームオーバーシーン名
    /// </summary>
    const string Game_Over_Scene_Name = "GameOver";

    /// <summary>
    /// 制限時間
    /// </summary>
    public const int Time_Limit = 300;

    /// <summary>
    /// 残り時間
    /// </summary>
    float remainingTime;

    /// <summary>
    /// カウントを停止するか
    /// </summary>
    bool stopCount;

    /// <summary>
    /// 変数の初期化を行う
    /// </summary>
    void Start()
    {
        remainingTime = Time_Limit;
        stopCount = false;
    }

    /// <summary>
    /// カウントダウンを進め、0未満になったらゲームオーバー
    /// </summary>
    void Update()
    {
        // 停止中でなければ、カウントダウンを行う
        if(!stopCount) {
            remainingTime -= Time.deltaTime;

            // 時間切れになったら
            if (remainingTime < 0) {
                // 値がマイナスにならないようにし、ゲームオーバー画面へ
                remainingTime = 0;
                ChangeSceneComponent.Change(Game_Over_Scene_Name);
            }
        }
    }

    /// <summary>
    /// カウントを停止する
    /// </summary>
    public void StopCount()
        => stopCount = true;

    /// <summary>
    /// 残り時間を取得する
    /// </summary>
    /// <returns>現地点での残り時間(int)</returns>
    public int GetRemainingTime()
        => (int)remainingTime;
}
