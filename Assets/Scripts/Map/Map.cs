using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    public MapSection[] sections { get; private set; }
    public Vector3 size;
    public static Map main;
    public static Vector3 Size
    {
        get
        {
            return main.size;
        }
    }
    

    private void Start()
    {
        main = this;
    }


    public void Create(int level)
    {
        // FInd the right level
        //Type[] types = AppDomain.CurrentDomain.FindAllOfType<Level>();

        //Type _t = types.First((t) => (Activator.CreateInstance(t) as Level).Index == level);
        //Level levelInst = Activator.CreateInstance(_t) as Level;

        //current = levelInst;

        //InitGroundPlane();
        //InitSections(levelInst);
        //InitWind();






    }

    private void InitGroundPlane()
    {
        // Tesselate the ground

        

        GameObject ground = 
            SceneManager.GetActiveScene().GetRootGameObjects().First(
            (g) => g.name == "Env").transform
            .Find("Ground")
            .gameObject;

        Mesh m = ground.GetComponent<MeshFilter>().mesh;

        for (int i = 0; i < m.vertices.Length; i++)
            m.vertices[i].y += UnityEngine.Random.Range(-10f, 10f);
    }

    private void InitSections(Level level)
    {
        Type[] found = AppDomain.CurrentDomain.FindAllOfType<MapSection>();
        List<MapSection> toUse = new List<MapSection>();

        foreach (Type t in found)
        {
            MapSection instance = Activator.CreateInstance(t) as MapSection;

            if (instance.Level != level.Index)
                continue;

            toUse.Add(instance);
        }

        toUse.OrderBy(
            (i) => i.Weight);

        float size = level.Size.z;

        GameObject container = new GameObject("Level Container");
        container.transform.SetParent(transform);


        for(int i = 0; i < toUse.Count; i++)
        {
            MapSection s = toUse[i];
            GameObject obj = 
                s.Generate(new MapSection.MapSectionDimensions()
            {
                position = new Vector3(0, 0, i / size),
                volume = new Vector3(level.Size.x, level.Size.y, size / toUse.Count)
            });

            obj.transform.SetParent(container.transform);
        }

        GamePhysics.TrackAll();
    }

    private void InitWind()
    {
        
    }




}

