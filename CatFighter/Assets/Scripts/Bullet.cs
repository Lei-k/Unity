using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    private float damage = 0;
    private string owner = "";

    void Awak()
    {
        
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(DisapperTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 當偵測到碰撞時，取得對方物件，並造成傷害
    void OnTriggerEnter2D(Collider2D other)
    {

        Destroy(this.gameObject);

        if(!this.owner.Equals(other.tag))
            other.gameObject.GetComponent<CatHealth>().TakeDamage(damage);
    }

    public void SetDemage(float damage)
    {
        this.damage = damage;
    }

    public void SetOwner(string owner)
    {
        this.owner = owner;
    }

    // 攻擊物件過0.5秒後將會被摧毀
    IEnumerator DisapperTimer()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}