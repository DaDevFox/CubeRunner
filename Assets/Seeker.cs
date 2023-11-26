using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float health;
}

public class Seeker : Enemy
{
    [SerializeField]
    private Player player;

    public float damage = 100f;

    public float smoothing = 2f;
    public float speed = 1f;
    public Vector3 desiredPosition;

    public float delay = 2f;
    private float time = 0f;
    public bool active = false;

    private void Update()
    {
        if (time > delay)
            active = true;

        if (active)
        {
            desiredPosition = player.transform.position;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * speed * smoothing);
        }
        

        transform.LookAt(player.transform);
        time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
            other.GetComponent<Player>().health -= damage;
    }

}
