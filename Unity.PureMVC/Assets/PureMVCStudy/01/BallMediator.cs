using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
using PureMVC.Patterns;

public class BallMediator : Mediator
{
    public const string NAME = "BallMediator";

    public GameObject redBall, blueBall;

    public float moveTime = 0.5f;

    public BallMediator(GameObject mainGameObject) : base(NAME)
    {
        redBall = mainGameObject.transform.Find("redBall").gameObject;
        blueBall = mainGameObject.transform.Find("blueBall").gameObject;
    }

    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>();
        list.Add(NotificationString.CHANGEDATA);
        return list;
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);
        if (notification.Name.Equals(NotificationString.CHANGEDATA))
        {
            V3Info[] v3Infos = (V3Info[])notification.Body;
            if (redBall.transform.position == v3Infos[0].postion)
            {
                
                redBall.transform.position = Vector3.Slerp(redBall.transform.position, v3Infos[1].postion, moveTime);
                blueBall.transform.position = Vector3.Slerp(blueBall.transform.position, v3Infos[0].postion, moveTime);
            }
            else
            {
                redBall.transform.position = Vector3.Slerp(redBall.transform.position, v3Infos[0].postion, moveTime);
                blueBall.transform.position = Vector3.Slerp(blueBall.transform.position, v3Infos[1].postion, moveTime);
            }
        }
    }



    IEnumerator IMove(Transform trans,Vector3 target,float t)
    {
        float ti = 0;
        while (ti<t)
        {
            ti += Time.deltaTime;
            trans.position = Vector3.Slerp(trans.position, target, ti / t);
            yield return null;
        }

    }
}
