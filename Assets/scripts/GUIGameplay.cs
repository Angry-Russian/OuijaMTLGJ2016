﻿using UnityEngine;
using System.Collections;

public class GUIGameplay : MonoBehaviour {
    public const int boxHeight=90,boxWidth=70;
    public int offset = 10;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
        int x = 0;
        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        for (int i = 0; i<GameState.Instance.players.Length;++i)
        {
            Player p = GameState.Instance.players[i];
            GUIStyle boxStyle = new GUIStyle();
            string playerName = "";
            Texture2D beardTex = null;
            switch (i)
            {
                case 0:
                    playerName = "Yellow beard";
                    beardTex = Resources.Load<Texture2D>("yellowbeard");
                    break;
                case 1:
                    playerName = "Red beard";
                    beardTex = Resources.Load<Texture2D>("redbeard");
                    break;
                case 2:
                    playerName = "Black beard";
                    beardTex = Resources.Load<Texture2D>("blackbeard");
                    break;
                default:
                    break;
            }
            //beardTex.Resize(10, 10);
            //int offset = (int)((Screen.width  - 3 * boxWidth) / 5f);
			x = (int) (offset + Screen.width / 3 * i);

            GUI.Box(new Rect(x , 0, boxWidth, 90), beardTex, boxStyle);
            
            for (int j = 0;j< p.letters.Length;++j)
            {
                char character = (char) p.letters[j];
                //Debug.Log(character);
                Texture2D rune = Resources.Load<Texture2D>("runic_" + (char)('a' + character));
               // Debug.Log(p.score);
                if (j+1 > p.index)
                {
                    Color temp = GUI.color ;
                    GUI.color = new Color(1f, 1f, 1f, 0.50f);
                    GUI.DrawTexture(new Rect(x + j * 40 + 20 , 50, 40, 40), rune);
                    GUI.color = temp;
                }
                else
                {
                    GUI.DrawTexture(new Rect(x + j * 40 + 20 , 50, 40, 40), rune);
                }

                
            }
            //x += boxWidth;
        }
        
    }
}
