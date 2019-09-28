using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour {

    [SerializeField] List<Path> paths = new List<Path>();
    [SerializeField] bool debug = false;
    AudioSource audioData;

    void Start() {
        audioData = gameObject.GetComponent<AudioSource>();
        //Debug.Log("loool");
    }
    

    public Path GetNearestPath(Vector3 monsterPosition)
    {
        float lowestDistance = float.PositiveInfinity;
        int lowestIndex = 0;
        for(int i = 0; i < paths.Count; i++)
        {
            if (paths[i] == null || paths[i].actualPath.Count == 0)
            {
                if(debug) Debug.Log("Path Manager: un path ha sido ignorado por ser igual a null o no tener waypoints.");
                continue;
            }
            float distance = Vector3.Distance(monsterPosition, paths[i].actualPath[0]);
            if(distance < lowestDistance)
            {
                lowestDistance = distance;
                lowestIndex = i;
            }
        }
        return paths[lowestIndex];
    }

    public void playDeath() {
        //audioData = gameObject.GetComponent<AudioSource>();
        //audioData.Play(0);
    }
}
