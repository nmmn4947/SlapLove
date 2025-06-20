using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Audio List", order = 1)]
public class AudioList : ScriptableObject
{
    public List<AudioClip> audioClips;
}
