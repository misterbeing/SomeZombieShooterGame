
using System.Collections.Generic;
using UnityEngine;

public class ProceduralWall : MonoBehaviour
{
    [SerializeField] private float width = 1f;
    [SerializeField] private float height = 2f;
    [SerializeField] private Material leafMaterial;

    //private void Awake()
    //{
    //    GenerateLeaf();
    //}

    public void Update()
    {
        // Optional: Update the leaf mesh in real-time if width or height changes
        GenerateLeaf();
    }

    [ContextMenu("Generate Leaf")]
    public void GenerateLeaf()
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        mesh.name = "Procedural Leaf";

        //              Top
        //               /\
        //              /  \
        //             /    \
        //            /      \
        //           /        \
        //          /          \
        //         /            \
        //        /      |       \
        //       /       |        \
        //      /________|________\
        //    Bottom Left       Bottom Right

        Vector3[] vertices =
        {
            new Vector3(0, height, 0),             // 0 - Top
            new Vector3(-width * 0.5f, 0, 0),      // 1 - Bottom Left
            new Vector3(width * 0.5f, 0, 0),       // 2 - Bottom Right
            new Vector3(0, height * 0.5f, 0)       // 3 - Middle
        };

        int[] triangles =
        {
            // Front
            0, 1, 3,
            0, 3, 2,

            // Back
            0, 3, 1,
            0, 2, 3
        };

        Vector2[] uv =
        {
            new Vector2(0.5f, 1f), // Top
            new Vector2(0f, 0f),   // Bottom Left
            new Vector2(1f, 0f),   // Bottom Right
            new Vector2(0.5f, 0.5f)
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter.sharedMesh = mesh;

        if (leafMaterial != null)
        {
            meshRenderer.sharedMaterial = leafMaterial;
        }
    }

}
