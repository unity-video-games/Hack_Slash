using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public static Load loab;
	public string next;
	int enemy;
    void Awake()
    {
        loab = this;
		enemy = LevelManager.level.EnemyKill;
    }


    void Update()
    {
        
    }
    public void start()
    {
		//when click on Play 
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
		//when click on Quit 
        Application.Quit(0);
    }
    public void Restart()
    {
		//when click on Restart 
        SceneManager.LoadScene("Menu");
    }
     public void Next()
    {
		//Going to Next level
        if (LevelManager.level.enemyCount == enemy)
            StartCoroutine(NextWay());
    }
    IEnumerator NextWay()
    {
		//Going to Next level
        yield return new WaitForSeconds(5f);
         SceneManager.LoadScene(next);
    }
	  public void Level2()
    {
		//Going to  level 2
        SceneManager.LoadScene("Game2");
    }
}
