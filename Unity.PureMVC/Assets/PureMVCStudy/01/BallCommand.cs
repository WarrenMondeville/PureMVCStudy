using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCommand : SimpleCommand {

    public const string NAME = "BallCommand";

    public BallCommand()
    {


    }

    public override void Execute(INotification notification)
    {
        if (notification.Name.Equals(NotificationString.CHANGEPOSTION))
        {
            BallProxy ballProxy = (BallProxy)Facade.RetrieveProxy(BallProxy.NAME);
            ballProxy.toChangePostion();
        }
    }
}
