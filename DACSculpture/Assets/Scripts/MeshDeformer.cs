 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour {
    public bool clickrelache=false;
    public float brush = 1.0f;
    Mesh deformingMesh;
    Vector3[] originalVertices, displacedVertices;
    Vector3[] vertexVelocities;
    // Use this for initialization
    void Start()
    {

        init();
    }

    public void init()
    {
        deformingMesh = GetComponent<MeshFilter>().mesh;
        originalVertices = deformingMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
        for (int i = 0; i < originalVertices.Length; i++)
        {
            displacedVertices[i] = originalVertices[i];
        }
        vertexVelocities = new Vector3[originalVertices.Length];
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < displacedVertices.Length; i++)
            {
                UpdateVertex(i);
            }
            deformingMesh.vertices = displacedVertices;
            clickrelache = true;
        }
        
        else
        {
            ResetVertex();
            deformingMesh.vertices = displacedVertices;
        }
        deformingMesh.RecalculateNormals();
    }

    public void AddDeformingForce(Vector3 point, float force)
    {
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            AddForceToVertex(i, point, force);
        }
    }

    public void AddDeformingForce2(Vector3 point, float force)
    {
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            AddForceToVertex2(i, point, force);
        }
    }

    public void AddDeformingForce3(Vector3 point, float force, Vector3 dir)
    {
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            AddForceToVertex3(i, point, force, dir);
        }
    }

    public void AddDeformingForce4(Vector3 point, float force, Vector3 dir)
    {
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            AddForceToVertex4(i, point, force, dir);
        }
    }



    void AddForceToVertex(int i, Vector3 point, float force)
    {
       
        Vector3 pointToVertex = displacedVertices[i] - point;
        float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
        float velocity = attenuatedForce * Time.deltaTime;
        vertexVelocities[i] += pointToVertex.normalized * velocity;
    }

    void AddForceToVertex2(int i, Vector3 point, float force)
    {
        
        Vector3 pointToVertex = displacedVertices[i] - point;
        float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
        float velocity = attenuatedForce * Time.deltaTime;
        vertexVelocities[i] += pointToVertex.normalized * velocity;
    }

    void AddForceToVertex3(int i, Vector3 point, float force, Vector3 dir)
    {
        Vector3 pointToVertex = displacedVertices[i] - point;
        float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
        float velocity = attenuatedForce * Time.deltaTime;
        vertexVelocities[i] = dir * velocity;
        
    }

    void AddForceToVertex4(int i, Vector3 point, float force, Vector3 dir)
    {
        Vector3 pointToVertex = displacedVertices[i] - point;
        float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
        float velocity = attenuatedForce * Time.deltaTime;
        vertexVelocities[i] = -dir * velocity;
    }

    public void test()
    {
        init();
    }

    void ResetVertex()
    {
        
        for ( int i = 0; i < vertexVelocities.Length; i++)
        {
            vertexVelocities[i] = Vector3.zero;
        }
        for ( int i = 0;i< originalVertices.Length; i++)
        {
            originalVertices[i] = displacedVertices[i];
        }
        
        if (clickrelache)
        {
            MeshCollider[] colliders = FindObjectsOfType<MeshCollider>();
            int len = colliders.Length;
            for (int i = 0; i < len; i++) GameObject.Destroy(colliders[i]);
            MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
            clickrelache = false;
        }
    }
    void UpdateVertex(int i)
    {
        Vector3 velocity = vertexVelocities[i];
       
        displacedVertices[i] += velocity * Time.deltaTime;
    }
    
}
