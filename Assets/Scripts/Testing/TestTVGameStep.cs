using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTVGameStep : MonoBehaviour
{
    [SerializeField] GameStepEvent gameStep;
    
    public void CompleteGameStep()
    {
        gameStep.ChangeContext(GameStepEventState.Completed);
    }
}
