using UnityEngine;

public class SlimeContainer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Slime")) return;
        if (SlimesManager.Singleton.CapturedSlimes.Contains(other.gameObject)) return;

        SlimesManager.Singleton.CapturedSlimes.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Slime")) return;

        if (SlimesManager.Singleton.CapturedSlimes.Contains(other.gameObject)) 
        {
            SlimesManager.Singleton.CapturedSlimes.Remove(other.gameObject);
        }
    }
}
