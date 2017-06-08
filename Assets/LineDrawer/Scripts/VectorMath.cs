using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTools;
using UnityEngine.UI;

public class VectorMath : MonoBehaviour {

	public int Mode = 0;

	public string[] ModeNames = new string[] {
		"Add",
		"Substract",
		"Dot Product",
		"Cross Product"
	};

	public Vector3 VectorOne;
	public Vector3 VectorTwo;
	Vector3 VectorOneOrigin;
	Vector3 VectorTwoOrigin;

	public Text VectorOneText;
	public Text VectorTwoText;
	public Text ResultText;
	public Text ModeText;

	public Transform VectorOneHead;
	public Transform VectorTwoHead;
	public Transform VectorThreeHead;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		VRInputDevice ViveLeft = VRInput.GetDevice("ViveLeft");
		VRInputDevice ViveRight = VRInput.GetDevice("ViveRight");

		if(ViveLeft.GetButtonDown("Trigger")) {
			VectorOneOrigin = ViveLeft.position;
		}
		if(ViveLeft.GetButton("Trigger")) {
			VectorOne = ViveLeft.position - VectorOneOrigin;
		}

		if(ViveRight.GetButtonDown("Trigger")) {
			VectorTwoOrigin = ViveRight.position;
		}
		if(ViveRight.GetButton("Trigger")) {
			VectorTwo = ViveRight.position - VectorTwoOrigin;
		}

		DrawVector(VectorOne, VectorOneOrigin, Color.red);
		VectorOneHead.position = VectorOneOrigin + VectorOne;
		VectorOneHead.rotation = Quaternion.LookRotation(VectorOne);

		/*
		DrawVector(VectorTwo, VectorTwoOrigin, Color.blue);
		VectorTwoHead.position = VectorTwoOrigin + VectorTwo;
		VectorTwoHead.rotation = Quaternion.LookRotation(VectorTwo);
		*/

		switch(Mode) {
		case 0:
			//VRDebug.DrawLine(new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f) + (VectorOne + VectorTwo), Color.green);
			VRDebug.DrawLine(VectorOneOrigin, VectorOneOrigin + (VectorOne + VectorTwo), Color.green);
			VectorThreeHead.position = VectorOneOrigin + (VectorOne + VectorTwo);
			VectorThreeHead.rotation = Quaternion.LookRotation((VectorOne + VectorTwo));

			DrawVector(VectorTwo, VectorOneOrigin + VectorOne, Color.blue);
			VectorTwoHead.position = VectorOneOrigin + VectorOne + VectorTwo;
			VectorTwoHead.rotation = Quaternion.LookRotation(VectorTwo);

			ResultText.text = "A + B = " + (VectorOne + VectorTwo);
			break;
		case 1:
			VRDebug.DrawLine(VectorOneOrigin + VectorTwo, VectorOneOrigin + VectorTwo + (VectorOne - VectorTwo), Color.green);
			VectorThreeHead.position = VectorOneOrigin + VectorTwo + (VectorOne - VectorTwo);
			VectorThreeHead.rotation = Quaternion.LookRotation((VectorOne - VectorTwo));

			DrawVector(VectorTwo, VectorOneOrigin, Color.blue);
			VectorTwoHead.position = VectorOneOrigin + VectorTwo;
			VectorTwoHead.rotation = Quaternion.LookRotation(VectorTwo);

			ResultText.text = "A - B = " + (VectorOne - VectorTwo);
			break;
		case 2:
			ResultText.text = "A · B = " + (Mathf.Round(Vector3.Dot(VectorOne, VectorTwo) * 100f) / 100f);
			VectorThreeHead.position = new Vector3(0, -1, 0);

			DrawVector(VectorTwo, VectorOneOrigin, Color.blue);
			VectorTwoHead.position = VectorOneOrigin + VectorTwo;
			VectorTwoHead.rotation = Quaternion.LookRotation(VectorTwo);

			break;
		case 3:
			VRDebug.DrawLine(VectorOneOrigin, VectorOneOrigin + Vector3.Cross(VectorOne, VectorTwo), Color.green);
			VectorThreeHead.position = VectorOneOrigin + Vector3.Cross(VectorOne, VectorTwo);
			VectorThreeHead.rotation = Quaternion.LookRotation(Vector3.Cross(VectorOne, VectorTwo));

			DrawVector(VectorTwo, VectorOneOrigin, Color.blue);
			VectorTwoHead.position = VectorOneOrigin + VectorTwo;
			VectorTwoHead.rotation = Quaternion.LookRotation(VectorTwo);

			ResultText.text = "A x B = " + Vector3.Cross(VectorOne, Vector3.Cross(VectorOne, VectorTwo));
			break;
		}



		VectorOneText.text = VectorOne.ToString();
		VectorOneText.transform.parent.position = VectorOneOrigin + new Vector3(0f, -0.1f, 0f);
		VectorTwoText.text = VectorTwo.ToString();
		VectorTwoText.transform.parent.position = VectorTwoOrigin + new Vector3(0f, -0.1f, 0f);
		ModeText.text = ModeNames[Mode];

		if(ViveLeft.GetButtonDown("Touchpad") || ViveRight.GetButtonDown("Touchpad")) {
			Mode++;
			if(Mode >= ModeNames.Length) Mode = 0;
		}
	}

	public void DrawVector(Vector3 Input, Vector3 Origin, Color inputCol) {
		VRDebug.DrawLine(Origin, Origin + Input, inputCol);
	}
}
