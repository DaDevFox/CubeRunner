using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class MapSection
{
    public virtual string Name { get; } = "new Map section";
    public virtual float Weight { get; } = 1f;
    public virtual int Level { get; } = 1;


    public abstract GameObject Generate(MapSectionDimensions dimensions);




    public struct MapSectionDimensions
    {
        public Vector3 position;
        public Vector3 volume;
    }
}


//public class PlainSection : MapSection
//{
//    public static int numRocks = 15;
//    public static int numRockVariance = 10;


//    public override int Level => 1;


//    public override GameObject Generate(MapSectionDimensions dimensions)
//    {
//        GameObject @base = new GameObject("Plains Section");

//        int rocks = numRocks + Random.Range(-numRockVariance, numRockVariance);

//        for(int i = 0; i < rocks; i++)
//        {
//            GameObject obj = new GameObject("boulder" + i);
//            obj.AddComponent<BoulderObject>();
//            obj.transform.SetParent(@base.transform);

//            obj.transform.position = new Vector3(
//                Random.Range(-dimensions.volume.x, dimensions.volume.x),
//                0f,
//                Random.Range(-dimensions.volume.z, dimensions.volume.z));
//        }







//        return @base;
//    }
//}







//public class CanyonSection : MapSection
//{
//    public override string Name
//    {
//        get
//        {
//            return "Canyon";
//        }
//    }

//    public override MapObject[] Generate(MapSectionDimensions dimensions)
//    {
//        return null ;



//    }
//}



