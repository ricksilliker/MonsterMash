using UnityEngine;

public class HoverTool : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerController controller = FindObjectOfType<PlayerController>();
        controller.tileHovered.AddListener(OnSelectionChanged);
    }

    private void OnDisable()
    {
        PlayerController controller = FindObjectOfType<PlayerController>();
        controller.tileHovered.RemoveListener(OnSelectionChanged);    }

    private void OnSelectionChanged(Vector3 position)
    {
        transform.position = position;
    }
}
