using UnityEngine;
using UnityEngine.UI;


public class ExclamationMark : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    void Update()
    {
        transform.rotation = Quaternion.Inverse(target.transform.rotation);
        transform.position = target.position + offset;
    }
}