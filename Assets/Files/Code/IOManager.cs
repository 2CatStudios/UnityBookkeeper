using System;
using System.IO;
using UnityEngine;
using System.Collections;
//Written by M. Gibson Bethke
public class IOManager : MonoBehaviour
{

	static string mac = "/Users/" + Environment.UserName + "/Documents/UnityBookkeeper/";
	static string windows = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\UnityBookkeeper\\";
	string path;
	string logPath;
	
	
	
	void Start ()
	{
		
		if(Environment.OSVersion.ToString().Substring (0, 4) == "Unix")
		{

			path = mac;
		} else
		{

			path = windows;
		}
		
		logPath = path + "Transaction Log.txt";
	}
	
	
	public void NewTransaction ( string transactionType, string transactionName, string transactionAmount, bool reoccurring )
	{
		
		if ( !Directory.Exists ( path ))
			Directory.CreateDirectory ( path );
			
		using ( StreamWriter writer = File.AppendText ( logPath )) 
		{
			
			writer.WriteLine ( DateTime.Today.ToString ( "D" ) + "|" + DateTime.Now.ToString ( "T" ) + "|" + transactionName + "|" + transactionType + "|" + transactionAmount + "|" + "New Total" );
		}
	}
}