
using UnityEngine;

public class EditorGizmos : MonoBehaviour
{
#if UNITY_EDITOR
    private enum colors {red,green,blue,yellow,black,magenta,cyan}
    [SerializeField] private colors color;
    void OnDrawGizmos()
    {
        switch (color)
        {
            case colors.red:
                Gizmos.color = Color.red;
                break;
            case colors.green:
                Gizmos.color = Color.green;
                break;
            case colors.blue:
                Gizmos.color = Color.blue;
                break;
            case colors.yellow:
                Gizmos.color = Color.yellow;
                break;
            case colors.black:
                Gizmos.color = Color.black;
                break;
            case colors.magenta:
                Gizmos.color = Color.magenta;
                break;
            case colors.cyan:
                Gizmos.color = Color.cyan;
                break;
            default:
                 Gizmos.color = Color.black;
                break;
        }
        // Convert the local coordinate values into world
        // coordinates for the matrix transformation.
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }
#endif
}
