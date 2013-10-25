using UnityEngine;
using System.Collections;
//Written by M. Gibson Bethke
public class GUIManager : MonoBehaviour
{
	
	IManager iManager;
	IOManager ioManager;
	
	public GUISkin guiSkin;
	internal Rect screenArea;
	
	internal Rect transactionWindowPosition;
	internal Rect historyWindowPosition;
	
	string transactionName = "Transaction Name";
	string transactionAmount = "000.00";
	bool reoccurring = false;
	
	
	void Start ()
	{
		
		screenArea = new Rect (0, 0, Screen.width, Screen.height);
		
		iManager = GameObject.FindGameObjectWithTag ( "IManager" ).GetComponent<IManager> ();
		ioManager = GameObject.FindGameObjectWithTag ( "IOManager" ).GetComponent<IOManager> ();
		
		transactionWindowPosition = new Rect ( screenArea.x, screenArea.y + 100, screenArea.width, screenArea.height - 100 );
		historyWindowPosition = new Rect ( screenArea.x, -screenArea.height + 100, screenArea.width, screenArea.height );
	}
	

	void OnGUI ()
	{
		
		GUI.skin = guiSkin;
	
		GUI.Window ( 0, transactionWindowPosition, TransactionWindow, "" );
		GUI.Window ( 1, historyWindowPosition, HistoryWindow, "" );
	}
	
	
	void TransactionWindow ( int wID )
	{
		
		GUI.Label ( new Rect ( screenArea.width/2 - 300, screenArea.height/2 - 200, 600, 60 ), "Current Balance: " + iManager.balance.ToString ());
		
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
	
	
	void HistoryWindow ( int wID )
	{
		
		
	}
}
