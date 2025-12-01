using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DebugRayVisualizer : MonoBehaviour
{
    [Header("Ray Settings")]
    [SerializeField] private Transform _rayTransform;
    [SerializeField] private float _maxDistance = 30f;

    [Header("Layer Filtering")]
    [SerializeField] private LayerMask hitLayers;

    [Header("Line Appearance")]
    [SerializeField] private float _lineWidth = 0.05f;
    [SerializeField] private Color _lineColor = Color.green;

    private LineRenderer _lineRenderer;

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        SetupLineRenderer();
    }

    void Update()
    {
        UpdateRaycast();
    }

    void SetupLineRenderer()
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.startWidth = _lineWidth;
        _lineRenderer.endWidth = _lineWidth;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = _lineColor;
        _lineRenderer.endColor = _lineColor;
        _lineRenderer.useWorldSpace = true;
    }

    void UpdateRaycast()
    {
        var origin = _rayTransform.position;
        var direction = _rayTransform.forward;
        Vector3 endPoint = origin + direction.normalized * _maxDistance;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, _maxDistance, hitLayers))
        {
            endPoint = hit.point;
        }

        _lineRenderer.SetPosition(0, origin);
        _lineRenderer.SetPosition(1, endPoint);
    }
}