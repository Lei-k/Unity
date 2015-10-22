using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;
    public Slider playerHealth;
    public Slider enemyHealth;

	// Use this for initialization
	void Start () {
        player = (GameObject)Instantiate(player, new Vector3(0.76f, 0.84f, -0.5f), Quaternion.identity);
        enemy = (GameObject)Instantiate(enemy, new Vector3(8f, 0.84f, -0.5f), Quaternion.identity);
        player.GetComponent<CatController>().enabled = false;
        player.GetComponent<BasicAttack>().enabled = false;
        player.GetComponent<CatHealth>().healthSlider = playerHealth;
        enemy.GetComponent<CatHealth>().healthSlider = enemyHealth;
        enemy.GetComponent<Move>().Flip();
        enemy.GetComponent<Move>().facingRight = false;
        player.tag = "Player";
        enemy.tag = "Enemy";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
