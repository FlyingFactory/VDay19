using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMapGenerator : MonoBehaviour {
    [SerializeField] private GameObject vertexPrefab;
    [SerializeField] private GameObject vertexMarkerPrefab;
    [SerializeField] private GameObject edgePrefab;
    [SerializeField] private GameObject edgeTrapPrefab;

    [SerializeField] private Transform vertexCardTransform;
    [SerializeField] private Transform edgeCardTransform;

    [SerializeField] private float spaceWidth = 5;
    [SerializeField] private float spaceHeight = 5;

    [SerializeField] private int numVertices = 10;
    [SerializeField] private int pathVertexCount = 5;
    [SerializeField] [Range(0,100)] private float dummyEdgeChance = 50f;

    
    public void OnValidate() {
        if (numVertices < 4) numVertices = 4;
        if (pathVertexCount < 3) pathVertexCount = 3;
        if (pathVertexCount > numVertices) pathVertexCount = numVertices;
    }

    [ContextMenu("Generate")]
    public void Generate() {
        if (vertexPrefab != null &&
            edgePrefab != null &&
            vertexMarkerPrefab != null &&
            edgeTrapPrefab != null &&
            vertexCardTransform != null &&
            edgeCardTransform != null) {

            List<GameObject> vertices = new List<GameObject>();
            List<GameObject> edges = new List<GameObject>();
            List<GameObject> edgeTraps = new List<GameObject>();

            for(int i = 0; i < numVertices; i++) {
                vertices.Add(Instantiate(vertexPrefab, 
                                         transform.position + new Vector3(Random.Range(-spaceWidth / 2, spaceWidth / 2), 
                                                                          Random.Range(0, spaceHeight), 
                                                                          Random.Range(-spaceWidth / 2, spaceWidth / 2)), 
                                         Quaternion.identity));
                vertices[i].transform.parent = transform;
            }

            Vector3 pos1;
            Vector3 pos2;
            Transform newEdge;

            Instantiate(vertexMarkerPrefab,
                        vertices[0].transform.position,
                        Quaternion.identity)
                        .transform.parent = vertexCardTransform;

            for (int i = 1; i < pathVertexCount; i++) {
                Instantiate(vertexMarkerPrefab,
                            vertices[i].transform.position,
                            Quaternion.identity)
                            .transform.parent = vertexCardTransform;
                pos1 = vertices[i].transform.position;
                pos2 = vertices[i - 1].transform.position;
                MakeEdge(pos1, pos2);
                //newEdge = Instantiate(edgePrefab,
                //                      (pos1 + pos2) / 2,
                //                      Quaternion.identity).transform;
                //newEdge.parent = edgeCardTransform;
                //newEdge.localScale = new Vector3(newEdge.localScale.x, newEdge.localScale.y * (pos1 - pos2).magnitude, newEdge.localScale.z);
                //newEdge.Rotate(new Vector3(pos1.z - pos2.z, 0, pos2.x - pos1.x),
                //               Mathf.Rad2Deg * Mathf.Atan2(Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.z - pos2.z, 2)), pos1.y - pos2.y));
            }

            for (int i = 0; i < pathVertexCount; i++) {
                for (int j = pathVertexCount; j < numVertices; j++) {
                    if (Random.Range(0, 100f) < dummyEdgeChance) {
                        pos1 = vertices[i].transform.position;
                        pos2 = vertices[j].transform.position;
                        MakeEdge(pos1, pos2, true);
                    }
                }
            }

            for (int i = pathVertexCount; i < numVertices-1; i++) {
                for (int j = i+1; j < numVertices; j++) {
                    if (Random.Range(0, 100f) < dummyEdgeChance) {
                        pos1 = vertices[i].transform.position;
                        pos2 = vertices[j].transform.position;
                        MakeEdge(pos1, pos2, true);
                    }
                }
            }
        }
    }

    public void MakeEdge(Vector3 pos1, Vector3 pos2, bool trap = false) {
        Transform newEdge = Instantiate(edgePrefab,
                                        (pos1 + pos2) / 2,
                                        Quaternion.identity).transform;
        newEdge.parent = transform;
        newEdge.localScale = new Vector3(newEdge.localScale.x, newEdge.localScale.y * (pos1 - pos2).magnitude, newEdge.localScale.z);
        newEdge.Rotate(new Vector3(pos1.z - pos2.z, 0, pos2.x - pos1.x),
                       Mathf.Rad2Deg * Mathf.Atan2(Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.z - pos2.z, 2)), pos1.y - pos2.y));

        if (trap) {
            newEdge = Instantiate(edgeTrapPrefab,
                                  (pos1 + pos2) / 2,
                                  Quaternion.identity).transform;
            newEdge.parent = edgeCardTransform;
            newEdge.localScale = new Vector3(newEdge.localScale.x, newEdge.localScale.y * (pos1 - pos2).magnitude, newEdge.localScale.z);
            newEdge.Rotate(new Vector3(pos1.z - pos2.z, 0, pos2.x - pos1.x),
                           Mathf.Rad2Deg * Mathf.Atan2(Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.z - pos2.z, 2)), pos1.y - pos2.y));
        }
    }

    [ContextMenu("Delete all children")]
    public void DeleteChildren() {
        for (int i = transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    [SerializeField] private Transform position1;
    [SerializeField] private Transform position2;
    [SerializeField] private bool FakeEdge;

    [ContextMenu("Generate single edge")]
    public void SingleEdge() {
        if (position1 != null && position2 != null) {
            MakeEdge(position1.position, position2.position, FakeEdge);
        }
    }
}
