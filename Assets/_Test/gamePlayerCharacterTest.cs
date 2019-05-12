using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamePlayerCharacterTest : MonoBehaviour
{
    public string CharacterName;
	public Animator CharacterAnimation;
	
	[SerializeField] string[] Stances = new string[1];
	[SerializeField] string[] Attacks = new string[4];
	
	public string[] preps;
	
	int playerstance = 0;
	
	// Start is called before the first frame update
    void Start()
    {
		CharacterAnimation.SetTrigger(Stances[2]);
    }

    // Update is called once per frame
    void Update()
    {

    }
	
	public void SetStance(int stance)
    {
        //Debug.Log("A stance was given: " + stance);

        playerstance = stance;

        Stance();
    }
	
	public void Stance()
    {
        switch (playerstance)
        {
            case 0:
                //text.text = "x";
                break;
            case 1:
                //text.text = "r";
                break;
            case 2:
                //text.text = "p";
                break;
            case 3:
                //text.text = "s";
                break;
        }

		CharacterAnimation.SetTrigger(Stances[playerstance]);
    }
	
	public void FinalStance()
    {
        switch (playerstance)
        {
            case 0:
                //text.text = "X";
                break;
            case 1:
                //text.text = "R";
                break;
            case 2:
                //text.text = "P";
                break;
            case 3:
                //text.text = "S";
                break;
        }

        CharacterAnimation.SetTrigger(Stances[playerstance]);
    }
	
	public void Results()
    {
        switch (playerstance)
        {
            case 0:
                //text.text = "X";
                break;
            case 1:
                //text.text = "R";
                break;
            case 2:
                //text.text = "P";
                break;
            case 3:
                //text.text = "S";
                break;
        }

        CharacterAnimation.SetTrigger(Stances[playerstance]);
    }
}
