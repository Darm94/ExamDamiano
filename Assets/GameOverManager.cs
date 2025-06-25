using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private Canvas canvasToEnable;

    private void OnTriggerEnter(Collider other)
    {
        
        if (canvasToEnable != null)
        {
            Debug.Log("TRIGGHEEEER");
            canvasToEnable.gameObject.SetActive(true);
        }
    }
}
