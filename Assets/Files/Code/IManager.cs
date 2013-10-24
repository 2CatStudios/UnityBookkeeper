using System;
using UnityEngine;
using System.Collections;
//Written by M. Gibson Bethke
public class IManager : MonoBehaviour
{

	internal double balance = 0.0F;
	internal string[] transactionHistory;
	
	
	void Start ()
	{
		
		if ( transactionHistory != null)
		{
		
			string[] lastBalance = transactionHistory[transactionHistory.Length - 1].Split('|');
			balance = double.Parse ( lastBalance [5] );
		}
	}
}