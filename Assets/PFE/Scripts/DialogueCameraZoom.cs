using UnityEngine;
using Cinemachine;

public class DialogueCameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomedInFOV = 40f; // Champ de vision réduit pour le zoom
    public float normalFOV = 60f; // Champ de vision normal
    public float zoomSpeed = 2f; // Vitesse d'animation du zoom

    private void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("🎥 ERREUR : Aucune CinemachineVirtualCamera assignée à " + gameObject.name);
        }
    }

    public void StartDialogue()
    {
        Debug.Log("🔍 Zoom IN activé !");
        if (virtualCamera != null)
        {
            StopAllCoroutines();
            StartCoroutine(ZoomCamera(zoomedInFOV));
        }
    }

    public void EndDialogue()
    {
        Debug.Log("🔄 Zoom OUT activé !");
        if (virtualCamera != null)
        {
            StopAllCoroutines();
            StartCoroutine(ZoomCamera(normalFOV));
        }
    }

    private System.Collections.IEnumerator ZoomCamera(float targetFOV)
    {
        float startFOV = virtualCamera.m_Lens.FieldOfView;
        float elapsedTime = 0f;

        while (elapsedTime < zoomSpeed)
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsedTime / zoomSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        virtualCamera.m_Lens.FieldOfView = targetFOV;
        Debug.Log("🎯 Zoom terminé : " + targetFOV);
    }
}
