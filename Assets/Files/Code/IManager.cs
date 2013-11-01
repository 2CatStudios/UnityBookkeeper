using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Written by M. Gibson Bethke
public class IManager : MonoBehaviour
{
	
	IOManager ioManager;
	
	internal double balance = 0.0F;
	internal List<String> transactionHistory;

	
	void Start ()
	{
		
		ioManager = GameObject.FindGameObjectWithTag ( "IOManager" ).GetComponent<IOManager> ();
	}
	
	
	internal void ReadLog ()
	{
		
		if ( transactionHistory != null)
		{
		
			string[] lastBalance = transactionHistory[transactionHistory.Count - 1].Split('|');
			balance = double.Parse ( lastBalance [5] );
		}
	}
	
	
	void ClearLog ()
	{
		
		if ( transactionHistory.Count > 2 )
		{
			
			transactionHistory.RemoveRange ( 0, transactionHistory.Count - 2 );
			
			ioManager.ClearLog ();
		}
	}
}