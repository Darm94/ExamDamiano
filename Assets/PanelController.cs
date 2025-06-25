using System.Collections;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] private Transform doorToMove;
    [SerializeField] private float targetX = 1f;
    [SerializeField] private float moveDuration = 1f;

    public void ActivatePanel()
    {
        if (doorToMove != null)
        {
            StopAllCoroutines();
            StartCoroutine(MoveDoorInX());
        }
        else
        {
            Debug.LogWarning("porta non assegnata!");
        }
    }

    private IEnumerator MoveDoorInX()
    {
        Vector3 startPos = doorToMove.position;
        Vector3 endPos = new Vector3(targetX, startPos.y, startPos.z);

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            doorToMove.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        doorToMove.position = endPos;
    }
}