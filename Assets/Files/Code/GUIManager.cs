using UnityEngine;
using System.Collections;
//Written by M. Gibson Bethke
public class GUIManager : MonoBehaviour
{
	
	IOManager ioManager;
	
	public GUISkin guiSkin;
	Rect screenArea;
	
	string transactionName = "Transaction Name";
	string transactionAmount = "00.00";
	bool reoccurring = false;
	
	
	void Start ()
	{
		
		screenArea = new Rect (0, 0, Screen.width, Screen.height);
		
		ioManager = GameObject.FindGameObjectWithTag ( "IOManager" ).GetComponent<IOManager> ();
	}
	

	void OnGUI ()
	{
		
		GUI.skin = guiSkin;
	
		GUI.Window ( 0, screenArea, TransactionWindow, "Top of Window" );
	}
	
	
	void TransactionWindow ( int wID )
	{
		
		GUI.Box ( new Rect ( screenArea.width/2 - 310, screenArea.height/2 - 10, 360, 160 ), "" );
		
		transactionName = GUI.TextField ( new Rect ( screenArea.width/2 - 300, screenArea.height/2, 340, 60 ), transactionName, 16);
		transactionAmount = GUI.TextField ( new Rect ( screenArea.width/2 - 300, screenArea.height/2 + 70, 170, 60 ), transactionAmount, 8);
		
		if ( GUI.Button ( new Rect ( screenArea.width/2 + 115, screenArea.height/2, 150, 60 ), "Deposit"))
		{
			
			ioManager.NewTransaction ( "Deposit", transactionName, transactionAmount, reoccurring );
		}
		
		if ( GUI.Button ( new Rect ( screenArea.width/2 + 115, screenArea.height/2 + 70, 150, 60 ), "Withdraw"))
		{
			
			ioManager.NewTransaction ( "Withdraw", transactionName, transactionAmount, reoccurring );
		}
	}
}
