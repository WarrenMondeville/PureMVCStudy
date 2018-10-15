using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

public class Test02Proxy : Proxy
{

    public new const string NAME = "Test02Proxy";
    public CharacterInfo Data { get; set; }

    public Test02Proxy() : base(NAME)
    {
        Data = new CharacterInfo();
    }

    public void ChangeLevel(int change)
    {
        Data.Level += change;
        SendNotification(NotificationConstant.LevelChange, Data);
        
    }

}

