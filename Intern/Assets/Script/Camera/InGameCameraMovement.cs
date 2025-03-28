using Unity.VisualScripting;
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
    float MouseSensitivityHorizonal = 30.0f;

    /// <summary>
    /// マウス縦移動量の補正値
    /// </summary>
    [SerializeField]
    float MouseSensitivityVertical = 10.0f;

    /// <summary>
    /// プレイヤーとの距離
    /// </summary>
    [SerializeField]
    float CameraDistance = 3.0f;

    /// <summary>
    /// プレイヤーとの最小距離
    /// </summary>
    [SerializeField]
    float CameraDistanceMin = 2.0f;

    /// <summary>
    /// プレイヤーとの距離
    /// </summary>
    [SerializeField]
    float CameraDistanceMax = 7.0f;

    /// <summary>
    /// カメラ追従速度
    /// </summary>
    [SerializeField]
    float CameraFollowingSpeed = 2.5f;

    /// <summary>
    /// Y座標追跡の補正値
    /// </summary>
    [SerializeField]
    float CameraFollowingSpeedY = 30f;

    /// <summary>
    /// カメラが追跡をやめる半径
    /// </summary>
    [SerializeField] 
    float CameraStopRange = 0.5f;

    /// <summary>
    /// カメラ向き上限
    /// </summary>
    [SerializeField] 
    float CameraForwardMaximum = -0.2f;

    /// <summary>
    /// カメラ向き下限
    /// </summary>
    [SerializeField] 
    float CameraForwardMinimum = -0.7f;

    /// <summary>
    /// 生成されたInputActionのインスタンスを保持する
    /// </summary>
    InputAction mouseMoveAction;

    /// <summary>
    /// 生成されたInputActionのインスタンスを保持する
    /// </summary>
    InputAction mouseWheel;

    /// <summary>
    /// 生成されたInputSystem_Actionsのインスタンスを保持する
    /// </summary>
    InputSystem_Actions inputActions;

    /// <summary>
    /// ターゲットのY座標
    /// </summary>
    float targetPositionY;

    /// <summary>
    /// 直前のターゲットのY座標
    /// </summary>
    float targetPositionYOld;

    void Start()
    {
        // カーソル固定
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = PlayerTransform.position + Vector3.back * CameraDistance;

        // InputActionクラスを読み込む
        inputActions = new InputSystem_Actions();
        mouseMoveAction = inputActions.Player.MouseMovement;
        mouseWheel = inputActions.Player.MouseWheel;

        // 入力アクションの開始を登録
        mouseMoveAction.Enable();
        mouseWheel.Enable();
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

    void LateUpdate()
    {
        targetPositionYOld = targetPositionY;
        targetPositionY = PlayerTransform.position.y;

        if (targetPositionY != targetPositionYOld) {
            transform.position += new Vector3(0, targetPositionY - targetPositionYOld, 0);
        }
    }

    /// <summary>
    /// プレイヤーの周りを回転させる処理
    /// </summary>
    void Rotate()
    {
        // マウスの移動量を取得
        var mouseDelta = mouseMoveAction.ReadValue<Vector2>();

        // カメラ横回転
        transform.RotateAround(PlayerTransform.position, Vector3.up, mouseDelta.x * Time.deltaTime * MouseSensitivityHorizonal);

        // カメラ縦回転
        // 下、上に行き過ぎないよう制限をかける
        if (CameraForwardMinimum < transform.forward.y && transform.forward.y < CameraForwardMaximum) {
            transform.RotateAround(PlayerTransform.position, Camera.main.transform.right, mouseDelta.y * Time.deltaTime * MouseSensitivityVertical);
        } else {
            //行き過ぎていたら中央に動かして範囲内に動かす
            transform.position += new Vector3(0.0f, (transform.forward.y - (CameraForwardMinimum + CameraForwardMaximum) / 2.0f) * Time.deltaTime * CameraFollowingSpeed, 0.0f);
        }
    }

    /// <summary>
    /// プレイヤーを追従させる処理
    /// </summary>
    void Tracking()
    {
        var cameraToPlayer = PlayerTransform.position - transform.position;
        var distance = cameraToPlayer.magnitude;

        var scrollValue = mouseWheel.ReadValue<Vector2>();
        CameraDistance -= scrollValue.y;
        CameraDistance = Mathf.Min(CameraDistance, CameraDistanceMax);
        CameraDistance = Mathf.Max(CameraDistance, CameraDistanceMin);

        if (CameraDistance < distance - CameraStopRange) {
            transform.position += cameraToPlayer.normalized * Time.deltaTime * CameraFollowingSpeed;
        } else if (distance + CameraStopRange < CameraDistance) {
            transform.position -= cameraToPlayer.normalized * Time.deltaTime * CameraFollowingSpeed;
        }

        targetPositionYOld = targetPositionY;
        targetPositionY = PlayerTransform.position.y;

        if (targetPositionY != targetPositionYOld) {
            transform.position += new Vector3(0, (targetPositionY - targetPositionYOld) * Time.deltaTime * CameraFollowingSpeedY, 0);
        }
    }
}
