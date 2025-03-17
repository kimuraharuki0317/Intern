using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// プレイヤーの移動速度
    /// </summary>
    const float moveSpeed = 3.0f;

    /// <summary>
    /// プレイヤーのジャンプ力
    /// </summary>
    const float jumpPower = 6.0f;

    /// <summary>
    /// 接地確認コンポーネント
    /// </summary>
    CheckGround checkGround;

    /// <summary>
    /// Rigidbodyコンポーネント
    /// </summary>
    Rigidbody rb;

    void Start()
    {
        checkGround = GetComponent<CheckGround>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 移動ベクトル
        Vector3 moveVector = Vector3.zero;

        // カメラのTransform取得
        Transform cameraTransform = Camera.main.transform;

        // Wキー（前方移動）
        if (Input.GetKey(KeyCode.W)){
            moveVector += cameraTransform.forward;
        }

        // Sキー（後方移動）
        if (Input.GetKey(KeyCode.S)){
            moveVector -= cameraTransform.forward;
        }

        // Dキー（右移動）
        if (Input.GetKey(KeyCode.D)){
            moveVector += cameraTransform.right;
        }

        // Aキー（左移動）
        if (Input.GetKey(KeyCode.A)){
            moveVector -= cameraTransform.right;
        }

        if(moveVector!=Vector3.zero)
        {
            moveVector.y = 0;

            //移動方向に向ける
            transform.forward = moveVector;

            //実際に移動させる
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }


        // スペースキー押下かつ接地していたらジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && checkGround.HitGround())
        {
            rb.AddForce(jumpPower * Vector3.up, ForceMode.Impulse);
        }
    }
}
