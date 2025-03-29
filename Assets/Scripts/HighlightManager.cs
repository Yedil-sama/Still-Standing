using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightManager : MonoBehaviour
{
    [SerializeField] private LayerMask selectableLayer;
    [SerializeField] private LayerMask allyLayer;
    [SerializeField] private LayerMask neutralLayer;
    private Transform highlightedTransform;
    private Transform selectedTransform;

    private Outline highlightOutline;
    private RaycastHit hit;

    public void Update()
    {
        HoverHighlight();
    }
    public void HoverHighlight()
    {
        if (highlightedTransform != null)
        {
            highlightOutline.enabled = false;
            highlightedTransform = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayer | allyLayer | neutralLayer))
        {
            highlightedTransform = hit.transform;

            int objectLayer = highlightedTransform.gameObject.layer;

            if (((1 << objectLayer) & allyLayer) != 0 || ((1 << objectLayer) & neutralLayer) != 0 || ((1 << objectLayer) & selectableLayer) != 0)
            {
                highlightOutline = highlightedTransform.GetComponent<Outline>();
                highlightOutline.enabled = true;
            }
            else
            {
                highlightedTransform = null;
            }
        }
    }
    public void SelectedHighlight()
    {
        if (highlightedTransform != null && ((1 << highlightedTransform.gameObject.layer) & selectableLayer) != 0)
        {
            if (selectedTransform != null)
            {
                selectedTransform.GetComponent<Outline>().enabled = false;
            }

            selectedTransform = hit.transform;
            selectedTransform.GetComponent<Outline>().enabled = true;

            highlightOutline.enabled = true;
            highlightedTransform = null;
        }
    }
    public void DeselectHighlight()
    {
        if (selectedTransform != null)
        {
            selectedTransform.GetComponent<Outline>().enabled = false;
            selectedTransform = null;
        }
    }
}