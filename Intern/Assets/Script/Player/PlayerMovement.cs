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
    /// 移動エフェクト
    /// </summary>
    [SerializeField]
    ParticleSystem MoveParticle;

    /// <summary>
    /// 空中にいるときのトレイル
    /// </summary>
    [SerializeField]
    TrailRenderer Trail;

    /// <summary>
    /// 接地しているか
    /// </summary>
    bool touchGround;

    /// <summary>
    /// 直前フレームに接地していたか
    /// </summary>
    bool touchGroundOld;

    /// <summary>
    /// ジャンプ、着地時のエフェクト
    /// </summary>
    [SerializeField]
    GameObject JumpEffect;

    /// <summary>
    /// ジャンプ、着地時のエフェクトの回転
    /// </summary>
    [SerializeField]
    Vector3 JumpEffectRotaion;

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
        touchGroundOld = touchGround;
        touchGround = CheckGround.GetHitGround();

        MoveAxis();
        MoveSky();

        // 接地、ジャンプ時にエフェクトを生成
        if (touchGround != touchGroundOld) {
            var effect = Instantiate(JumpEffect, transform.position, Quaternion.identity);
            effect.transform.rotation = Quaternion.Euler(JumpEffectRotaion);
        }
    }

    /// <summary>
    /// 地上の移動を行う
    /// </summary>
    void MoveAxis()
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
            var speed = Input.GetKey(KeyCode.LeftShift) ? MoveSpeed * DashSpeed : MoveSpeed;

            // 実際に移動させる
            transform.position += transform.forward * speed * Time.deltaTime;

            if (!MoveParticle.isPlaying && touchGround) {
                MoveParticle.Play(true);
            }
        }

        // 空中にいるか動いていない場合はパーティクル生成停止
        if (!touchGround || moveVector == Vector3.zero) {
            MoveParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    /// <summary>
    /// 空中の移動を行う
    /// </summary>
    void MoveSky()
    {
        // スペースキー押下かつ接地していたらジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && touchGround) {
            Rb.AddForce(JumpPower * Vector3.up, ForceMode.Impulse);
        }

        // 空中にいるかに応じてトレイルを切り替える
        Trail.emitting = !touchGround;
    }
}
