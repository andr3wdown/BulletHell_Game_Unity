using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public float camSpeed;
    float speedHolder;
    public GameObject playerPrefab;
    public Transform spawnPoint;
    public int lives = 3;
    public float respawnTime = 2f;
    public GameObject ended;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        speedHolder = camSpeed;
    }
    void FixedUpdate()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 pos = transform.position;

        pos = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, camSpeed * Time.deltaTime);
        pos.z = -10f;
        transform.position = pos;
    }
    public GameObject pwrUp;
    public void StopCam(GameObject destroyable)
    {
        anim.SetTrigger("Shake");
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        if(lives > 0)
        {
            Instantiate(pwrUp, destroyable.transform.position + transform.up, transform.rotation);
        }
        Destroy(destroyable);
        camSpeed = 0;
        StartCoroutine(Respawn(allEnemies));
    }
    public void StartCam()
    {
        camSpeed = speedHolder;
    }

    public IEnumerator Respawn(Enemy[] allEnemies)
    {
        yield return new WaitForSeconds(respawnTime);
        if(lives > 0)
        {
            Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
            lives--;
            StartCam();    
        }
        else
        {
            ended.SetActive(true);
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        StopCoroutine(Respawn(allEnemies));
    }
}