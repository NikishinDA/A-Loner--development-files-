// The Game Events used across the Game.
// Anytime there is a need for a new event, it should be added here.

using UnityEngine;

public static class GameEventsHandler
{
    public static readonly BoostPickUpEvent BoostPickUpEvent = new BoostPickUpEvent();
    public static readonly GameStartEvent GameStartEvent = new GameStartEvent();
    public static readonly GameOverEvent GameOverEvent = new GameOverEvent();
    public static readonly ChangeLevelNumberEvent ChangeLevelNumberEvent = new ChangeLevelNumberEvent();
    public static readonly ObjectiveChangeEvent ChangeObjectiveEvent = new ObjectiveChangeEvent();
    public static readonly ObjectiveCompleteEvent ObjectiveCompleteEvent = new ObjectiveCompleteEvent();
    public static readonly ItemPickUpEvent ItemPickUpEvent = new ItemPickUpEvent();
    public static readonly ChangeLevelProgressEvent ChangeLevelProgressEvent = new ChangeLevelProgressEvent();
    public static readonly DebugCallEvent DebugCallEvent = new DebugCallEvent();
    public static readonly ShowQuestPopUpEvent ShowQuestPopUpEvent = new ShowQuestPopUpEvent();
    public static readonly ObjectiveRevertEvent ObjectiveRevertEvent= new ObjectiveRevertEvent();
}

public class GameEvent {}

public class GameStartEvent : GameEvent
{
}

public class GameOverEvent : GameEvent
{
    public bool IsWin;
}

public class ChangeLevelNumberEvent : GameEvent
{
    public int CurrentLevelNumber;
}

public class ObjectiveChangeEvent : GameEvent
{
    public int Id;
    public Transform Objective;
}

public class ObjectiveCompleteEvent : GameEvent
{
    
}

public class ItemPickUpEvent : GameEvent
{
    public bool Discarding;
    public CollectItems Item;
    public Transform ItemTransform;
}

public class ObjectiveRevertEvent : GameEvent
{
    
}

public class DebugCallEvent : GameEvent
{
    public float Speed;
    public int SpawnRows;
    public int SpawnCols;
    public float SpawnBoundsX;
    public float SpawnBoundsY;
    public float PlayerBoundsSide;
    public float PlayerBoundsTop;
    public float PlayerBoundsBottom;
}

public class BoostPickUpEvent : GameEvent
{
    public BoostType Type;
}

public class ShowQuestPopUpEvent : GameEvent
{
    public bool Toggle;
}

public class ChangeLevelProgressEvent : GameEvent
{
    public float CurrentProgress;
}


