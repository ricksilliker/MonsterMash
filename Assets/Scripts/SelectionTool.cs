using UnityEngine;

public class SelectionTool : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerController controller = FindObjectOfType<PlayerController>();
        controller.tileSelected.AddListener(OnSelectionChanged);
    }

    private void OnDisable()
    {
        PlayerController controller = FindObjectOfType<PlayerController>();
        controller.tileSelected.RemoveListener(OnSelectionChanged);    }

    private void OnSelectionChanged(Vector3 position)
    {
        transform.position = position;
    }
}
