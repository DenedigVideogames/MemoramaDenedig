using Dan.Main;
using Dan.Models;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private string _leaderboardPublicKey = "";
    [SerializeField] private TextMeshProUGUI _playerScoreText;
    [SerializeField] private TextMeshProUGUI[] _entryFields;

    //[SerializeField] private TMP_InputField _playerUsernameInput;
    public TextMeshProUGUI username;

    [SerializeField] private TextMeshProUGUI _personalEntryText;

    public TimeCount timeCount;
    private int _playerScore;
    private float _playerScore1;

    private void Start()
    {
        Load();
    }

    public void AddPlayerScore()
    {

        _playerScore = (int)timeCount.puntos;
        _playerScore1 = timeCount.puntos;
        _playerScoreText.text = "Your score: " + _playerScore1;

    }

    public void Load() => Leaderboards.DemoSceneLeaderboard.GetEntries(OnLeaderboardLoaded);

    private void OnLeaderboardLoaded(Entry[] entries)
    {
        foreach (var entryField in _entryFields)
        {
            entryField.text = "";
        }

        for (int i = 0; i <= 4; i++)
        {
            _entryFields[i].text = $"{entries[i].Username} : {entries[i].Score}";
        }
    }

    public void Submit()// Sonido boton sube puntuacion 
    {
        //Leaderboards.DemoSceneLeaderboard.UploadNewEntry(_playerUsernameInput.text, _playerScore, Callback, ErrorCallback);
        Leaderboards.DemoSceneLeaderboard.UploadNewEntry(username.text, (int)timeCount.puntos, Callback, ErrorCallback);
        Debug.Log(username.text);
    }

    public void DeleteEntry()// Sonido Resetea boton puntuacion 
    {
        Leaderboards.DemoSceneLeaderboard.DeleteEntry(Callback, ErrorCallback);
    }

    public void ResetPlayer()//Sonido Resetea Puntuacion 
    {
        LeaderboardCreator.ResetPlayer();
    }

    public void GetPersonalEntry()
    {
        Leaderboards.DemoSceneLeaderboard.GetPersonalEntry(OnPersonalEntryLoaded);
    }

    private void OnPersonalEntryLoaded(Entry entry)
    {
        _personalEntryText.text = $"{entry.Rank}. {entry.Username} : {entry.Score}";
    }

    private void Callback(bool success)
    {
        if (success)
            Load();
    }

    private void ErrorCallback(string error)
    {
        Debug.LogError(error);
    }
}