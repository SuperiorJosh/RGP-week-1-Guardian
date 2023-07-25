using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStepDatabase : IDatabase<GameStepEvent>
{
    private List<GameStepEvent> m_db = new();
    public void Init()
    {
        m_db = new();
        var assets = DatabaseHelpers<GameStepEvent>.GetAssets("GameSteps");
        foreach (var asset in assets)
        {
            m_db.Add(asset);
        }
        Debug.Log($"Loaded <b>{assets.Count}</b> Game Steps");
    }

    public void GetInstance(GameStepEvent data)
    {
        
    }

    public IEnumerable<GameStepEvent> GetAll()
    {
        return m_db.ToList();
    }

    public GameStepEvent GetSingle()
    {
        return null;
    }
}