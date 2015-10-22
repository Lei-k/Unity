using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatHealth : MonoBehaviour {

    public float startingHealth = 100;
    public AudioClip hurtAudio;
    public float currentHealth;
    public Slider healthSlider;

    void Awake()
    {
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //由攻擊物件呼叫此函數，對角色造成傷害
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        healthSlider.value = currentHealth;

        AudioSource.PlayClipAtPoint(hurtAudio, transform.position);
    }
}
