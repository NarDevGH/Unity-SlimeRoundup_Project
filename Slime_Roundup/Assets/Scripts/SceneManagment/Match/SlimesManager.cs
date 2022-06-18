using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlimesManager : MonoBehaviour
{
    public List<GameObject> AllSlimes { get; set; }
    public List<GameObject> CapturedSlimes { get; set; }
    public List<GameObject> ScapedSlimes { get; set; }

    public int AllSlimes_Ammount => AllSlimes.Count;
    public int CapturedSlimes_Ammount => CapturedSlimes.Count;
    public int ScapedSlimes_Ammount => ScapedSlimes.Count;
    public int InGameSlimes_Ammount => AllSlimes_Ammount - ScapedSlimes_Ammount - CapturedSlimes_Ammount;

    public static SlimesManager Singleton;

    private void Awake()
    {
        HandleInstances();

        AllSlimes = new List<GameObject>();
        CapturedSlimes = new List<GameObject>();
        ScapedSlimes = new List<GameObject>();
    }

    public void StopAllCapturedSlimes() 
    {
        foreach (var slime in CapturedSlimes) 
        {
            slime.GetComponent<Slime_BehaviorsHandler>().currentBehavior.EndBehavior();
        }
    }

    public void StopAllSlimes()
    {
        foreach (var slime in AllSlimes)
        {
            slime.GetComponent<Slime_BehaviorsHandler>().currentBehavior.EndBehavior();
        }
    }

    public void SetInGameSlimesAsScaped() 
    {
        if (InGameSlimes_Ammount == 0) return;

        ScapedSlimes.AddRange(AllSlimes.Where(x => !CapturedSlimes.Contains(x) && !ScapedSlimes.Contains(x)));
    }

    private void HandleInstances()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }

}
