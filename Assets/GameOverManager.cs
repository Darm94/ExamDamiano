using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private Canvas canvasToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canvasToEnable != null)
        {
            canvasToEnable.enabled = true;
        }
    }
}
