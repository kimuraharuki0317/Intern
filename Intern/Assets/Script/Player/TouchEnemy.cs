using System.Collections;
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
    /// シーン遷移までのディレイ
    /// </summary>
    const float Scene_Trandition_Delay = 2.0f;

    /// <summary>
    /// PlayerMovementコンポーネント
    /// </summary>
    [SerializeField]
    PlayerMovement Movement;

    /// <summary>
    /// Enemyのタグを持つコライダーが入ってきたらゲームオーバー画面に遷移する
    /// </summary>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == Enemy_Tag) {
            StartCoroutine(ChangeScene());
        }
    }

    /// <summary>
    /// プレイヤーの動きを止め、呼び出されてからScene_Trandition_Delay秒後にゲームオーバー画面に遷移する
    /// </summary>
    IEnumerator ChangeScene()
    {
        Movement.enabled = false;
        yield return new WaitForSeconds(Scene_Trandition_Delay);
        SceneManager.LoadScene(Game_Over_Scene_Name);
    }
}
