using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

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

    private ARRaycastManager ARRaycastManager;

    // Start is called before the first frame update
    void Start()
    {
        ARRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!TryGetTouchPos(out Vector2 touchPos))
        {
            return;
        }

        if(ARRaycastManager.Raycast(touchPos, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            Instantiate(PlacedPrefab, hitPose.position, hitPose.rotation);
        }
    }

    bool TryGetTouchPos(out Vector2 touchPos)
    {
        if(Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }

        touchPos = default;
        return false;
    }

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
}
