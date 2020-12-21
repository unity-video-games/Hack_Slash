using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Declare the most using variables
public class LevelManager : MonoBehaviour
{
	
    public int InstanceCopyCount = 1;
    public List<Transform> particles;
    public bool WillGrow = true;
    List<GameObject> pooledObects;
    public List<AudioClip> levelClip;
    public  GameObject  PrefabInstance;
    public Transform player;
    public int levelItem =0;
    public int Score = 0;
    public static LevelManager level;
    public Transform mainCanvas;
    public int enemyCount;
    public int EnemyKill;
   // public TextMeshProUGUI txtEnemy;
    void Awake()
    {
        level = this;
        EnemyKill = 0;
    }
    void Start()
    {
		//Create gameObjects to play Sound
        pooledObects = new List<GameObject>();
        for (int i = 0;i< InstanceCopyCount; i++)
        {
            GameObject obj = Instantiate(PrefabInstance);
            obj.SetActive(false);
            pooledObects.Add(obj);
        }
    }

   public GameObject pooledInstantiate()
    {
		//Check if gameObject isn't active if not we create new one
        for (int i = 0;i < pooledObects.Count;i++) {
            if (!pooledObects[i].activeInHierarchy) {
                return pooledObects[i];
            }
            if (WillGrow) {
                GameObject obj = Instantiate(PrefabInstance);
                pooledObects.Add(obj);
                return obj;
            }
        }
            return null;
    }

    public void Play(int number)
    {
		//To play sound
        GameObject obj = pooledInstantiate();
         obj.transform.position = player.position;
        AudioSource audio =  obj.GetComponent<AudioSource>();
         obj.SetActive(true);
        audio.PlayOneShot(levelClip[number]);
        StartCoroutine(SoundFX(audio));
    }
    IEnumerator SoundFX(AudioSource audio)
    {
		//disable gameObject if sound is finished
        while (audio.isPlaying)
            yield return new WaitForSeconds(0.5f);

        audio.gameObject.SetActive(false);

    }
    

}
