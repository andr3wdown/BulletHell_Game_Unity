using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameData gameData;
    public static int gameNumber;

    private void Start()
    {
        gameNumber = PlayerPrefs.GetInt("SaveSlot");
    }
    public void SaveGame()
    {
        gameData = CreateGameData();

        BinarySerialier<GameData, string>.SaveData(gameData, gameData.gameName);
    }
    GameData CreateGameData()
    {
        HubWorldPlayerController hb = FindObjectOfType<HubWorldPlayerController>();

        PlayerData pd = new PlayerData(hb.transform.position.x, hb.transform.position.y);
        ProgressData prd = new ProgressData();

        return new GameData(pd, prd, gameNumber, ZRotations());
    }

    float[] ZRotations()
    {
        List<Orbiter> orbitersInOrder = OrbitersInOrder();
        float[] zrots = new float[orbitersInOrder.Count];
        for(int i = 0; i < orbitersInOrder.Count; i++)
        {
            zrots[i] = orbitersInOrder[i].currentRotZ;
        }
        return zrots;      
    }

    void ApplyZRotations(float[] zrots)
    {
        List<Orbiter> orbitersInOrder = OrbitersInOrder();
        for (int i = 0; i < orbitersInOrder.Count; i++)
        {
            orbitersInOrder[i].currentRotZ = zrots[i];
        }
    }

    void ApplyPlayerPosition(float x, float y)
    {
        FindObjectOfType<HubWorldPlayerController>().transform.position = new Vector3(x, y, 0);
        Camera.main.transform.position = new Vector3(x, y, -10);
     }

    List<Orbiter> OrbitersInOrder()
    {
        Orbiter[] allOrbiters = FindObjectsOfType<Orbiter>();
        List<Orbiter> orbitersInOrder = new List<Orbiter>();
        Orbiter newAddable = allOrbiters[0];
        Orbiter notNewAddable = allOrbiters[0];
        while (orbitersInOrder.Count < allOrbiters.Length)
        {
            newAddable = notNewAddable;
            for (int i = 0; i < allOrbiters.Length; i++)
            {
                if (orbitersInOrder.Count > 0)
                {

                    for (int j = 0; j < orbitersInOrder.Count; j++)
                    {
                        if (allOrbiters[i] != orbitersInOrder[j])
                        {
                            if (Vector3.Distance(allOrbiters[i].transform.GetChild(0).transform.position, transform.position) < Vector3.Distance(newAddable.transform.GetChild(0).transform.position, transform.position))
                            {
                                newAddable = allOrbiters[i];
                            }
                        }
                    }
                }
                else
                {
                    if (Vector3.Distance(allOrbiters[i].transform.GetChild(0).transform.position, transform.position) < Vector3.Distance(newAddable.transform.GetChild(0).transform.position, transform.position))
                    {
                        newAddable = allOrbiters[i];

                    }
                    else if (Vector3.Distance(allOrbiters[i].transform.GetChild(0).transform.position, transform.position) > Vector3.Distance(notNewAddable.transform.GetChild(0).transform.position, transform.position))
                    {
                        notNewAddable = allOrbiters[i];
                    }
                }
            }
            if (newAddable != null)
                orbitersInOrder.Add(newAddable);
        }
        return orbitersInOrder;
    }

    public void LoadGame()
    {

        gameData = BinarySerialier<GameData, string>.LoadData("SaveSlot" + gameNumber);

        ReadGameData(gameData);

    }

    void ReadGameData(GameData data)
    {
        ApplyZRotations(data.zrotations);
        ApplyPlayerPosition(data.playerData.xPos, data.playerData.yPos);
    }
   
}
[System.Serializable]
public class GameData
{
    public string gameName;
    public PlayerData playerData;
    public ProgressData progressData;
    public float[] zrotations;

    public GameData(PlayerData _playerData, ProgressData _progressData, int saveSlot, float[] _zrotations)
    {
        playerData = _playerData;
        progressData = _progressData;
        gameName = "SaveSlot" + saveSlot;
        zrotations = _zrotations;
    }
}
[System.Serializable]
public class PlayerData
{
    public float xPos, yPos;

    public PlayerData(float _xPos, float _yPos)
    {
        xPos = _xPos;
        yPos = _yPos;
    }
}
[System.Serializable]
public class ProgressData
{
    bool[] questCompletion;
    bool[] eventsFired;
    bool eventActive;
    int eventIndex;
}

[System.Serializable]
public class Event
{

}

