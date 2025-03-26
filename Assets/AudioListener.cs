using UnityEngine;

public class AudioListenerManager : MonoBehaviour
{
    void Awake()
    {
        // Get all AudioListener components in the scene
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();

        // If there is more than one, disable all but the first one
        if (listeners.Length > 1)
        {
            for (int i = 1; i < listeners.Length; i++)
            {
                listeners[i].enabled = false;
            }
        }
    }
}
