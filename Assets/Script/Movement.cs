using TMPro;                 //Add TextMeshPro
using UnityEngine;           //Unity libraries
using UnityEngine.UI;        //Ui Elements
using System.Collections;    //Collections


public class Movement : MonoBehaviour      //extends MonoBehavior class
{
    public static Movement move;     //Movement objet
    [SerializeField]                 //for showing elemets in inspector
    float speed;
    [SerializeField]
    float gravity;
    [SerializeField]
    float Jump;
    float vertical;
    float horizontal;
    float verticalVelocity;
    Vector3 moveDirection = Vector3.zero;      //player position
    CharacterController controller;           //collider
    Transform mainCam;
    float angle;           //camera angle
    Animator anim;
    float Run;
    BoxCollider box;         
    CharacterStats stats;     //script health
   
    GameObject effect1;     //effects of 3 hit
    GameObject effect2;      //effects of 3 hit

    /**
    * Set This script to player during begnnig + disable effects
    *
    */
    void Awake()
    {
        move = this;
        effect1 = GameObject.FindWithTag("effect1");
        effect1.SetActive(false);
        effect2 = GameObject.FindWithTag("effect2");
        effect2.SetActive(false);
    }

     /**
     * set values when Start game
     *
     */
    void Start()
    {
        LevelManager.level.levelItem = 0;     //hints power
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        mainCam = Camera.main.transform;
        box = transform.Find("box").GetComponent<BoxCollider>();
        stats = GetComponent<CharacterStats>();

       
    }

    /**
    *  Reapeat depends on frames in every seconds
    *
    */
    void Update()
    {
        if (!stats.over) {
        if (Input.GetMouseButton(0))   //mouse right click = ,left=0
            {
            anim.SetTrigger("Attack");

                if (BigHit())
                {
                    StartCoroutine(waitUnitul());
                }
            }
        Run = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;   //escape when click on 
        vertical = Input.GetAxis("Vertical"); //get keeyboard arrow up=1 down = -1 
			horizontal = Input.GetAxis("Horizontal"); //get keeyboard arrow right=1 left = -1

        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            verticalVelocity = Jump * Time.deltaTime;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        
               moveDirection = new Vector3(horizontal, 0, vertical);

            anim.SetFloat("Speed",Mathf.Clamp(moveDirection.magnitude, 0,0.5f) + ( Run == 2 && moveDirection.magnitude > 0.01f ?  0.5f : 0));

            if (moveDirection.magnitude > 0.01f)
            {
                angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
            moveDirection = mainCam.TransformDirection(moveDirection);
        moveDirection.y = verticalVelocity;
        controller.Move(moveDirection * Time.deltaTime * speed * Run);
        }
    }
    public void enableBox()
    {
        box.enabled = true;
 
    }
   public bool End()
    {
        if (stats.currentHealth <= 0) {
            anim.SetTrigger("Die");
            return true;
        }
        return false;
    }
    //public void Endo()
    //{
    //    if (stats.currentHealth <= 0)
    //    {
    //        anim.SetTrigger("Die");
           
    //    }
        
    //}


    /**
    *  Detect if user hits player otherwise disable it
    *
    */
    public  void disableBox()
    {
        box.enabled = false;
    }

    /**
    *  make player hack with hearts from enemys
    * 
    */
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heart")) {
            Instantiate(LevelManager.level.particles[1], other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
          //  print("maxHealth "+ stats.maxHealth);
           // print("currentHealth "+ stats.currentHealth);
            //if (stats.currentHealth < stats.maxHealth)
            //{
        stats.changeHealth(5);
            //       }
            if (stats.currentHealth > stats.maxHealth)
            {
                stats.currentHealth = stats.maxHealth;
          //      print("currentHealth " + stats.currentHealth);
            }
            stats.refreshHealth();


            LevelManager.level.Play(0);
       }  else if (other.CompareTag("6wrni"))
        {
          //  level++;
            Instantiate(LevelManager.level.particles[0],other.transform.position,other.transform.rotation);
            LevelManager.level.Play(1);
            Destroy(other.gameObject);
            LevelManager.level.levelItem++;
            LevelManager.level.mainCanvas.Find("PnlStats").Find("6wrni").GetComponent<TextMeshProUGUI>().text = LevelManager.level.levelItem +"/3";
          //  print(LevelManager.level.levelItem);
            if (LevelManager.level.levelItem == 3)
            {
                effect1.SetActive(true);
                LevelManager.level.mainCanvas.Find("PnlStats").Find("6wrni").GetComponent<TextMeshProUGUI>().color = Color.red;
                LevelManager.level.mainCanvas.Find("PnlStats").Find("Image").GetComponent<Image>().color = Color.red;

            }
           

        }

    }
    /**
    * make hints triangle white after using it
    *
    */
    IEnumerator waitUnitul()
    {
      //  stats.power = 20;
    
       
        yield return new WaitForSeconds(3f); //delay for 3s to make /_\ white
        LevelManager.level.mainCanvas.Find("PnlStats").Find("6wrni").GetComponent<TextMeshProUGUI>().color = Color.white;
        LevelManager.level.mainCanvas.Find("PnlStats").Find("Image").GetComponent<Image>().color = Color.white;
        LevelManager.level.levelItem = 0;
    }

   /**
   *  Check if the three tokens are taken /_\
   *
   */
    bool BigHit()
    {
           
        switch (LevelManager.level.levelItem)
        {
            case 3:
               
                LevelManager.level.mainCanvas.Find("PnlStats").Find("6wrni").GetComponent<TextMeshProUGUI>().text = "0/3";
                effect1.SetActive(false);
                effect2.SetActive(true);
                return true;
            default:
                stats.power = 7;
                return false;
        }
       
    }
   



}
