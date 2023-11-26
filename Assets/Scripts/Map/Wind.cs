using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wind : MonoBehaviour
{
    public Player player;

    public static float windPushback = 0.1f;

    public float healthDrain = 1f;
    public float streakChance = 0.8f;
    public float streakSpeed = 100f;
    public float time = 0f;
    public float increment = 0.01f;

    public static float streakDamage = 30f;


    private void Update()
    {
        if (time > increment)
        {
            
            for(int i = 0; i < (int)(time / increment); i++)
                if (Random.value < streakChance)
                    DoStreak();

            time = 0f;
        }

        time += Time.deltaTime;
    }

    private void DoStreak()
    {
        Map map = GetComponentInParent<Map>();
        
        float x = Random.Range(-map.size.x, map.size.x);
        GameObject streak = GameObject.Instantiate(Resources.Load("Particles/Wind") as GameObject);
        streak.transform.position = this.transform.position;
        streak.transform.position = new Vector3(x, streak.transform.position.y, streak.transform.position.z);
        Projectile s = streak.AddComponent<Projectile>();
        s.velocity = new Vector3(0f, 0f, -1f * streakSpeed);
        s.damage = streakDamage;

        s.knockback = windPushback;
    }


}

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Distance to move in seconds;
    /// </summary>
    public Vector3 velocity = new Vector3(0f, 0f, -100f);
    public float duration = 10f;
    public float knockback = 0.1f;
    public float damage = 30f;
    private float timeElapsed = 0f;

    private void Update()
    {
        transform.position += velocity * Time.deltaTime;

        timeElapsed += Time.deltaTime;
        if (timeElapsed > duration)
            GameObject.Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (other.GetComponent<Player>() != null)
        {
            other.GetComponent<Player>().health -= damage;
            other.transform.position += velocity * knockback;

            GameObject.Destroy(this.gameObject);
        }
    }

    



}




