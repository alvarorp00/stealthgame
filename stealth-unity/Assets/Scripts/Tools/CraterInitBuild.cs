using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class CraterInitBuild : MonoBehaviour
{
    public GameObject parentObj;
    public bool run = false;
    public string nametag = "CraterTerrainCubes";
    public GameObject[] layer16, layer15, layer14, layer13, layer12, layer11, layer10, layer9, layer8, layer7, layer6, layer5, layer4, layer3, layer2, layer1, layer0; // kinda mess all af theses variables... :(

    private int xFrom = 0, xTo = 150;
    private int zFrom = 0, zTo = 150;
    private int yFrom = 20, yTo = 36;
    private int ljump = 1;

    private int x, y, z;
    private int xref, zref, yref;
    private static float cutDistance = 16.0f;

    private Vector3 vector, refVector;
    private Quaternion qr = new Quaternion(0,0,0,1);
    private GameObject newObj;

    private Dictionary<uint, GameObject[]> layerObjects = new Dictionary<uint, GameObject[]>(17);
    private Hashtable generatedObjects = new Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        if (run)
        {
            layerObjects.Add(0, layer0);
            layerObjects.Add(1, layer1);
            layerObjects.Add(2, layer2);
            layerObjects.Add(3, layer3);
            layerObjects.Add(4, layer4);
            layerObjects.Add(5, layer5);
            layerObjects.Add(6, layer6);
            layerObjects.Add(7, layer7);
            layerObjects.Add(8, layer8);
            layerObjects.Add(9, layer9);
            layerObjects.Add(10, layer10);
            layerObjects.Add(11, layer11);
            layerObjects.Add(12, layer12);
            layerObjects.Add(13, layer13);
            layerObjects.Add(14, layer14);
            layerObjects.Add(15, layer15);
            layerObjects.Add(16, layer16);

            xref = ( xTo + xFrom ) / 2;
            zref = ( zTo + zFrom ) / 2;
            yref = yTo; // we don't want circular cut but parabolic one

            refVector = new Vector3(xref, yref, zref);

            Operate();
        }
    }

    private void Operate()
    {
        _buildCrater();
    }

    void _buildCrater()
    {
        if (parentObj != null)
        {
            for (y = yFrom; y <= yTo; y += ljump)
            {
                for (x = xFrom; x <= xTo; x += ljump)
                {
                    for (z = zFrom; z <= zTo; z += ljump)
                    {
                        vector = new Vector3(x, y, z);
                        if (Vector3.Distance(vector, refVector) - ((float)(y + 1) / yTo) > cutDistance + 1) // not in edge
                            continue;
                        if (Vector3.Distance(vector, refVector) - ((float)(y + 1) / yTo) <= cutDistance && y != yFrom)
                            continue;
                        newObj = Instantiate(layerObjects[(uint)(y - yFrom)][Random.Range(0, layerObjects[(uint)(y - yFrom)].Length)], vector, qr);
                        newObj.tag = nametag;
                        newObj.transform.parent = parentObj.transform;
                        generatedObjects[vector] = newObj;
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
