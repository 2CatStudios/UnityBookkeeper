using System;
using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
//Written by M. Gibson Bethke
public class GUIManager : MonoBehaviour
{
	
	IManager iManager;
	IOManager ioManager;
	
	public Texture2D line;
	
	public GUISkin guiSkin;
	internal Rect screenArea;
	
	internal Rect transactionWindowPosition;
	internal Rect historyWindowPosition;
	
	string transactionName = "Transaction Name";
	string transactionAmount = "000.00";
	
	Vector2 scrollPosition = Vector2.zero;
	
	internal int pane = 0;
	
	public static string RemoveDigits ( string key )
   	{
				
		return Regex.Replace ( key, "[^0-9.]", "" );
  	 }
	
	
	void Start ()
	{
		
		screenArea = new Rect (0, 0, Screen.width, Screen.height);
		
		iManager = GameObject.FindGameObjectWithTag ( "IManager" ).GetComponent<IManager> ();
		ioManager = GameObject.FindGameObjectWithTag ( "IOManager" ).GetComponent<IOManager> ();
		
		transactionWindowPosition = new Rect ( screenArea.x, screenArea.y + 100, screenArea.width, screenArea.height - 100 );
		historyWindowPosition = new Rect ( screenArea.x, -screenArea.height + 100, screenArea.width, screenArea.height );
		scrollPosition = new Vector2 ( scrollPosition.x, Mathf.Infinity );
	}
	

	void OnGUI ()
	{
		
		GUI.skin = guiSkin;
	
		GUI.Window ( 0, transactionWindowPosition, TransactionWindow, "" );
		GUI.Window ( 1, historyWindowPosition, HistoryWindow, "" );
		
		if ( pane == 0 )
		{
			
			GUI.FocusWindow ( 0 );
		} else if ( pane == 1 )
		{
			
			GUI.FocusWindow ( 1 );
		}
	}
	
	
	void TransactionWindow ( int wID )
	{
		
		GUI.skin.label.fontSize = 40;
		GUI.Label ( new Rect ( screenArea.width/2 - 300, screenArea.height/2 - 200, 600, 60 ), "Current Balance: $" + iManager.balance.ToString ( "000.00" ));
		
		GUI.Box ( new Rect ( screenArea.width/2 - 310, screenArea.height/2, 360, 160 ), "" );	
		transactionName = GUI.TextField ( new Rect ( screenArea.width/2 - 300, screenArea.height/2 + 10, 340, 60 ), transactionName.TrimStart (), 16);
		transactionAmount = GUI.TextField ( new Rect ( screenArea.width/2 - 300, screenArea.height/2 + 95, 170, 60 ), transactionAmount.Trim (), 8);
		transactionAmount = RemoveDigits ( transactionAmount );
		
		if ( GUI.Button ( new Rect ( screenArea.width/2 + 115, screenArea.height/2 + 15, 150, 60 ), "Deposit" ))
		{
			
			ioManager.NewTransaction ( "Deposit", transactionName, transactionAmount );
			
			transactionName = "Transaction Name";
			transactionAmount = "000.00";
			
			scrollPosition = new Vector2 ( scrollPosition.x, Mathf.Infinity );
		}
		
		if ( GUI.Button ( new Rect ( screenArea.width/2 + 115, screenArea.height/2 + 85, 150, 60 ), "Withdraw" ))
		{
			
			ioManager.NewTransaction ( "Withdraw", transactionName, transactionAmount );			
			transactionName = "Transaction Name";
			transactionAmount = "000.00";
			
			scrollPosition = new Vector2 ( scrollPosition.x, Mathf.Infinity );
		}
	}
	
	
	void HistoryWindow ( int wID )
	{
		
		if ( iManager.transactionHistory != null )
		{
		
			if ( GUI.Button ( new Rect ( screenArea.width / 2 - 75, 10, 150, 40 ), "Clear Log" ))
			{
				
				iManager.SendMessage ( "ClearLog" );
			}
			
			GUILayout.BeginHorizontal ();
			GUILayout.Space ( screenArea.width / 2 - 375 );
			GUILayout.BeginVertical ();
			GUILayout.Space ( 50 );
			scrollPosition = GUILayout.BeginScrollView ( scrollPosition, GUILayout.Width( 750 ), GUILayout.Height ( 500 ));
			
			GUILayout.FlexibleSpace ();
			
			GUI.skin.label.fontSize = 16;
			for ( int i = 0; i < iManager.transactionHistory.Count; i += 1 )
			{
				
				GUILayout.Box ( iManager.transactionHistory[i] );
			}
			GUI.skin.label.fontSize = 40;
			
			GUI.EndScrollView();
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			
			GUI.DrawTexture ( new Rect ( 0, screenArea.height - 35, screenArea.width, 10 ), line, ScaleMode.StretchToFill );
		}
	}
}
