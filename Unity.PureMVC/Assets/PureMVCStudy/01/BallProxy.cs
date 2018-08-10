using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProxy : Proxy
{

    public const string NAME = "BallProxy";

    public V3Info redBallInfo, blueBallInfo;

    private V3Info[] v3Infos;

    public BallProxy() : base(NAME)
    {
        redBallInfo = new V3Info();
        blueBallInfo = new V3Info();
        BallMediator ballMediator = (BallMediator)Facade.RetrieveMediator(BallMediator.NAME);
        redBallInfo.postion = ballMediator.redBall.transform.position;
        blueBallInfo.postion = ballMediator.blueBall.transform.position;
        v3Infos = new V3Info[2];
    }


    /// <summary>
    /// 触发改变位置
    /// </summary>
    /// <param name="redBallTra"></param>
    /// <param name="blueBallTra"></param>
    public void ChangePostion(Transform BallTra1, Transform BallTra2)
    {
        redBallInfo.postion = BallTra1.position;
        blueBallInfo.postion = BallTra2.position;
        v3Infos[0] = redBallInfo;
        v3Infos[1] = blueBallInfo;
        SendNotification(NotificationString.CHANGEPOSTION);

    }

    public void toChangePostion()
    {
        SendNotification(NotificationString.CHANGEDATA, v3Infos);
    }
}
