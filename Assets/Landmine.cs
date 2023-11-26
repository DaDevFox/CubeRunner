using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : Enemy
{
    public float damage = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>()) 
        {
            other.GetComponent<Player>().health -= damage;
            
            
            transform.Find("Explosion").GetComponent<ParticleSystem>().Play();

            GameObject.Destroy(this.gameObject);
        }
    }
}
