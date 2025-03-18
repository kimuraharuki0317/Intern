using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// オブジェクトに2点を往復させる
/// </summary>
public class MoveObject : MonoBehaviour
{
    /// <summary>
    /// 折り返し地点となるオブジェクト
    /// </summary>
    [SerializeField]
    Transform ReturnTransform;

    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField]
    float MoveSpeed;

    /// <summary>
    /// 初期の移動割合
    /// </summary>
    [SerializeField, Range(0, 1)]
    float InitRatio;

    /// <summary>
    /// 折り返し中か
    /// </summary>
    bool returning;

    /// <summary>
    /// 初期座標
    /// </summary>
    Vector3 initPosition;

    /// <summary>
    /// 折り返し地点の座標
    /// </summary>
    Vector3 returnPosition;

    /// <summary>
    /// 移動方向
    /// </summary>
    Vector3 moveVector;

    /// <summary>
    /// 折り返し地点の半径
    /// </summary>
    const float Return_Point_Range = 0.3f;

    /// <summary>
    /// 初期状態の2点の距離
    /// </summary>
    float initDistance;

    void Start()
    {
        // 初期座標、折り返し座標の決定
        initPosition = transform.position;
        returnPosition = ReturnTransform.position;

        // 初期距離の計算
        initDistance = Vector3.Magnitude(returnPosition - initPosition);

        // Ratioをもとに移動させる
        transform.position += (returnPosition - initPosition) * InitRatio;
    }

    void Update()
    {
        // 移動方向を求める
        moveVector = Vector3.Normalize(returnPosition - initPosition);

        // 折り返し中か
        if(returning) {
            // 目標地点との距離を求める
            var distance = Vector3.Magnitude(initPosition - transform.position);

            // 目標地点外か
            if (Return_Point_Range < distance) {
                // 移動させる
                transform.position -= moveVector * Time.deltaTime * MoveSpeed;
            } else {
                // 往復を切り替える
                returning = false;
            }
        }
        else {
            // 目標地点との距離を求める
            var distance = Vector3.Magnitude(returnPosition - transform.position);

            // 目標地点外か
            if (Return_Point_Range < distance) {
                // 移動させる
                transform.position += moveVector * Time.deltaTime * MoveSpeed;
            } else {
                // 往復を切り替える
                returning = true;
            }
        }
    }

    /// <summary>
    /// 移動床のコライダーがキャラクターに触れたら一緒に移動するよう子オブジェクトにする
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")) {
            // 触れたキャラクターの親をこのオブジェクトにする
            other.transform.SetParent(transform);
        }
    }

    /// <summary>
    /// 移動床のキャラクターから離れたら別々に移動するよう親子関係を断つ
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")) {
            // 離れたキャラクターの親をなくす
            other.transform.SetParent(null);

            // 慣性を乗せるために Rigidbody コンポーネントを参照
            var rb = other.gameObject.GetComponent<Rigidbody>();

            // null参照しないよう確認
            if(rb != null)
            {
                // そのときの移動方向と同じ方向に力を加える
                if (returning) {
                    rb.AddForce(-moveVector * MoveSpeed, ForceMode.Impulse);
                } else {
                    rb.AddForce(moveVector * MoveSpeed, ForceMode.Impulse);
                }
            }
        }
    }
}
