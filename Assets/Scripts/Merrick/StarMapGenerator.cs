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

    [SerializeField] [Min(4)] private int numVertices = 10;
    [SerializeField] [Min(3)] private int pathVertexCount = 5;
    [SerializeField] [Range(0,100)] private float dummyEdgeChance = 50f;

    public void OnValidate() {
        if (pathVertexCount > numVertices) pathVertexCount = numVertices;
    }

    public void Reset() {
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
                                         Quaternion.identity, 
                                         transform));
            }

            Vector3 pos1;
            Vector3 pos2;
            Transform newEdge;

            for (int i = 1; i < pathVertexCount; i++) {
                Instantiate(vertexMarkerPrefab,
                            vertices[i].transform.position,
                            Quaternion.identity,
                            transform);
                pos1 = vertices[i].transform.position;
                pos2 = vertices[i - 1].transform.position;
                newEdge = Instantiate(edgePrefab,
                                    (pos1 + pos2) / 2,
                                    Quaternion.identity,
                                    edgeCardTransform).transform;
                newEdge.localScale = new Vector3(newEdge.localScale.x, (pos1 - pos2).magnitude, newEdge.localScale.z);
                newEdge.Rotate(new Vector3(pos1.z - pos2.z, 0, pos2.x - pos1.x),
                               Mathf.Rad2Deg * Mathf.Atan2(pos1.z - pos2.z, Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x,2) + Mathf.Pow(pos1.z - pos2.z, 2))));
            }

            for (int i = 0; i < pathVertexCount; i++) {
                for (int j = pathVertexCount; i < numVertices; j++) {
                    if (Random.Range(0, 100f) < dummyEdgeChance) {
                        pos1 = vertices[i].transform.position;
                        pos2 = vertices[j].transform.position;

                        newEdge = Instantiate(edgePrefab,
                                              (pos1 + pos2) / 2,
                                              Quaternion.identity,
                                              edgeCardTransform).transform;
                        newEdge.localScale = new Vector3(newEdge.localScale.x, (pos1 - pos2).magnitude, newEdge.localScale.z);
                        newEdge.Rotate(new Vector3(pos1.z - pos2.z, 0, pos2.x - pos1.x),
                                       Mathf.Rad2Deg * Mathf.Atan2(pos1.z - pos2.z, Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.z - pos2.z, 2))));

                        newEdge = Instantiate(edgeTrapPrefab,
                                              (pos1 + pos2) / 2,
                                              Quaternion.identity,
                                              edgeCardTransform).transform;
                        newEdge.localScale = new Vector3(newEdge.localScale.x, (pos1 - pos2).magnitude, newEdge.localScale.z);
                        newEdge.Rotate(new Vector3(pos1.z - pos2.z, 0, pos2.x - pos1.x),
                                       Mathf.Rad2Deg * Mathf.Atan2(pos1.z - pos2.z, Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.z - pos2.z, 2))));
                    }
                }
            }

            for (int i = pathVertexCount; i < numVertices-1; i++) {
                for (int j = i+1; i < numVertices; j++) {
                    if (Random.Range(0, 100f) < dummyEdgeChance) {
                        pos1 = vertices[i].transform.position;
                        pos2 = vertices[j].transform.position;

                        newEdge = Instantiate(edgePrefab,
                                              (pos1 + pos2) / 2,
                                              Quaternion.identity,
                                              edgeCardTransform).transform;
                        newEdge.localScale = new Vector3(newEdge.localScale.x, (pos1 - pos2).magnitude, newEdge.localScale.z);
                        newEdge.Rotate(new Vector3(pos1.z - pos2.z, 0, pos2.x - pos1.x),
                                       Mathf.Rad2Deg * Mathf.Atan2(pos1.z - pos2.z, Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.z - pos2.z, 2))));

                        newEdge = Instantiate(edgeTrapPrefab,
                                              (pos1 + pos2) / 2,
                                              Quaternion.identity,
                                              edgeCardTransform).transform;
                        newEdge.localScale = new Vector3(newEdge.localScale.x, (pos1 - pos2).magnitude, newEdge.localScale.z);
                        newEdge.Rotate(new Vector3(pos1.z - pos2.z, 0, pos2.x - pos1.x),
                                       Mathf.Rad2Deg * Mathf.Atan2(pos1.z - pos2.z, Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.z - pos2.z, 2))));
                    }
                }
            }


        }
    }
}
