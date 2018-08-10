using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Camera MainCamera;
    private List<Transform> transforms;
    [Range(0, 1)]
    public float movetime;
    void Start()
    {
        new MyFacade(gameObject);
        BallMediator ballMediator = (BallMediator)Facade.Instance.RetrieveMediator(BallMediator.NAME);
        ballMediator.moveTime = movetime;
        transforms = new List<Transform>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                Debug.DrawRay(MainCamera.transform.position, ray.GetPoint(50), Color.red);
                GameObject game = raycastHit.collider.gameObject;
                if (transforms.Count == 0 || game.transform != transforms[0])
                {
                    transforms.Add(game.transform);
                    if (transforms.Count >= 2)
                    {
                        BallProxy ballProxy = (BallProxy)Facade.Instance.RetrieveProxy(BallProxy.NAME);
                        ballProxy.ChangePostion(transforms[0], transforms[1]);
                        transforms.Clear();
                    }
                    Debug.Log(transforms.Count);
                }
            }
        }
    }
}