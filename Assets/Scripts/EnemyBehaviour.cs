using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Vector3 _destination;
    public float distance;
    public float speed;

    // Start is called before the first frame update
    void Awake()
    {
        _destination = new Vector3(transform.localPosition.x + distance, transform.localPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Vector3.Distance(transform.localPosition, _destination) < .1f)
            {
                _destination = new Vector3(-_destination.x, transform.localPosition.y);
            }

        transform.localPosition = Vector3.Lerp(transform.localPosition, _destination, speed * Time.deltaTime);
    }
}
