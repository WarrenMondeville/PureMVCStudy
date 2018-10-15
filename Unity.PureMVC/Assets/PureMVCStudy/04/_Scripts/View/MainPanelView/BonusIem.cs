using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusIem : MonoBehaviour {

    public BonusModel bonusData;
    public Text Desc;
	public Image img;
	// Use this for initialization
	void Start () {
		
	}
	

    public void UpdateItem(BonusModel model)
    {
		//RandomColor ();

        bonusData = model;
        if(bonusData!=null)
        {
            Desc.text = bonusData.Name + "\n" + bonusData.Reward;
            img.color = bonusData.color;
        }
    }

	private void RandomColor()
	{
		Color[] color = {Color.white,Color.yellow,Color.blue,Color.cyan,Color.gray,Color.green,Color.magenta,Color.red};
		int val = Random.Range (0, color.Length);
		img.color = color [val];

	}
}
