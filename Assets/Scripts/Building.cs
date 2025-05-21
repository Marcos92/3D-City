using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Building : MonoBehaviour
{
    public BuildingInfo info;
    private Vector3 size;
    [SerializeField] private Transform modelParent;
    [SerializeField] private bool animateOnEnable = true;

    void Awake()
    {
        size = info.size;
        GetComponent<BoxCollider>().size = size;
    }

    void OnEnable()
    {
        Instantiate(info.model, transform.position, transform.rotation, modelParent);

        if (!animateOnEnable)
        {
            GetComponent<Animator>().enabled = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 0.5f);
        Gizmos.DrawCube(transform.position, size);
    }
}