using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBoard : MonoBehaviour {

	public AudioSource cave;//
	public AudioSource wind;//
	public AudioSource sea;//
	public AudioSource stream;//
	public AudioSource bambiAttack;//
	public AudioSource boulderDrop;//
	public AudioSource bowHit;//
	public AudioSource bowShoot;//
	public AudioSource chestOpen;//
	public AudioSource clubHit;//
	public AudioSource swing1;//
	public AudioSource swing2;//
	public AudioSource swing3;//
	public AudioSource death;//
	public AudioSource elixir;//
	public AudioSource wispAttack;//
	public AudioSource wispHurt;//
	public AudioSource hurt;//
	public AudioSource jump;//
	public AudioSource drop;//
	//public AudioSource oob1;//-
	public AudioSource oob2;//
	public AudioSource oob3;//
	//public AudioSource boulderPush;//-
	public AudioSource raccoonHurt;//
	public AudioSource roll;//
	//public AudioSource run;//-
	public AudioSource save;//
	public AudioSource weakGround;//
	public AudioSource wolfAttack;//
	public AudioSource wolfHurt;//
	//public AudioSource robot1;
	//public AudioSource robot2;
	//public AudioSource robot3;
	//public AudioSource robot4;
	//public AudioSource robotStand;
	//public AudioSource robotStep1;
	//public AudioSource robotStep2;
	public AudioSource robotStep3;
	//public AudioSource robotStep4;
	//public AudioSource robotStomp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Play(string name) {
		if(name == "cave") cave.Play();
		if(name == "wind") wind.Play();
		if(name == "sea") sea.Play();
		if(name == "stream") stream.Play();
		if(name == "bambiAttack") bambiAttack.Play();
		if(name == "boulderDrop") boulderDrop.Play();
		if(name == "bowHit") bowHit.Play();
		if(name == "bowShoot") bowShoot.Play();
		if(name == "chestOpen") chestOpen.Play();
		if(name == "clubHit") clubHit.Play();
		if(name == "swing1") swing1.Play();
		if(name == "swing2") swing2.Play();
		if(name == "swing3") swing3.Play();
		if(name == "death") death.Play();
		if(name == "elixir") elixir.Play();
		if(name == "wispAttack") wispAttack.Play();
		if(name == "wispHurt") wispHurt.Play();
		if(name == "hurt") hurt.Play();
		if(name == "jump") jump.Play();
		if(name == "drop") drop.Play();
		//if(name == "oob1") oob1.Play();
		if(name == "oob2") oob2.Play();
		if(name == "oob3") oob3.Play();
		//if(name == "boulderPush") boulderPush.Play();
		if(name == "raccoonHurt") raccoonHurt.Play();
		if(name == "roll") roll.Play();
		//if(name == "run") run.Play();
		if(name == "save") save.Play();
		if(name == "weakGround") weakGround.Play();
		if(name == "wolfAttack") wolfAttack.Play();
		if(name == "wolfHurt") wolfHurt.Play();
		//if(name == "robot1") robot1.Play();
		//if(name == "robot2") robot2.Play();
		//if(name == "robot3") robot3.Play();
		//if(name == "robot4") robot4.Play();
		//if(name == "robotStand") robotStand.Play();
		//if(name == "robotStep1") robotStep1.Play();
		//if(name == "robotStep2") robotStep2.Play();
		if(name == "robotStep3") robotStep3.Play();
		//if(name == "robotStep4") robotStep4.Play();
		//if(name == "robotStomp") robotStomp.Play();
	}
	public void Stop(string name) {
		if(name == "cave") cave.Stop();
		if(name == "wind") wind.Stop();
		if(name == "sea") sea.Stop();
		if(name == "stream") stream.Stop();
		if(name == "bambiAttack") bambiAttack.Stop();
		if(name == "boulderDrop") boulderDrop.Stop();
		if(name == "bowHit") bowHit.Stop();
		if(name == "bowShoot") bowShoot.Stop();
		if(name == "chestOpen") chestOpen.Stop();
		if(name == "clubHit") clubHit.Stop();
		if(name == "swing1") swing1.Stop();
		if(name == "swing2") swing2.Stop();
		if(name == "swing3") swing3.Stop();
		if(name == "death") death.Stop();
		if(name == "elixir") elixir.Stop();
		if(name == "wispAttack") wispAttack.Stop();
		if(name == "wispHurt") wispHurt.Stop();
		if(name == "hurt") hurt.Stop();
		if(name == "jump") jump.Stop();
		if(name == "drop") drop.Stop();
		//if(name == "oob1") oob1.Stop();
		if(name == "oob2") oob2.Stop();
		if(name == "oob3") oob3.Stop();
		//if(name == "boulderPush") boulderPush.Stop();
		if(name == "raccoonHurt") raccoonHurt.Stop();
		if(name == "roll") roll.Stop();
		//if(name == "run") run.Stop();
		if(name == "save") save.Stop();
		if(name == "weakGround") weakGround.Stop();
		if(name == "wolfAttack") wolfAttack.Stop();
		if(name == "wolfHurt") wolfHurt.Stop();
		//if(name == "robot1") robot1.Stop();
		//if(name == "robot2") robot2.Stop();
		//if(name == "robot3") robot3.Stop();
		//if(name == "robot4") robot4.Stop();
		//if(name == "robotStand") robotStand.Stop();
		//if(name == "robotStep1") robotStep1.Stop();
		//if(name == "robotStep2") robotStep2.Stop();
		if(name == "robotStep3") robotStep3.Stop();
		//if(name == "robotStep4") robotStep4.Stop();
		//if(name == "robotStomp") robotStomp.Stop();
	}
	public void StopAll() {
		cave.Stop();
		wind.Stop();
		sea.Stop();
		stream.Stop();
		bambiAttack.Stop();
		boulderDrop.Stop();
		bowHit.Stop();
		bowShoot.Stop();
		chestOpen.Stop();
		clubHit.Stop();
		swing1.Stop();
		swing2.Stop();
		swing3.Stop();
		death.Stop();
		elixir.Stop();
		wispAttack.Stop();
		wispHurt.Stop();
		hurt.Stop();
		jump.Stop();
		drop.Stop();
		//oob1.Stop();
		oob2.Stop();
		oob3.Stop();
		//boulderPush.Stop();
		raccoonHurt.Stop();
		roll.Stop();
		//run.Stop();
		save.Stop();
		weakGround.Stop();
		wolfAttack.Stop();
		wolfHurt.Stop();
		//robot1.Stop();
		//robot2.Stop();
		//robot3.Stop();
		//robot4.Stop();
		//robotStand.Stop();
		//robotStep1.Stop();
		//robotStep2.Stop();
		robotStep3.Stop();
		//robotStep4.Stop();
		//robotStomp.Stop();
	}
}
