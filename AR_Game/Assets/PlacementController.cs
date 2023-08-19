using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class PlacementController : MonoBehaviour
{
    public GameObject prefab;

    public GameObject PlacedPrefab
    {
        get
        {
            return prefab;
        }

        set 
        { 
            prefab = value;
        }
    }

    private ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!TryGetTouchPos(out Vector2 pos))
        {
            return;
        }

        if(m_RaycastManager.Raycast(pos, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            Instantiate(prefab, hitPose.position, hitPose.rotation);
        }
    }

    bool TryGetTouchPos(out Vector2 pos)
    {
        if(Input.touchCount > 0)
        {
            pos = Input.GetTouch(0).position;
            return true;
        }

        pos = default;

        return false;
    }
}
