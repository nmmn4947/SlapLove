using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BPM Data", order = 1)]
public class BPMData : ScriptableObject
{
    public float BPM;
    public float ToHitTime; // Time for one beat in seconds  
}
