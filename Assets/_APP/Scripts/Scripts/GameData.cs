using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum GenderEnum
{
    Male,
    Female
}
[CreateAssetMenu(menuName = "Scriptable Objects/Game/GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] private string _firstName;
    [SerializeField] private string _lastName;
    [SerializeField] private string _email;
    [SerializeField] private string _phoneNumber;
    [SerializeField] private string _folderPath;
    [SerializeField] private string _postProfile;
    [SerializeField] private GenderEnum _playerGender;

    #region Readin & Writing over data

    public string FirstName
    {
        get => _firstName;
        set => _firstName = value;
    }

    public string LastName
    {
        get => _lastName;
        set => _lastName = value;
    }

    public string Email
    {
        get => _email;
        set => _email = value;
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set => _phoneNumber = value;
    }



    public string FolderPath
    {
        get => _folderPath;
        set => _folderPath = value;
    }

    public string PostProfile
    {
        get => _postProfile;
        set => _postProfile = value;
    }

    public GenderEnum playerGender
    {
        get => _playerGender;
        set => _playerGender = value;
    }

    #endregion

    public void SaveGameData()
    {
        string filePath = Path.Combine(_folderPath, $"{_firstName + " " + _lastName}.txt");

        using StreamWriter writer = new StreamWriter(filePath);
        writer.WriteLine($"firstname: {FirstName}");
        writer.WriteLine($"lastname: {LastName}");
        writer.WriteLine($"");
        writer.WriteLine($"email: {Email}");
        writer.WriteLine($"phonenumber: {PhoneNumber}");
        writer.Close();
    }

    public void GenerateFolderPath()
    {
        _folderPath = Application.dataPath + "/Fujairah VR Interview" + $"/{_firstName + " " + _lastName} " +
                      DateTime.UtcNow.ToString("yyyy_MM_dd HH_mm_ss_ffff");
        if (!Directory.Exists(_folderPath))
        {
            // If not, create it
            Directory.CreateDirectory(_folderPath);
            Debug.Log("Created 'Recordings' folder.");
        }
    }
}