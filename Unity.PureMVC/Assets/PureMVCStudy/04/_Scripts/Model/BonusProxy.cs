using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
public class BonusProxy : PureMVC.Patterns.Proxy  {
    
    //奖励列表
    public List<BonusModel> BonusLists = new List<BonusModel>();

    public new static string NAME = "Bonus";

    private static string[] REWARD_NAME = new string[]{ "金创药", "薄荷叶", "橡木果", "圣灵药", "还魂丹", "解毒药" };
    private static int[] REWARD_PRICE = new int[]{100,300,500,800,1200 ,400};
    private static Color[] REWARD_COLOR = { Color.yellow, Color.blue, Color.cyan, Color.green, Color.magenta, Color.red };

    public BonusProxy(string proxyName)
		: base(proxyName)
	{
        Debug.Log("bonusProxy create");
    }

    public void AddBonus(BonusModel bonus)
    {
        BonusLists.Add(bonus);
    }

    public void Clear()
    {
        BonusLists.Clear();
    }

    /// <summary>
    /// 创建奖池
    /// </summary>
    public void CreateRewardPool(int count)
    {
        for(int i=0;i<count;++i)
        {
            //int id = 0;
            int index = Random.Range(0, REWARD_NAME.Length);
            string name = REWARD_NAME[index];
            int price = REWARD_PRICE[index];
            Color color = REWARD_COLOR[index];
            BonusModel model = new BonusModel(i+1, name, price,color);
            BonusLists.Add(model);
        }
        
        //更新UI的通知也可以在这里写
        

    }

    /// <summary>
    /// 注册成功后会自动调用
    /// </summary>
    public override void OnRegister()
    {
        base.OnRegister();
        Debug.Log("BonusProxy OnRegister");
    }

    /// <summary>
    /// Called by the Model when the Proxy is removed
    /// </summary>
    public override void OnRemove()
    {
        base.OnRemove();
        Debug.Log("BonusProxy OnRemove");
    }

}
