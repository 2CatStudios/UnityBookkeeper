using System.IO;
using UnityEngine;
using System.Collections;
//Written by GibsonBethke
//Jesus, you are awesome!
//Thanks to Jan Heemstra for the idea
public class PaneManager : MonoBehaviour
{
	
	internal GUIManager guiManager;

	internal bool popupBlocking = false;
	
	internal bool loading = true;

	internal enum pane {transactionWindow, historyWindow};
	internal pane currentPane = pane.transactionWindow;

	internal bool moving = false;

	float moveVelocity = 0.0F;

	
	void Start ()
	{
		
		guiManager = GameObject.FindGameObjectWithTag ( "GUIManager" ).GetComponent<GUIManager> ();
	}
	

	void Update()
	{
		
		if ( popupBlocking == false && Input.GetKey ( KeyCode.UpArrow ) && currentPane == pane.historyWindow && moving == false )
		{
			
			moving = true;
		}

		if ( popupBlocking == false && Input.GetKey ( KeyCode.DownArrow ) && currentPane == pane.transactionWindow && moving == false )
		{
			
			moving = true;
		}

			
		if ( currentPane == pane.historyWindow )
		{
			
			if ( moving == true )
			{
				
				float smoothDampIn = Mathf.SmoothDamp ( guiManager.transactionWindowPosition.y, guiManager.screenArea.height - guiManager.transactionWindowPosition.height, ref moveVelocity, 0.1F, 4000 );
				float smoothDampOut = Mathf.SmoothDamp ( guiManager.historyWindowPosition.y, -guiManager.historyWindowPosition.height + 100, ref moveVelocity, 0.1F, 4000 );
		
				guiManager.historyWindowPosition.y = smoothDampOut;
				guiManager.transactionWindowPosition.y = smoothDampIn;
					
				if ( guiManager.transactionWindowPosition.y < ( guiManager.screenArea.height - guiManager.transactionWindowPosition.height ) + 5 )
				{
						
					moveVelocity = 0;
					moving = false;
						
					guiManager.historyWindowPosition.y = -guiManager.historyWindowPosition.height + 100;
					guiManager.transactionWindowPosition.y = guiManager.screenArea.height - guiManager.transactionWindowPosition.height;
						
					currentPane = pane.transactionWindow;
				}
			}
		}
			
		if ( currentPane == pane.transactionWindow )
		{
				
			if ( moving == true )
			{
				
				float smoothDampIn = Mathf.SmoothDamp ( guiManager.historyWindowPosition.y, 0.0F, ref moveVelocity, 0.1F, 4000 );
				float smoothDampOut = Mathf.SmoothDamp ( guiManager.transactionWindowPosition.y, guiManager.screenArea.height, ref moveVelocity, 0.1F, 4000 );
	
				guiManager.transactionWindowPosition.y = smoothDampOut;
				guiManager.historyWindowPosition.y = smoothDampIn;
				
				if ( guiManager.historyWindowPosition.y > -5 )
				{
					
					moveVelocity = 0;
					moving = false;
					
					guiManager.transactionWindowPosition.y = guiManager.screenArea.height;
					guiManager.historyWindowPosition.y = 0;
					
					currentPane = pane.historyWindow;
				}
			}
		}
	}
}