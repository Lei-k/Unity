using UnityEngine;
using System.Collections;

/*Controller做為一個操作者腳本的基本原則就是從各個物件取得資料後，
  吩咐需要執行的物件去做事就沒他的事啦~~~~XD這就是所謂的管理階層，爽~~~*/
public class CatController : MonoBehaviour
{
    Move move;
    [HideInInspector]
    public bool jump = false;
    //[HideInInspector]
    public bool canBasicAttack = true;
    BasicAttack basicAttack;

    //  Awake method 初始物件的狀態與變數
    void Awake()
    {
        move = GetComponent<Move>();
        basicAttack = GetComponent<BasicAttack>();
    }

    // 更新每一偵的狀態
    void Update()
    {
        // 偵測角色是否要跳躍，需要包在Update裡使每一偵都能偵測到
        if (SkillObserver.GetType(SkillType.JUMP))
            jump = true;
    }

    // 更新角色狀態
    void FixedUpdate()
    {
        // 取得玩家的水平Input，AD或方向鍵皆可，負值為向左移，正值則向右
        float h = Input.GetAxis("Horizontal");
        // 移動角色
        if (SkillObserver.GetType(SkillType.MOVE))
            move.Forward(h);
        // 角色跳躍
        if (jump)
        {
            move.Jump();
            jump = false;
        }
        // 基本攻擊
        if (SkillObserver.GetType(SkillType.BASICATTACK) && canBasicAttack)
        {
            canBasicAttack = false;
            basicAttack.Play();
        }
    }

    // 可做基本攻擊的回乎含式
    public void SetCanBasicAttack(bool canBasicAttack)
    {
        this.canBasicAttack = canBasicAttack;
    }
}
