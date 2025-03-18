using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    const float Mouse_Sensitivity_Horizonal = 1000.0f;

    /// <summary>
    /// マウス縦移動量の補正値
    /// </summary>
    const float Mouse_Sensitivity_Vertical = 300.0f;

    /// <summary>
    /// プレイヤーとの距離
    /// </summary>
    const float Camera_Distance = 10.0f;

    /// <summary>
    /// カメラ追従速度
    /// </summary>
    const float Camera_Following_Speed = 3.0f;

    void Start()
    {
        Cursor.visible = false;
        transform.position = PlayerTransform.position + Vector3.back;
    }

    void Update()
    {
        // プレイヤー注視
        transform.LookAt(PlayerTransform.position);

        // カメラ横回転
        var mouseMoveX = Input.GetAxis("Mouse X");
        transform.RotateAround(PlayerTransform.position, Vector3.up, mouseMoveX * Time.deltaTime * Mouse_Sensitivity_Horizonal);

        // カメラ縦回転
        var mouseMoveY = Input.GetAxis("Mouse Y");
        
        // 下、上に行き過ぎないよう制限をかける
        if( -0.9f < transform.forward.y && transform.forward.y < -0.1f) {
            transform.RotateAround(PlayerTransform.position, Camera.main.transform.right, mouseMoveY * Time.deltaTime * Mouse_Sensitivity_Vertical);
        } else {
            //行き過ぎていたら上・下に動かして範囲内に動かす
            transform.position += new Vector3(0.0f, (0.5f + transform.forward.y) * Time.deltaTime * Camera_Following_Speed, 0.0f);
        }

        // 追従処理
        var cameraToPlayer = PlayerTransform.position - transform.position;
        var distance = cameraToPlayer.magnitude;

        if(Camera_Distance < distance) {
            transform.position += cameraToPlayer.normalized * Time.deltaTime * Camera_Following_Speed;
        } else if(distance < Camera_Distance) {
            transform.position -= cameraToPlayer.normalized * Time.deltaTime * Camera_Following_Speed;
        }
    }
}
