using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public Movement move;
    public void Allow()
    {
		//enable player trrigger when player hit sword
        move.enableBox();
        StartCoroutine(Decline());
    }
    private IEnumerator Decline()
    {
		//disable player trrigger  when player hit sword after 0.25 second
        yield return new WaitForSeconds(0.25f);
        move.disableBox();
    }
    public void damagePlayer()
    {
		//Decrease player health when the attack animation starts of enemy
        transform.GetComponentInParent<Enemy>().playerDamage();
     
    }
     
 
 
    public void Die_Sound()
    {
		//when enemy die
        LevelManager.level.Play(4);

    }
    public void Fight_Sound()
    {
		//Sword sound
        LevelManager.level.Play(5);

    }
}
