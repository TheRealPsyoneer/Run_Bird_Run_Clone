using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEnum : MonoBehaviour
{
    
}

public enum State
{
    Idle, Move, Die, Sleep, Rotate, Fall
}

public enum GameState
{
    MainMenu, Playing, Pause
}

public enum ChallengeType
{
    PlayGames, ScoreSingleGame, CollectTotalCandies, ScoreTotalPoints, CollectCandiesSingleGame
}
