using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintSequence : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera drawingCamera;
    [Header("Trail Prefab from Folders")]
    [SerializeField] private LineRenderer trailPrefab;
    [Header("Other Connections")]
    [SerializeField] private Transform finishTpPoint;
    [SerializeField] private Animator boy;
    [SerializeField] private Slider widthSlider;
    
    private LineRenderer currentTrail;
    private List<Vector3> points = new List<Vector3>();

    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameStates.painting)
            Drawable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            GameManager.Instance.gameState = GameManager.GameStates.painting;
            mainCamera.gameObject.SetActive(false);
            drawingCamera.gameObject.SetActive(true);

            other.gameObject.transform.position = finishTpPoint.position;

            boy.SetTrigger("Dance");
        }
    }

    private void Drawable()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateNewLine();
        }

        if (Input.GetMouseButton(0))
        {
            AddPoint();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (transform.childCount != 0)
            {
                foreach (Transform R in transform)
                {
                    Destroy(R.gameObject);
                }
            }
        }
    }

    private void CreateNewLine()
    {
        currentTrail = Instantiate(trailPrefab);
        currentTrail.transform.SetParent(transform, true);
        points.Clear();
    }

    private void UpdateLinePoints()
    {
        if (currentTrail != null && points.Count > 1)
        {
            currentTrail.positionCount = points.Count;
            currentTrail.SetPositions(points.ToArray());
        }
    }

    private void AddPoint()
    {
        var Ray = drawingCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(Ray, out hit))
        {
            if (hit.collider.CompareTag("Writeable"))
            {
                points.Add(hit.point);
                UpdateLinePoints();
                return;
            }
            else
                return;
        }
    }

    public void ChangeLineRendererMaterial(Material material)
    {
        foreach (Transform child in transform)
        {
            LineRenderer lineRenderer = child.GetComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                lineRenderer.material = material;
            }
        }
    }

    public void ChangeLineRendererWidth()
    {
        foreach (Transform child in transform)
        {
            LineRenderer lineRenderer = child.GetComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                lineRenderer.startWidth = widthSlider.value;
                lineRenderer.endWidth = widthSlider.value;
            }
        }
    }
}
