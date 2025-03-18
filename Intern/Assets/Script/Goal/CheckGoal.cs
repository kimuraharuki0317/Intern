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
    /// プレイヤーのタグを持つコライダーが入ってきたらクリア画面に遷移する
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Player_Tag) {
            SceneManager.LoadScene(Result_Scene_Name);
        }
    }
}
