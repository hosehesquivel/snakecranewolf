using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamePlayerCharacter : MonoBehaviour
{
    public string CharacterName;
	public Animator CharacterAnimation;
	
	[SerializeField] string[] stances = new string[4];
	[SerializeField] string[] windups = new string[4];
	[SerializeField] string[] attacks = new string[4];
	[SerializeField] string[] results = new string[4];
	
	
	//[SerializeField] SpriteRenderer character;

    //[SerializeField] Sprite[] stances = new Sprite[4];
    //[SerializeField] Sprite[] attacks = new Sprite[4];
    //[SerializeField] Sprite[] results = new Sprite[4];

    int playerstance = 0;

	//[SerializeField] AnimationClip[] animationclip = new AnimationClip[4];
	

    // Use this for initialization
    void Start()
    {
        //text.gameObject.SetActive(ShowText);

        //character.sprite = stances[0];
		//animation.Animator = animationclip[0];
		//animation.Play();
		CharacterAnimation.SetTrigger(stances[playerstance]);
    }

    // Update is called once per frame
    void Update()
    {

    }
	
	
	void Animation()
	{
		//animation.SetTrigger("Effect");
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

        CharacterAnimation.SetTrigger(stances[playerstance]);
		//character.sprite = stances[playerstance];
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

        CharacterAnimation.SetTrigger(attacks[playerstance]);
		//character.sprite = attacks[playerstance];
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

        CharacterAnimation.SetTrigger(results[playerstance]);
		//character.sprite = results[playerstance];
    }
}
