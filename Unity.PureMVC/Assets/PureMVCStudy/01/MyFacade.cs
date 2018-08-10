using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFacade : Facade {

    public MyFacade(GameObject game)
    {
        RegisterMediator(new BallMediator(game));
        RegisterProxy(new BallProxy());
        RegisterCommand(NotificationString.CHANGEPOSTION, typeof(BallCommand));

    }
}
