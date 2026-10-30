using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    Vector3 diff;
    //public GameObject target; //カメラの対象（Player)
    public GameObject player; //カメラの対象（Player)

    public float followSpeed = 5.0f; //追従スピード


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //diff = target.transform.position - transform.position; //最初に維持すべき距離を定義
        diff = player.transform.position - transform.position; //最初に維持すべき距離を定義

    }

    //Playerが動いた(Update・ FixUpdate)後に実行される
    // Update is called once per frame
    void LateUpdate()
    {
        //現時点A→目的地Ｂに向けて収束していく
        //現時点Aの位置はどんどん詰めていくのでやがて限りなくイコールに近い距離になってくる
        //目的地には限りなく近づき続けるだけでイコールにはならないが、描画上は完全に止まっているかのように表現される

        transform.position = Vector3.Lerp(
            transform.position, //現時点Ａ
            //target.transform.position - diff, //目的地Ｂ
            player.transform.position - diff, //目的地Ｂ

            Time.deltaTime * followSpeed//A->Bへの進行に対して、どれだけ進んだ位置にいるべきかの進捗率
            );

    }
}
