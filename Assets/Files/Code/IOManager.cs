using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Written by M. Gibson Bethke
public class IOManager : MonoBehaviour
{
	
	IManager iManager;

	static string mac = "/Users/" + Environment.UserName + "/Documents/UnityBookkeeper/";
	static string windows = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\UnityBookkeeper\\";
	string path;
	string logPath;
	
	
	void Start ()
	{
		
		iManager = GameObject.FindGameObjectWithTag ( "IManager" ).GetComponent<IManager>();
		
		if(Environment.OSVersion.ToString().Substring (0, 4) == "Unix")
		{

			path = mac;
		} else
		{

			path = windows;
		}
		
		logPath = path + "Transaction Log.txt";

		
		if ( File.Exists ( logPath ))
		{
			
			iManager.transactionHistory = new List<String> ( File.ReadAllText( logPath ).Split( new string[] { "\r\n", "\n" }, StringSplitOptions.None ));
			iManager.ReadLog ();
		} else {
			
//			UnityEngine.Debug.Log ( "No log file could be found." );
		}
	}
	
	
	internal void NewTransaction ( string transactionType, string transactionName, string transactionAmount )
	{
		
		if ( !Directory.Exists ( path ))
			Directory.CreateDirectory ( path );
			
		double newBalance;
		if ( transactionType == "Deposit" )
			newBalance = iManager.balance + double.Parse ( transactionAmount );
		else
			newBalance = iManager.balance - double.Parse ( transactionAmount );
		
		string firstTransaction = "";
		if ( File.Exists ( logPath ))
			firstTransaction = "\n";
		else
			firstTransaction = "";
		
		using ( StreamWriter writer = File.AppendText ( logPath )) 
		{
				
			writer.Write ( firstTransaction + DateTime.Today.ToString ( "D" ) + "|" + DateTime.Now.ToString ( "T" ) + "|" + transactionName + "|" + transactionType + "|" + transactionAmount + "|" + newBalance );
		}
		
		iManager.transactionHistory = new List<string> ( File.ReadAllText( logPath ).Split( new string[] { "\r\n", "\n" }, StringSplitOptions.None ));
		iManager.ReadLog ();
	}
	
	
	internal void ClearLog ()
	{
		
		if ( File.Exists ( logPath ))
		{
			
			File.Delete ( logPath );
			
			using ( StreamWriter writer = File.AppendText ( logPath )) 
			{
			
				writer.Write ( iManager.transactionHistory[0] );
				writer.Write ( "\n" + iManager.transactionHistory[0] );
			}
			
			iManager.ReadLog ();
		}
	}
}