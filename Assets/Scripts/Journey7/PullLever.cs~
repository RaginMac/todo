using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullLever : MonoBehaviour {

	public PlaceValueAddition pva;
	public int counterValOne, counterValTen, counterValHun;
	public GameObject leverOne, leverTen, leverHun;

	public void OnMouseDown(){
		
		if(this.gameObject.tag=="Drop1"){
			this.GetComponent<Animator>().SetTrigger("OneLever");
			if(counterValOne!=pva.N2D1){
				print("this1");
				if(!pva.hasOnesShifted){
					pva.SetDropLocation(this.gameObject.tag, pva.spawnedOneCoins, pva.dropIndexOne, pva.coinOneParent, pva.coinTenParent, pva.coinHunParent);
				}else{
					pva.SetDropLocation(this.gameObject.tag, pva.spawnedOneCoins, pva.dropIndexOne, pva.newCoinOneParent, pva.newCoinTenParent, pva.coinHunParent);
				}
				UpdateCounter(this.transform.GetComponentInChildren<TextMesh>(), counterValOne, this.gameObject.tag);
			}
			else{
				this.GetComponent<BoxCollider>().enabled = false;
			}
		}
		else if(this.gameObject.tag=="Drop10"){
			this.GetComponent<Animator>().SetTrigger("TenLever");
			if(counterValTen!=pva.N2D2){
				if(!pva.hasTensShifted){
					pva.SetDropLocation(this.gameObject.tag, pva.spawnedTenCoins, pva.dropIndexTen, pva.coinOneParent, pva.coinTenParent, pva.coinHunParent);
				}else{
					
					pva.SetDropLocation(this.gameObject.tag, pva.spawnedTenCoins, pva.dropIndexTen, pva.newCoinOneParent, pva.newCoinTenParent, pva.coinHunParent);
				}
				UpdateCounter(this.transform.GetComponentInChildren<TextMesh>(), counterValTen, this.gameObject.tag);
			}
			else{
				this.GetComponent<BoxCollider>().enabled = false;
			}
		}
		else if(this.gameObject.tag=="Drop100"){
			this.GetComponent<Animator>().SetTrigger("HunLever");
			if(counterValHun!=pva.N2D3){
				pva.SetDropLocation(this.gameObject.tag, pva.spawnedHunCoins, pva.dropIndexHun, pva.coinOneParent, pva.coinTenParent, pva.coinHunParent);
				UpdateCounter(this.transform.GetComponentInChildren<TextMesh>(), counterValHun, this.gameObject.tag);
			}
			else{
				this.GetComponent<BoxCollider>().enabled = false;
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

		CheckDropLimit();

		text.text = whichCounter.ToString();

	}

	void CheckDropLimit(){
		if(counterValOne==pva.N2D1 && (pva.diff == PlaceValueAddition.Difficulty.Grade2 || pva.diff == PlaceValueAddition.Difficulty.Grade3)){
			leverTen.GetComponent<BoxCollider>().enabled = true;
		}

		if(counterValTen==pva.N2D2 && (pva.diff == PlaceValueAddition.Difficulty.Grade3)){
			leverHun.GetComponent<BoxCollider>().enabled = true;
		}
	}
}
