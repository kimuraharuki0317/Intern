using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 敵に触れたらゲームオーバーにする
/// </summary>
public class TouchEnemy : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのタグ
    /// </summary>
    const string Enemy_Tag = "Enemy";

    /// <summary>
    /// リザルトシーン名
    /// </summary>
    const string Game_Over_Scene_Name = "GameOver";

    /// <summary>
    /// Enemyのタグを持つコライダーが入ってきたらゲームオーバー画面に遷移する
    /// </summary>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == Enemy_Tag) {
            SceneManager.LoadScene(Game_Over_Scene_Name);
        }
    }
}
