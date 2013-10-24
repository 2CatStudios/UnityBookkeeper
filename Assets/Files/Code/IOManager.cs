using System;
using System.IO;
using UnityEngine;
using System.Collections;
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
			
			iManager.transactionHistory = File.ReadAllText( logPath ).Split( new string[] { "\r\n", "\n" }, StringSplitOptions.None );
		} else {
			
			UnityEngine.Debug.Log ( "No file could be found" );
		}
	}
	
	
	public void NewTransaction ( string transactionType, string transactionName, string transactionAmount, bool reoccurring )
	{
		
		if ( !Directory.Exists ( path ))
			Directory.CreateDirectory ( path );
			
		double newBalance;
		if ( transactionType == "Deposit" )
			newBalance = iManager.balance + double.Parse ( transactionAmount );
		else
			newBalance = iManager.balance - double.Parse ( transactionAmount );
			
		UnityEngine.Debug.Log ( newBalance );
		iManager.balance = newBalance;

		if ( File.Exists ( logPath ))
		{
			
			using ( StreamWriter writer = File.AppendText ( logPath )) 
			{
				
				writer.Write ( "\n" + DateTime.Today.ToString ( "D" ) + "|" + DateTime.Now.ToString ( "T" ) + "|" + transactionName + "|" + transactionType + "|" + transactionAmount + "|" + newBalance );
			}
		} else {
			
			using ( StreamWriter writer = File.AppendText ( logPath )) 
			{
			
				writer.Write ( DateTime.Today.ToString ( "D" ) + "|" + DateTime.Now.ToString ( "T" ) + "|" + transactionName + "|" + transactionType + "|" + transactionAmount + "|" + transactionAmount );
			}
		}
	}
}