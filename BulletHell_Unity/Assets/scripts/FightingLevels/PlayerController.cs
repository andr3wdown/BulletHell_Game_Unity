using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    VirtualJoystick joystick;
    public float hp;
    public const float maxHp = 10f;
    public float speed;
    public float shootDelay;
    float cooldown;
    bool isBreaking;
    public int shotCount;
    public float spreadMult;
    float camSpeed;
    public float spreadBounds = 8f;
    public static PlayerController instance;
    public float invicibility = 3f;
    public float spreadChange;
    bool dirOfSreadDiffrentiation = false;
    Animator anim;
    Collider2D col;
	// Use this for initialization
	void Start ()
    {
        col = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        instance = this;
        camSpeed = Camera.main.GetComponent<CameraController>().camSpeed;
        joystick = FindObjectOfType<VirtualJoystick>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        if(instance != this)
        {
            Destroy(gameObject);
        }
        Move();

        if (invicibility <= 0)
        {
            col.enabled = true;
            anim.SetBool("Inv", false);
            
            if (!isBreaking)
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                {
                    Shoot();
                    cooldown = shootDelay;
                }
                spreadMult = SpreadDifferntiator(spreadMult, spreadBounds, spreadChange);
            }
        }
        else
        {
            col.enabled = false;
            anim.SetBool("Inv", true);
            invicibility -= Time.deltaTime;

        }
    
	}

    void Move()
    {
        Vector3 pos = transform.position;

        //pos = Vector3.MoveTowards(transform.position, transform.position + joystick.GetInput(), speed * Time.deltaTime);

/*#if UNITY_ANDROID
        if(Input.touches.Length > 0)
        {
            Vector3 targPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            pos = Vector3.MoveTowards(transform.position, targPos, speed * Time.deltaTime);
        }
        else
        {
             pos = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, camSpeed * Time.deltaTime);
        }

#endif*/

//#if UNITY_STANDALONE
        Vector3 targPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = Vector3.MoveTowards(transform.position, targPos, speed * Time.deltaTime);
//#endif

        pos = ScreenClamping(pos);

        transform.position = pos;
    }

    Vector3 ScreenClamping(Vector3 pos)
    {
        pos.y = Mathf.Clamp(pos.y, -Camera.main.orthographicSize + Camera.main.transform.position.y + 0.15f, Camera.main.orthographicSize + Camera.main.transform.position.y - 0.15f);
        float xOrtho = (float)Screen.width / (float)Screen.height;
        pos.x = Mathf.Clamp(pos.x, -Camera.main.orthographicSize * xOrtho + 0.15f, Camera.main.orthographicSize * xOrtho - 0.15f);
        pos.z = 0;

        return pos;
    }
    public Transform
        barrel1,
        barrel2;

    public GameObject[] bullets;
    public GameObject rocketLaunchers;
    void Shoot()
    {
        for(int i = 0; i < shotCount; i++)
        {
            switch (i)
            {
                case 0:
                    Instantiate(bullets[0], barrel1.position, barrel1.rotation);
                    break;
                case 1:
                    if (!rocketLaunchers.activeSelf)
                    {
                        rocketLaunchers.SetActive(true);
                    }
                    break;

                default:
                    Quaternion barrelRot = barrel1.rotation;
                    barrelRot = Quaternion.Euler(0, 0, -spreadMult * (i - 1f));
                    Instantiate(bullets[0], barrel1.position, barrelRot);
                    break;
            }
            
        }
        for (int i = 0; i < shotCount; i++)
        {
            switch (i)
            {
                case 0:
                    Instantiate(bullets[0], barrel2.position, barrel2.rotation);
                    break;

                case 1:
                    if (!rocketLaunchers.activeSelf)
                    {
                        rocketLaunchers.SetActive(true);
                    }
                    break;

                default:
                    Quaternion barrelRot = barrel2.rotation;
                    barrelRot = Quaternion.Euler(0, 0, spreadMult * (i - 1f));
                    Instantiate(bullets[0], barrel2.position, barrelRot);
                    break;
            }
        }
    }
    float SpreadDifferntiator(float spread, float bounds, float rate)
    {
        if (dirOfSreadDiffrentiation)
        {
            spread -= Time.deltaTime * rate;

            if(spread <= -bounds)
            {
                dirOfSreadDiffrentiation = false;
            }
        }
        else
        {
            spread += Time.deltaTime * rate;

            if (spread >= bounds)
            {
                dirOfSreadDiffrentiation = true;
            }
        }

        return spread;
    }
    public void GiveHp(float hpToGive)
    {
        hp += hpToGive;
        if(hp >= maxHp)
        {
            hp = maxHp;
        }
    }
    public void TakeHp(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Camera.main.SendMessage("StopCam", this.gameObject);
        }
    }

    private void OnDestroy()
    {
        EnemyBullet[] allactiveBullets = FindObjectsOfType<EnemyBullet>();
        for(int i = 0; i < allactiveBullets.Length; i++)
        {
            allactiveBullets[i].Deactivate();
        }
    }
}
