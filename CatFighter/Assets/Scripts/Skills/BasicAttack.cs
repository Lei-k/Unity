using UnityEngine;
using System.Collections;

public class BasicAttack : MonoBehaviour {

    public SkillType type;             //使用哪種基本攻擊的版本，會對應到下面的MODEL1，MODEL2等方法
    public int level = 1;              //儲存BasicAttack的等級
    public float damage = 5f;          //攻擊傷害
    public float attack = 5f;          //角色攻擊力，暫時存在這邊，之後會新增角色的Data Class 來存攻擊力跟下面的防禦力等等
    public float defense = 3f;         //角色防禦力
    public Bullet bullet;              //攻擊物件，攻擊時才會被生出來的隱形物件，且一碰中到角色就給予傷害
    public AudioClip clip;             //攻擊音效
    private CatController controller;  //連結到CatController，並在需要時回呼CatController的方法
    private Animator anim;             //角色的狀態機
    private Move move;                 //角色的移動狀態
    private bool canInput = false;     //決定現在是否可以按下攻擊鍵
    private bool nextStep = false;     //決定是否做下一階段的攻擊
    public int currentState = 0;       //目前是基本攻擊的第幾號攻擊了 
    public int endState = 5;           //最後一號攻擊狀態

	void Awake()
    {
        controller = GetComponent<CatController>();
        move = GetComponent<Move>();
        anim = GetComponent<Animator>();
        move = GetComponent<Move>();
    }

    void Update()
    {
        if (canInput && SkillObserver.GetType(SkillType.BASICATTACK))
        {
            nextStep = true;
        }
    }

    //從外部引用的唯一方法，可依照本身的SkillType來判斷使用哪一個攻擊MODEL
    public void Play()
    {
        switch (type)
        {
            case SkillType.MODEL_ONE: StartCoroutine(Model1()); break;
        }
    }

    /*我們的第一個基本攻擊MODEL，在測試程式時可以多創建一兩個MODEL供測試，
      SkillType裡的MODEL不夠時只需在SkillType裡添加就好，
      如果有其他角色適用此MODEL也可以直接套用*/
    IEnumerator Model1()
    {
        // MODEL1的攻擊力函數，暫時先這樣XD
        damage = Mathf.Log((level * attack) / defense) * attack;
        anim.Play("Basic_Attack_One");
        // 這裡的語法就是執行一個協程(可被插段的程式)，至於MODEL1Fragment是啥下面會講啦~~~
        yield return StartCoroutine(Model1Fragment(0.4f, 0.2f));
        // 符合下一個攻擊操作便執行
        if (nextStep)
        {
            anim.Play("Basic_Attack_Two");
            yield return StartCoroutine(Model1Fragment(0.25f, 0.15f));
        }
        if (nextStep)
        {
            anim.Play("Basic_Attack_Three");
            yield return StartCoroutine(Model1Fragment(0.25f, 0.25f));
        }
        if (nextStep)
        {
            anim.Play("Basic_Attack_Four");
            yield return StartCoroutine(Model1Fragment(0.28f, 0.28f));
        }
        if (nextStep)
        {
            anim.Play("Basic_Attack_Five");
            yield return StartCoroutine(Model1Fragment(0.6f, 0.5f));
        }
    }

    // MODEL1的執行片段，主要負責攻擊裡會出現的重複動作，兩個Time參數就是告訴他要停個多久比較好
    IEnumerator Model1Fragment(float attackTime, float waitTime)
    {
        canInput = false;
        AudioSource.PlayClipAtPoint(clip, transform.position);
        // 生成攻擊物件
        Bullet newBullet = Instantiate(bullet);
        if (move.facingRight)
            newBullet.transform.position = transform.position + new Vector3(0.7f, 0, 0);
        else
            newBullet.transform.position = transform.position + new Vector3(-0.7f, 0, 0);
        newBullet.SetOwner(this.tag);
        newBullet.SetDemage(damage);
        currentState++;
        nextStep = false;
        yield return new WaitForSeconds(attackTime);
        // 允許玩家在一小段時間內按基本攻擊鍵
        canInput = true;
        yield return new WaitForSeconds(waitTime);
        canInput = false;
        if (!nextStep || currentState == endState)
        {
            currentState = 0;
            controller.SetCanBasicAttack(true);
        }
    }
}
