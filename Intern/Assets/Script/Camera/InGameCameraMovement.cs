using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// カメラがプレイヤーを追従、マウスで回転する処理
/// </summary>
public class InGameCameraMovement : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのトランスフォーム
    /// </summary>
    [SerializeField]
    Transform PlayerTransform;

    /// <summary>
    /// マウス横移動量の補正値
    /// </summary>
    const float Mouse_Sensitivity_Horizonal = 100.0f;

    /// <summary>
    /// マウス縦移動量の補正値
    /// </summary>
    const float Mouse_Sensitivity_Vertical = 50.0f;

    /// <summary>
    /// プレイヤーとの距離
    /// </summary>
    const float Camera_Distance = 10.0f;

    /// <summary>
    /// カメラ追従速度
    /// </summary>
    const float Camera_Following_Speed = 3.0f;

    /// <summary>
    /// カメラ向き上限
    /// </summary>
    const float Camera_Forward_Maximum = -0.1f;

    /// <summary>
    /// カメラ向き下限
    /// </summary>
    const float Camera_Forward_Minimum = -0.9f;

    /// <summary>
    /// 生成されたInputActionのインスタンスを保持する
    /// </summary>
    InputAction mouseMoveAction;

    void Start()
    {
        Cursor.visible = false;
        transform.position = PlayerTransform.position + Vector3.back;

        // InputActionクラスを読み込む
        var inputActions = new InputSystem_Actions();
        mouseMoveAction = inputActions.Player.MouseMovement; // PlayerアクションマップとMouseMovementアクションを参照

        // 入力アクションの開始を登録
        mouseMoveAction.Enable();
    }

    void Update()
    {
        // プレイヤー注視
        transform.LookAt(PlayerTransform.position);

        // 回転処理
        Rotate();

        // 追従処理
        Tracking();
    }

    /// <summary>
    /// プレイヤーの周りを回転させる処理
    /// </summary>
    void Rotate()
    {
        // マウスの移動量を取得
        var mouseDelta = mouseMoveAction.ReadValue<Vector2>();

        // カメラ横回転
        transform.RotateAround(PlayerTransform.position, Vector3.up, mouseDelta.x * Time.deltaTime * Mouse_Sensitivity_Horizonal);

        // カメラ縦回転
        // 下、上に行き過ぎないよう制限をかける
        if (Camera_Forward_Minimum < transform.forward.y && transform.forward.y < Camera_Forward_Maximum) {
            transform.RotateAround(PlayerTransform.position, Camera.main.transform.right, mouseDelta.y * Time.deltaTime * Mouse_Sensitivity_Vertical);
        } else {
            //行き過ぎていたら中央に動かして範囲内に動かす
            transform.position += new Vector3(0.0f, (transform.forward.y - (Camera_Forward_Minimum + Camera_Forward_Maximum) / 2.0f) * Time.deltaTime * Camera_Following_Speed, 0.0f);
        }
    }

    /// <summary>
    /// プレイヤーを追従させる処理
    /// </summary>
    void Tracking()
    {
        var cameraToPlayer = PlayerTransform.position - transform.position;
        var distance = cameraToPlayer.magnitude;

        if (Camera_Distance < distance) {
            transform.position += cameraToPlayer.normalized * Time.deltaTime * Camera_Following_Speed;
        } else if (distance < Camera_Distance) {
            transform.position -= cameraToPlayer.normalized * Time.deltaTime * Camera_Following_Speed;
        }
    }
}
