using UnityEngine;

/*狀態機與玩家輸入的觀察者，其他腳本可以藉由此物件return的布林值，
  來判斷是否該做接下來的動作，與Input.GetKey(KeyCode.A)此種用法雷同*/
public class SkillObserver : MonoBehaviour{

    static Animator anim;
    static Move move;

    void Awake()
    {
        anim = GetComponent<Animator>();
        move = GetComponent<Move>();       
    }

    public static bool GetType(SkillType type)
    {
        switch (type)
        {
            case SkillType.MOVE:             return    !anim.GetCurrentAnimatorStateInfo(0).IsName("Cat1_Squat");
            case SkillType.JUMP:             return    Input.GetButtonDown("Jump") && move.grounded;
            case SkillType.SQUAT:            return    Input.GetButtonDown("Squat")
                                                       && !anim.GetCurrentAnimatorStateInfo(0).IsName("Cat1_Squat");
            case SkillType.SPRING:           return    Input.GetButtonDown("Spring")
                                                       && !anim.GetCurrentAnimatorStateInfo(0).IsName("Cat1_Spring");
            case SkillType.BASICATTACK:      return    Input.GetButton("BasicAttack");
            case SkillType.COUNTERATTACK:    return    Input.GetButton("CounterAttack") 
                                                       && anim.GetCurrentAnimatorStateInfo(0).IsName("Cat1_Squat") ;
        }
        return false;
    }
}