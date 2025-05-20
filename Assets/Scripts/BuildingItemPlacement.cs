using System;
using UnityEngine;

public class BuildingItemPlacement : MonoBehaviour
{
    private LayerMask planeMask;
    private LayerMask obstacleMask;

    private RaycastHit hit;

    [HideInInspector] public bool isSelected;

    private bool isValid;
    public bool IsValid => isValid;

    [HideInInspector] public Vector3 size;

    [SerializeField] private SafeArea safeAreaPrefab;
    private SafeArea safeArea;

    void Start()
    {
        planeMask = LayerMask.GetMask("Plane");
        obstacleMask = LayerMask.GetMask("Obstacle");

        safeArea = Instantiate(safeAreaPrefab);
        safeArea.transform.localScale = size;
        safeArea.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (!isSelected)
        {
            isValid = false;
        }
        else
        {
            isValid = CanPlaceOnPlane() && !IsColliding();
        }

        UpdateSafeArea();
    }

    private bool CanPlaceOnPlane()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity, planeMask);
    }

    private bool IsColliding()
    {
        Collider[] colliders = Physics.OverlapBox(hit.point, size * 0.5f, Quaternion.identity, obstacleMask);
        return colliders.Length > 0;
    }

    private void UpdateSafeArea()
    {
        if (!isSelected)
        {
            safeArea.gameObject.SetActive(false);
            return;
        }

        if (CanPlaceOnPlane())
        {
            safeArea.gameObject.SetActive(true);
            safeArea.transform.position = hit.point;
            safeArea.SetValid(!IsColliding());
        }
        else
        {
            safeArea.gameObject.SetActive(false);
        }
    }

    public void PlaceBuilding(Building building)
    {
        if (isValid)
        {
            Instantiate(building, hit.point, Quaternion.identity);
        }
    }
}