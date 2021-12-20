using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class craterQuadrantBuilder : MonoBehaviour
{
    public bool run = false;
    public GameObject material = null;
    public GameObject parent = null;
    public float xFrom = .0f, xTo = .0f;
    public float zFrom = .0f, zTo = .0f;
    public float yLayer = .0f;
    public float jump = .0f;

    //private variables

    private List<GameObject> generated = new List<GameObject>();

    private GameObject newObj;
    private Vector3 vector;
    private string nametag = "floorCubes";

    private Vector3 refVector;
    private float xRef;
    private float zRef;
    private float cutDistance = 17.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (run && material != null && parent != null)
        {
            xRef = (xTo / 2);
            zRef = (zTo / 2);
            refVector = new Vector3(xRef, yLayer, zRef);
            _build();
        }
    }

    void _build()
    {
        float x, z;
        for (x = xFrom; x <= xTo; x += jump)
        {
            for (z = zFrom; z <= zTo; z += jump)
            {
                vector = new Vector3(x, yLayer, z);
                if (Vector3.Distance(vector, refVector) <= cutDistance)
                    continue;
                newObj = Instantiate(material, vector, new Quaternion(0, 0, 0, 1));
                newObj.transform.parent = parent.transform;
                newObj.tag = nametag;
                generated.Add(newObj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
