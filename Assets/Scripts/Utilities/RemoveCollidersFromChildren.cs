using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Attach to a root GameObject and run the Context Menu command or call RemoveColliders()
/// This will remove all MeshCollider and BoxCollider components on children/grandchildren (does not remove colliders on the root itself).
/// If a child contains a MeshFilter but has no Collider, a BoxCollider will be added automatically.
/// </summary>
public class RemoveCollidersFromChildren : MonoBehaviour
{
    [Tooltip("Include inactive children when searching")]
    public bool includeInactive = true;

    [ContextMenu("Remove Mesh and Box Colliders From Children")]
    public void RemoveColliders()
    {
        // Remove MeshColliders
        var meshColliders = GetComponentsInChildren<MeshCollider>(includeInactive);
        foreach (var mc in meshColliders)
        {
            if (mc == null) continue;
            if (mc.gameObject == gameObject) continue; // skip root
#if UNITY_EDITOR
            if (!Application.isPlaying) Object.DestroyImmediate(mc);
            else Object.Destroy(mc);
#else
            Object.Destroy(mc);
#endif
        }

        // Remove BoxColliders
        var boxColliders = GetComponentsInChildren<BoxCollider>(includeInactive);
        foreach (var bc in boxColliders)
        {
            if (bc == null) continue;
            if (bc.gameObject == gameObject) continue; // skip root
#if UNITY_EDITOR
            if (!Application.isPlaying) Object.DestroyImmediate(bc);
            else Object.Destroy(bc);
#else
            Object.Destroy(bc);
#endif
        }

        // For each child with a MeshFilter, ensure it has a BoxCollider.
        // Skip root object and any child that already has any Collider.
        var meshFilters = GetComponentsInChildren<MeshFilter>(includeInactive);
        foreach (var mf in meshFilters)
        {
            if (mf == null) continue;
            var go = mf.gameObject;
            if (go == gameObject) continue; // skip root
            if (mf.sharedMesh == null) continue; // no mesh to bound
            if (go.GetComponent<Collider>() != null) continue; // already has a collider

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                Undo.AddComponent(go, typeof(BoxCollider));
            }
            else
            {
                go.AddComponent<BoxCollider>();
            }
#else
            go.AddComponent<BoxCollider>();
#endif
        }
    }   
}