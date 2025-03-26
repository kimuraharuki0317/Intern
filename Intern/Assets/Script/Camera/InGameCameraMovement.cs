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
    [SerializeField]
    float Mouse_Sensitivity_Horizonal = 30.0f;

    /// <summary>
    /// マウス縦移動量の補正値
    /// </summary>
    [SerializeField]
    float Mouse_Sensitivity_Vertical = 10.0f;

    /// <summary>
    /// プレイヤーとの距離
    /// </summary>
    [SerializeField]
    float Camera_Distance = 3.0f;

    /// <summary>
    /// カメラ追従速度
    /// </summary>
    [SerializeField]
    float Camera_Following_Speed = 2.5f;

    /// <summary>
    /// カメラが追跡をやめる半径
    /// </summary>
    [SerializeField] 
    float Camera_Stop_Range = 0.5f;

    /// <summary>
    /// カメラ向き上限
    /// </summary>
    [SerializeField] 
    float Camera_Forward_Maximum = -0.2f;

    /// <summary>
    /// カメラ向き下限
    /// </summary>
    [SerializeField] 
    float Camera_Forward_Minimum = -0.7f;

    /// <summary>
    /// 生成されたInputActionのインスタンスを保持する
    /// </summary>
    InputAction mouseMoveAction;

    /// <summary>
    /// 生成されたInputSystem_Actionsのインスタンスを保持する
    /// </summary>
    InputSystem_Actions inputActions;

    void Start()
    {
        // カーソル固定
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = PlayerTransform.position + Vector3.back;

        // InputActionクラスを読み込む
        inputActions = new InputSystem_Actions();
        mouseMoveAction = inputActions.Player.MouseMovement;

        // 入力アクションの開始を登録
        mouseMoveAction.Enable();
    }

    void OnDestroy()
    {
        //インプットシステムの停止
        inputActions.Disable();
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

        if (Camera_Distance < distance - Camera_Stop_Range) {
            transform.position += cameraToPlayer.normalized * Time.deltaTime * Camera_Following_Speed;
        } else if (distance + Camera_Stop_Range < Camera_Distance) {
            transform.position -= cameraToPlayer.normalized * Time.deltaTime * Camera_Following_Speed;
        }
    }
}
