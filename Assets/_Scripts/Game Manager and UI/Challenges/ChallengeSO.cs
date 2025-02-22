using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Challenge", menuName = "Challenge SO")]
public class ChallengeSO : ScriptableObject
{
    public string instruction;
    public int goal;
    public ChallengeType challengeType;
}
