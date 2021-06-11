using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public List<GameObject> bullets = new List<GameObject>();
    public GameObject bullet;
    public int bulletNumber;
    public float secondsUntilNextBomb = 3f;

    private void Start()
    {
        GameObject tempBullet;

        for(int i = 0; i < bulletNumber; i++)
        {
            tempBullet = Instantiate(bullet);
            tempBullet.SetActive(false);
            bullets.Add(tempBullet);
        }
        StartCoroutine(DropTheNuke());
    }

    private IEnumerator DropTheNuke()
    {
        while(true)
        {
            if(GetIdleBullet() != null)
            {
                GetIdleBullet().GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetIdleBullet().transform.position = transform.position;
                GetIdleBullet().SetActive(true);
                yield return new WaitForSeconds(secondsUntilNextBomb);
            }

            yield return null;
        }
    }

    private GameObject GetIdleBullet()
    {
        for (int i = 0; i < bulletNumber; i++)
        {
            if(!bullets[i].activeInHierarchy)
            {
                return bullets[i];
            }
        }

        return null;
    }

    private void Update()
    {
        
    }
}
