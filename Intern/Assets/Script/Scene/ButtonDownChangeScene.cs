using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// ボタン押下により、シーン遷移を行うクラス
/// </summary>
public class ButtonDownChangeScene : MonoBehaviour
{
    /// <summary>
    /// シーンの名前
    /// </summary>
    [SerializeField]
    string SceneName;

    /// <summary>
    /// ChangeSceneコンポーネント
    /// </summary>
    [SerializeField]
    ChangeScene ChangeSceneComponent;

    void Start()
    {
        // カーソル非表示状態になっていたら表示させる
        Cursor.visible = true;
    }

    /// <summary>
    /// ボタンが押されたときに呼び出す。シーン遷移処理
    /// </summary>
    public void ChangeScene()
    {
        ChangeSceneComponent.Change(SceneName);
    }

    /// <summary>
    /// ボタンが押されたときに呼び出す。ゲーム終了処理
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif 
    }
}
