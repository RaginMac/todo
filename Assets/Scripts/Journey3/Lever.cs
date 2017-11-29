using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour {

//	public int leverValue;
//	public bool leverPulled;
//	public TextMesh counter;


	//public PlaceValueAddition pva;
	public TestJ3 pva;
	public int counterValOne, counterValTen, counterValHun;
	public GameObject leverOne, leverTen, leverHun;

	public void OnMouseDown(){

		if(this.gameObject.tag=="Drop1"){
			this.GetComponent<Animator>().SetTrigger("OneLever");
			if(counterValOne<9){
				//if(!pva.hasOnesShifted)
				{
					pva.SetDropLocation(this.gameObject.tag, pva.spawnedOneCoins, pva.dropIndexOne, pva.coinOneParent, pva.coinTenParent, pva.coinHunParent);
				}
				//				else{
				//					pva.SetDropLocation(this.gameObject.tag, pva.spawnedOneCoins, pva.dropIndexOne, pva.newCoinOneParent, pva.newCoinTenParent, pva.coinHunParent);
				//				}
				UpdateCounter(this.transform.GetComponentInChildren<TextMesh>(), counterValOne, this.gameObject.tag);
			}
		}
		else if(this.gameObject.tag=="Drop10"){
			this.GetComponent<Animator>().SetTrigger("TenLever");
			if(counterValTen<9){
				//if(!pva.hasTensShifted)
				{
					pva.SetDropLocation(this.gameObject.tag, pva.spawnedTenCoins, pva.dropIndexTen, pva.coinOneParent, pva.coinTenParent, pva.coinHunParent);
				}
				//				else{
				//					pva.SetDropLocation(this.gameObject.tag, pva.spawnedTenCoins, pva.dropIndexTen, pva.newCoinOneParent, pva.newCoinTenParent, pva.coinHunParent);
				//				}
				UpdateCounter(this.transform.GetComponentInChildren<TextMesh>(), counterValTen, this.gameObject.tag);
			}
		}
		else if(this.gameObject.tag=="Drop100"){
			this.GetComponent<Animator>().SetTrigger("HunLever");
			if(counterValHun<9)
			{
				pva.SetDropLocation(this.gameObject.tag, pva.spawnedHunCoins, pva.dropIndexHun, pva.coinOneParent, pva.coinTenParent, pva.coinHunParent);
				UpdateCounter(this.transform.GetComponentInChildren<TextMesh>(), counterValHun, this.gameObject.tag);
			}
		}

	}

	void UpdateCounter(TextMesh text, int whichCounter, string tag){

		//print(whichCounter.ToString());
		whichCounter++;
		if(tag=="Drop1"){
			counterValOne = whichCounter;
		}else if(tag=="Drop10"){
			counterValTen = whichCounter;
		}else{
			counterValHun = whichCounter;
		}

		//CheckDropLimit();

		text.text = whichCounter.ToString();

	}

	void CheckDropLimit(){
		if(counterValOne==pva.N1D1 && (pva.diff == TestJ3.Difficulty.Grade2 || pva.diff == TestJ3.Difficulty.Grade3)){
			leverTen.GetComponent<BoxCollider>().enabled = true;
		}

		if(counterValTen==pva.N1D2 && (pva.diff == TestJ3.Difficulty.Grade3)){
			leverHun.GetComponent<BoxCollider>().enabled = true;
		}
	}
}
