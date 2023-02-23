using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{

    Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;
    private Color[] colors;

    public int SizeX = 100;
    public int SizeZ = 400;

    [SerializeField] private Gradient gradient;
    public static int spawned = 0;
    private int depth = 14;
    
    [SerializeField] private float startFall = 0.65f;

    private float minHeight;
    private float maxHeight;

    public GameObject[] objects;
    public GameObject MyObstacle;
    public GameObject land;

    private float last;
    void Start()
    {
        
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
        
    }
    
   
    void CreateShape()
    {
       
        vertices = new Vector3[(SizeX +1 ) * (SizeZ + 1)];
        float [,] noiseMap = FallOffMap(startFall,1f);
        int i = 0;
        for (int z = 0; z <= SizeZ ; z++){
            for( int x = 0; x <=SizeX; x++ )
            {
                float xOffSet = Random.Range (0f,0.04f);
                float yOffSet = Random.Range (0f,0.04f);
                
                float height = Mathf.PerlinNoise(x  * 0.05f+ xOffSet ,z * 0.05f +yOffSet ) * depth * noiseMap[x,z];
                if (System.Math.Abs(z - 200) < 2) height -= 0.5f;
                vertices[i] = new Vector3(x,height,z);

                if(height > maxHeight) maxHeight = height;
                if(height < minHeight) minHeight = height;
                i++;
            }
        }
        triangles = new int [SizeX * SizeZ * 6];

        int ver=0;
        int abg = 0;

       for (int z = 0; z <SizeZ ; z++){
            for( int x = 0; x <SizeX; x++ )
            {
                triangles[abg + 0] = ver + 0;
                triangles[abg + 1] = ver + SizeX +1 ;
                triangles[abg + 2] = ver + 1;
                triangles[abg + 3] = ver + 1;
                triangles[abg + 4] = ver + SizeX +1 ;
                triangles[abg + 5] = ver + SizeX +2;
                ver++;
                abg += 6;
            }
            ver++;
        } 
        i =0;
        colors = new Color[vertices.Length];
        for (int z = 0; z <= SizeZ ; z++){
            for( int x = 0; x <=SizeX; x++ )
            {
                float  height = Mathf.InverseLerp(minHeight,maxHeight, vertices[i].y);
                colors[i] = gradient.Evaluate(height);
                i++;
            }
        }
    }

    private float[,] FallOffMap(float falloffStart, float falloffEnd)
    {
        float [,] mapHeight = new float[SizeX+1,SizeZ+1];

        for(int y = 0; y <= SizeZ; y++)
        {
            for(int x = 0; x <= SizeX; x++)
            {
                Vector2 pos = new Vector2((float)x / SizeX * 2 -1,(float)y/SizeZ * 2 -1);
                float t = Mathf.Max(Mathf.Abs(pos.x),Mathf.Abs(pos.y));

                if(t < falloffStart) {
                    mapHeight[x , y ] = 1;
                }
                else if (t > falloffEnd) mapHeight[x,y] = 0;
                else mapHeight[x,y] = Mathf.SmoothStep(1,-0.8f,Mathf.InverseLerp(falloffStart,falloffEnd,t));
            }
        }

        return mapHeight;
    }
    



    private void MapPlacement() 
    {
        for (int i = 0; i < vertices.Length; i++)
        {
           
            var heightTree = vertices[i].y;
        
            if(System.Math.Abs(last - vertices[i].y) < 50 && objects.Length != 0)
            {
           
                if (heightTree > 6)
                {
                   
                    if (Random.Range(1,70) == 1 && System.Math.Abs(vertices[i].z - 200) > 2)
                    {
                        GameObject objectToSpawn = objects[Random.Range(0, objects.Length)];
                        var spawnAboveTerrainBy = heightTree*0.95f;
                        GameObject GO = Instantiate(objectToSpawn, new Vector3(vertices[i].x + spawned*100, spawnAboveTerrainBy, vertices[i].z), Quaternion.identity);
                        GO.transform.parent = land.transform;
                    }
                }
            }
            last = heightTree;
        }
    }

    void GenerateObstacle()
    {
        
        float lastX = -4 + spawned*100;
        for (int i = 0; i < vertices.Length; i++)
        {
            float curX = vertices[i].x + spawned*100;
            if(vertices[i].z == 200 && curX - lastX == 5 && MyObstacle != null)
            {   
                
                float offSet = Random.Range(.68f, 2.0f);
                float height = vertices[i].y + offSet;
                if (maxHeight-height > 5.5) height += (maxHeight-height)/1.6f;
                MyObstacle.gameObject.transform.position = new Vector3(curX ,height,196.2f);
                Instantiate(MyObstacle.gameObject, MyObstacle.gameObject.transform.position, MyObstacle.gameObject.transform.rotation);
                lastX = curX;
                
            }   
            
        }
       
    }
    

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.RecalculateBounds();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        MapPlacement();
        GenerateObstacle();
    }
}
