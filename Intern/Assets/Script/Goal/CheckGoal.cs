using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// プレイヤーがゴールにたどり着いたか検出するクラスです。
/// </summary>
public class CheckGoal : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのタグ
    /// </summary>
    const string Player_Tag = "Player";

    /// <summary>
    /// リザルトシーン名
    /// </summary>
    const string Result_Scene_Name = "Result";

    /// <summary>
    /// 画面切り替えまでの遅延秒数
    /// </summary>
    const float Scene_Trandition_Delay = 3.0f;

    /// <summary>
    /// クリア演出用オブジェクト
    /// </summary>
    [SerializeField]
    GameObject ClearEffect;

    /// <summary>
    /// PlayerMovementコンポーネント
    /// </summary>
    [SerializeField]
    PlayerMovement Movement;

    /// <summary>
    /// ChangeSceneコンポーネント
    /// </summary>
    [SerializeField]
    ChangeScene ChangeSceneComponent;

    /// <summary>
    /// TimeCounterコンポーネント
    /// </summary>
    [SerializeField]
    TimeCounter TimeCounterComponent;

    /// <summary>
    /// プレイヤーのタグを持つコライダーが入ってきたらクリア画面に遷移する
    /// </summary>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == Player_Tag) {
            // プレイヤーの動きを止める
            Movement.enabled = false;

            // 遅延を掛けてシーンを切り替え、演出オブジェクトを生成する
            ChangeSceneComponent.Change(Result_Scene_Name, Scene_Trandition_Delay);
            Instantiate(ClearEffect);

            // カウントダウン停止、残り時間をスコアに加算
            TimeCounterComponent.StopCount();
            ScoreCounter.AddScore(TimeCounterComponent.GetRemainingTime());
        }
    }
}
