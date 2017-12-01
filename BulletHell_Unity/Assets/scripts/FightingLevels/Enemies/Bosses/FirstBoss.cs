using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirstBoss : Enemy
{
    public enum ShootMode
    {
        flower,
        rotator,
        burst,
        none, 
        move
    }
    public ShootMode shootMode = ShootMode.none;
    public int modeIndex;
    int prevIndex;

    public GameObject victory;

    public Transform[] targets;
    int targetIndex = 0;

    public Transform target;
    public GameObject bullet;
    public Transform frontBarrel;
    public Transform
        rightBarrel,
        leftBarrel;

    float currentFrontRot = 0;
    bool dir = false;
    public Cooldown flower;
    public Cooldown leftRotator;
    public Cooldown rightRotator;
    public Cooldown rotatorSecondaries;
    public Cooldown burstCooldown;
    public LongCooldown modeCooldown;
    Camera bossBulletCam;
    float rotatorSpeed = 20;
    bool wasShooting = false;

    private void OnDestroy()
    {
        if(victory != null)
        victory.SetActive(true);
    }
    private void Start()
    {     
        bossBulletCam = GetComponentInChildren<Camera>();
    }
    void Update ()
    {
        if (isActive)
        {
            //bossBulletCam.enabled = true;
            if(Camera.main.GetComponent<CameraController>().camSpeed != 0)
            {
                Camera.main.GetComponent<CameraController>().camSpeed = 0;
            }
            modeCooldown.cooldown -= Time.deltaTime;

            if(modeCooldown.cooldown <= 0)
            {
                prevIndex = modeIndex;
                modeCooldown.cooldown = modeCooldown.delay;
                shootMode = GetMode();
                
            }


            switch (shootMode)
            {
                case ShootMode.flower:
                    if(targetIndex == 0)
                    {
                        targetIndex = Random.Range(0, targets.Length);
                    }

                    if(transform.position == targets[targetIndex].position)
                    FlowerPattern(12, 60f, 50f);

                    break;

                case ShootMode.rotator:
                    targetIndex = 0;

                    if(transform.position == targets[targetIndex].position)
                    UseRotatorPattern();

                    break;

                case ShootMode.burst:
                    if(transform.position == targets[targetIndex].position)
                    {
                        targetIndex = Random.Range(0, targets.Length);
                    }

                    BurstPattern(12);
                    break;

                case ShootMode.none:
                    if (transform.position == targets[targetIndex].position)
                    {
                        targetIndex = Random.Range(0, targets.Length);
                    }
                    RotatingSecondaries(12, 90f);
                    break;

                case ShootMode.move:
                    if (transform.position == targets[targetIndex].position)
                    {
                        targetIndex = Random.Range(0, targets.Length);
                    }
                    break;
            }
            transform.position = Vector3.MoveTowards(transform.position, targets[targetIndex].position, speed * Time.deltaTime);
           
           

        }
        else
        {
            //bossBulletCam.enabled = false;
        }
        
	}
    ShootMode GetMode()
    {
        if (!wasShooting)
        {
            speed = 2;
            while (modeIndex == prevIndex)
            {
                modeIndex = Random.Range(0, 4);
            }

            switch (modeIndex)
            {
                case 0:
                    wasShooting = true;
                    return ShootMode.burst;

                case 1:
                    wasShooting = true;
                    return ShootMode.flower;

                case 2:
                    wasShooting = true;
                    return ShootMode.rotator;

                case 3:
                    wasShooting = true;
                    return ShootMode.none;

                default:
                    wasShooting = true;
                    Debug.LogError("Invalid targetIndex");
                    return ShootMode.none;
            }
        }
        else
        {
            speed = 1;
            wasShooting = false;
            modeCooldown.cooldown = 5f;
            return ShootMode.move;
        }
        
        
    }
    void UseRotatorPattern()
    {
        rotatorSpeed = RotateBarrel(rotatorSpeed, 10, 20);
        RotatingPattern(rightBarrel, 6, rotatorSpeed, rightRotator);
        RotatingPattern(leftBarrel, 6, rotatorSpeed, leftRotator);
        RotatingSecondaries(7, 90f);
    }

    void FlowerPattern(int shotCount, float rotSpeed, float rotBounds)
    {
        currentFrontRot = RotateBarrel(currentFrontRot, rotSpeed, rotBounds);
        Quaternion barrelRot = Quaternion.Euler(0,0,currentFrontRot);

        frontBarrel.rotation = barrelRot;

        flower.cooldown -= Time.deltaTime;
        if(flower.cooldown <= 0)
        {
            float angleMult = 360f / (float)shotCount;

            for(int i = 1; i < shotCount +1; i++)
            {
                Quaternion rot = Quaternion.Euler(0,0,frontBarrel.rotation.eulerAngles.z + angleMult * i);

                GameObject go = SpawnBullet(frontBarrel.position, rot, bulletPool.basicBullets);


                //Instantiate(bullet, frontBarrel.position, rot);
            }
            flower.cooldown = flower.delay;
        }
    }
    void RotatingSecondaries(int secondaryCount, float secondayArc)
    {
        frontBarrel.transform.localRotation = Quaternion.identity;
        rotatorSecondaries.cooldown -= Time.deltaTime;
        if (rotatorSecondaries.cooldown <= 0)
        {
            int index = -secondaryCount / 2;
            float rotaScaler = secondayArc / secondaryCount;
            for (int i = 0; i < secondaryCount; i++)
            {
                Quaternion rota = Quaternion.Euler(0, 0, frontBarrel.rotation.eulerAngles.z + rotaScaler * index);
                GameObject Go = SpawnBullet(frontBarrel.position, rota, bulletPool.basicBullets);
                index++;
            }
            rotatorSecondaries.cooldown = rotatorSecondaries.delay;
        }
    }
    int rotIndex;
    void RotatingPattern(Transform barrel, int shotCount, float rotSpeed, Cooldown cooldown)
    {
        cooldown.cooldown -= Time.deltaTime;
        if (cooldown.cooldown <= 0)
        {
            GameObject go = new GameObject();
            go.transform.position = barrel.position;           
            Rotator newRotatorObj = go.AddComponent<Rotator>();
            newRotatorObj.rotSpeed = rotSpeed;
            newRotatorObj.index = rotIndex;
            

            float angleMult = 360f / shotCount;
            for(int i = 1; i < shotCount + 1; i++)
            {
                Quaternion rot = Quaternion.Euler(0, 0, barrel.rotation.eulerAngles.z + angleMult * i);
                GameObject newGo = SpawnBullet(barrel.position, rot, bulletPool.basicBullets);
                newGo.transform.parent = go.transform;
            }
            rotIndex++;
          

            cooldown.cooldown = cooldown.delay;       
         }

        
    }
    void BurstPattern(int shotCount)
    {
        burstCooldown.cooldown -= Time.deltaTime;
        if(burstCooldown.cooldown <= 0)
        {
            float angleMult = 360f / (float)shotCount;
            for (int i = 1; i < shotCount + 1; i++)
            {
                Quaternion rot = Quaternion.Euler(0, 0, angleMult * i);
                SpawnBullet(frontBarrel.position, rot, bulletPool.ringBullets);
            }
            burstCooldown.cooldown = burstCooldown.delay;
        }

        

    }
    float RotateBarrel(float value, float ratio, float bounds)
    {
        if (dir)
        {
            value -= Time.deltaTime * ratio;
            if(value <= -bounds)
            {
                dir = false;
            }
        }
        else
        {
            value += Time.deltaTime * ratio;
            if (value >= bounds)
            {
                dir = true;
            }
        }

        return value;
    }
}

[System.Serializable]
public class Cooldown
{
    [HideInInspector]
    public float cooldown;
    [Range(0.001f, 2.000f)]
    public float delay;
}

[System.Serializable]
public class LongCooldown
{
    [HideInInspector]
    public float cooldown;
    [Range(1.000f, 20.000f)]
    public float delay;
}
