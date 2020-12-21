using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class CharacterStats : MonoBehaviour
{
    public float maxHealth = 100;
    public float power = 10;
    public bool over = false;
   public GameObject heart;
    bool can = true;
    TextMeshProUGUI txtEnemy;

    public float currentHealth { get;  set; }
    void Start()
    {
        LevelManager.level.Score = 0;
        currentHealth = maxHealth;
        txtEnemy = GameObject.FindGameObjectWithTag("txtEnemy").GetComponent<TextMeshProUGUI>();
    }

    public void changeHealth(float value )
    {
		//add or minus for player and enemy

        currentHealth += value;

        Mathf.Clamp(currentHealth , 0, maxHealth);

        if (transform.CompareTag("Enemy")) { 
              transform.Find("Canvas").GetChild(1).GetComponent<Image>().fillAmount  = currentHealth / maxHealth;
        

        }
        else if (transform.CompareTag("Player"))
        {

            refreshHealth();
            LevelManager.level.mainCanvas.Find("PnlStats").Find("score").GetComponent<TextMeshProUGUI>().text = LevelManager.level.Score +"";
            
        }
        //PnlStats
        if (currentHealth <= 0)
        {
           
            Die();
        }
    }

    public void Die()
    {
		//check if player is dead and add score when enemy is dead
        if (transform.CompareTag("Player"))
        {
            //Game Over
            StartCoroutine(on());
            over = true;
     
        }
         else if (transform.CompareTag("Enemy"))
        {
      
            if (can)
            {
                txtEnemy.text = "" + --LevelManager.level.enemyCount;
            
                can = false;
                GameObject bb = Instantiate(heart);
                bb.transform.position = transform.position;
            }
           

         }
    
    }
    IEnumerator on()
    {
		//Show Game Over
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOver");
    }
    public void refreshHealth()
    {
		//refresh player Health
        LevelManager.level.mainCanvas.Find("PnlStats").Find("fillAmount").GetComponent<Image>().fillAmount = currentHealth / maxHealth;
        if (currentHealth > 0)
            LevelManager.level.mainCanvas.Find("PnlStats").Find("Health").GetComponent<TextMeshProUGUI>().text = (currentHealth / maxHealth * 100) + " %";
        else
            LevelManager.level.mainCanvas.Find("PnlStats").Find("Health").GetComponent<TextMeshProUGUI>().text = "0 %";
    }
   
}
