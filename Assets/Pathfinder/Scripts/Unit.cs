using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public const float minPathUpdateTime = 0.2f;
    public const float pathUpdateThreshold = 0.5f;

    public Transform target;
    public float speed = 5f;
    public float turnSpeed = 3f;
    public float turnDist = 5f;
    Path path;

    private void Start()
    {
        StartCoroutine(updatePath());

        // Use this if you want to search path only once in the beginning
        //PathRequestManager.requestPath(new pathRequest(transform.position, target.position, OnPathFound));
    }

    private void Update()
    {
        //PathRequestManager.requestPath(new pathRequest(transform.position, target.position, OnPathFound));        // Not advisable to use
        // due to performance reasons



        //*********** You can also instantiate a target at runtime ***********//
        //*********** and then assign it to the "Target" variable  ***********//
        //*********** and the script will find path to the target  ***********//
        //*********** as soon as variable is assigned. If the      ***********//
        //*********** target is already available in the scene and ***********//
        //*********** you want to find path only when a certain    ***********//
        //*********** condition is met, you may add a variable     ***********//
        //*********** check in the pathfinding coroutine.
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        //    {
        //        target.position = hit.point;
        //    }
        //}
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)                                                     // This is where you can add the above mentioned variable check.
        {
            path = new Path(waypoints, transform.position, turnDist);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
    
    IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);

        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while (path.turnBoundaries[pathIndex].hasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }

            if (followingPath)
            {
                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
            }

            yield return null;
        }
    }

    IEnumerator updatePath()
    {
#if UNITY_EDITOR
        if (Time.timeSinceLevelLoad < 0.3f)
        {
            yield return new WaitForSeconds(0.3f);
        }
#endif
        if (target != null)
        {
            PathRequestManager.requestPath(new pathRequest(transform.position, target.position, OnPathFound));

            float pathUpdateThresholdSqr = pathUpdateThreshold * pathUpdateThreshold;
            Vector3 targetPosOld = target.position;

            while (true)
            {
                yield return new WaitForSeconds(minPathUpdateTime);

                if ((target.position - targetPosOld).sqrMagnitude > pathUpdateThresholdSqr)
                {
                    PathRequestManager.requestPath(new pathRequest(transform.position, target.position, OnPathFound));
                    targetPosOld = target.position;
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            path.DrawWithGizmos();
        }
    }
}
