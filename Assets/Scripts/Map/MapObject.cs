using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public string title = "";
    public string description = "";

    public bool windCoverage = false;
    public bool blowable = true;

    public bool healEffect;
    public bool damageEffect;

    public Collider _collider;
    public Collider areaOfEffect;

    public float blownSpeed = 5f;


    public void Blow()
    {
        Debug.Log($"{name} got blown!!");
        this.gameObject.AddComponent<Projectile>().velocity = new Vector3(0f, 0f, -1f) * blownSpeed;
    }

    private void Update()
    {
        if (healEffect)
        {
            
        }
    }



}


//public class BoulderObject : MapObject
//{
//    public static int boulderCount = 2;
    
//    private void Start()
//    {
//        Debug.Log($"Models/Boulder{UnityEngine.Random.Range(0, boulderCount)}.obj");
//        GameObject obj = GameObject.Instantiate(Resources.Load($"Models/Boulder{UnityEngine.Random.Range(0, boulderCount)}") as GameObject);
//        obj.AddComponent<MeshCollider>().sharedMesh = obj.transform.GetChild(0).GetComponent<MeshFilter>().mesh;
//        obj.transform.SetParent(this.transform, false);
//    }
//}




