using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// フェードイン、フェードアウトを伴う画面遷移をする
/// </summary>
public class ChangeScene : MonoBehaviour
{
    /// <summary>
    /// フェード用のImageコンポーネント
    /// </summary>
    [SerializeField]
    Image FadeImage;

    /// <summary>
    /// フェードスピード
    /// </summary>
    [SerializeField]
    float FadeSpeed = 1.0f;

    /// <summary>
    /// 次のシーンの名前
    /// </summary>
    string nextSceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //　ヒエラルキーで一番下に移動し、前面に表示される
        transform.SetAsLastSibling();
        // 次のシーン名、パネルの色をリセット
        nextSceneName = null;
        FadeImage.color = new Color(0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        var alpha = FadeImage.color.a;

        // 次のシーンが決まっていたら
        if (nextSceneName != null) {
            //フェードアウトさせ、終わり次第画面を切り替える
            FadeImage.color = new Color(0, 0, 0, alpha + Time.deltaTime * FadeSpeed);

            if (1.0f < alpha){
                SceneManager.LoadScene(nextSceneName);
            }
        } else {
            //フェードインさせ、パネルを透明にする
            if (0.0f < alpha) {
                FadeImage.color = new Color(0, 0, 0, alpha - Time.deltaTime * FadeSpeed);
            } else {
                // ヒエラルキーで一番上に移動し、一番後ろに表示される
                transform.SetAsFirstSibling();
            }
        }
    }

    /// <summary>
    /// シーンを切り替える
    /// </summary>
    /// <param name="scenename">次シーンの名前</param>
    public void Change(string scenename)
    {
        // 次のシーン名、パネルの色をリセット
        nextSceneName = scenename;

        //　ヒエラルキーで一番下に移動し、前面に表示される
        transform.SetAsLastSibling();
    }
}
