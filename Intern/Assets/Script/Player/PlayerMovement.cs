using UnityEngine;

/// <summary>
/// プレイヤーを動かす機能を有するクラスです。
/// WASDで移動、スペースキーでジャンプします。
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// プレイヤーの移動速度
    /// </summary>
    [SerializeField]
    float MoveSpeed = 1.5f;

    /// <summary>
    /// ダッシュ時の速度倍率
    /// </summary>
    [SerializeField]
    float DashSpeed = 1.5f;

    /// <summary>
    /// プレイヤーのジャンプ力
    /// </summary>
    [SerializeField]
    float JumpPower = 5.0f;

    /// <summary>
    /// 接地確認コンポーネント
    /// </summary>
    [SerializeField] 
    CheckGround CheckGround;

    /// <summary>
    /// Rigidbodyコンポーネント
    /// </summary>
    [SerializeField] 
    Rigidbody Rb;

    void Update()
    {
        // 移動ベクトル
        var moveVector = Vector3.zero;

        // カメラのTransform取得
        var cameraTransform = Camera.main.transform;

        // Wキー（前方移動）
        if (Input.GetKey(KeyCode.W)) {
            moveVector += cameraTransform.forward;
        }

        // Sキー（後方移動）
        if (Input.GetKey(KeyCode.S)) {
            moveVector -= cameraTransform.forward;
        }

        // Dキー（右移動）
        if (Input.GetKey(KeyCode.D)) {
            moveVector += cameraTransform.right;
        }

        // Aキー（左移動）
        if (Input.GetKey(KeyCode.A)) {
            moveVector -= cameraTransform.right;
        }

        if (moveVector != Vector3.zero) {

            // Y 成分を 0 にする
            moveVector.y = 0;

            // 移動方向に向ける
            transform.forward = moveVector;

            // ダッシュ計算
            var speed = Input.GetKey(KeyCode.LeftShift) ? Dash_Speed * Move_Speed : Move_Speed;

            // 実際に移動させる
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        // スペースキー押下かつ接地していたらジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround.GetHitGround()) {
            Rb.AddForce(Jump_Power * Vector3.up, ForceMode.Impulse);
        }
    }
}
