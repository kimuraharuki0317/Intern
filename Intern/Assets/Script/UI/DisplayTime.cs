using TMPro;
using UnityEngine;

public class DisplayTime : MonoBehaviour
{
    /// <summary>
    /// シーン内のTimeCounterコンポーネント
    /// </summary>
    [SerializeField]
    TimeCounter TimeCounterComponent;

    /// <summary>
    /// 表示させるTMProコンポーネント
    /// </summary>
    [SerializeField]
    TMP_Text TimeText;

    /// <summary>
    /// 残り時間を取得、表示する
    /// </summary>
    void Update()
        => TimeText.text = TimeCounterComponent.GetRemainingTime().ToString();
}
