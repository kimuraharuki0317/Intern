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
    const float Move_Speed = 3.0f;

    /// <summary>
    /// プレイヤーのジャンプ力
    /// </summary>
    const float Jump_Power = 6.0f;

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

            // 実際に移動させる
            transform.position += transform.forward * Move_Speed * Time.deltaTime;
        }

        // スペースキー押下かつ接地していたらジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround.GetHitGround()) {
            Rb.AddForce(Jump_Power * Vector3.up, ForceMode.Impulse);
        }
    }
}
