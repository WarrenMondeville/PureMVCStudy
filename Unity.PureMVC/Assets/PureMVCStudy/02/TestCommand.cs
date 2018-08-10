
using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

public class TestCommand : SimpleCommand
{

    public new const string NAME = "TestCommand";

    public override void Execute(PureMVC.Interfaces.INotification notification)
    {
        Test02Proxy proxy = (Test02Proxy)Facade.RetrieveProxy(Test02Proxy.NAME);
        proxy.ChangeLevel(1);


    }
}
