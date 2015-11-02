using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.IO;
using System.Globalization;

public class colour : MonoBehaviour {
	string[,] vals = new string[45000, 19];
	string[] geneIDOnly = new string[45000];
	
	void OnGUI () {

	}
	
	// Use this for initialization
	public void Start()
	{
		// Gets input out of input field
		var input = GameObject.Find("InputFieldWew").GetComponent<InputField> ();
		var se = new InputField.SubmitEvent ();
		se.AddListener (SubmitName);
		input.onEndEdit = se;

		LoadDataset ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	private void SubmitName(string arg0)
	{
		// Pass string into function that can output string
		ColourFromText (arg0);
	}

	public void LoadDataset(){
		// Text file has already had Ensembl Gene IDs replaced
		// Text file has already been sorted
		
		// Open the text file containing the dataset for that piece
		StreamReader reader = new StreamReader (@"/Users/nmt/Documents/Unity/TestColour/Assets/sumAllGenes.txt");
		string myLine;
		char delim = '\t';
		string[] cols;
		
		int rowCount = 0;
		
		// Load dataset into an array
		while (!reader.EndOfStream) {
			myLine = reader.ReadLine ();
			cols = myLine.Split (delim);
			
			// Populate the gene ID column
			vals [rowCount, 0] = cols [0].ToLower();
			// Populate the gene ID only array - for searching!
			geneIDOnly[rowCount] = vals[rowCount,0];
			
			// Populate the array with values
			for (int i = 1; i < cols.Length; i++) {
				vals [rowCount, i] = cols [i];
			}
			rowCount++;
		}
		Debug.Log ("Dataset loaded!");
	}

	public void ColourFromText(string geneName){
		// Search the array for the gene name and get its index (location)
		int found = Array.IndexOf(geneIDOnly, geneName.ToLower());

		// If the gene name was found, go ahead and load that dataset into the pieces
		if (found > 0) {
			//Debug.Log ("found");
			colourHeartPiece ("A_1", float.Parse (vals [found, 1], CultureInfo.InvariantCulture.NumberFormat));
			colourHeartPiece ("A_2", float.Parse (vals [found, 2], CultureInfo.InvariantCulture.NumberFormat));
			colourHeartPiece ("A_3", float.Parse (vals [found, 3], CultureInfo.InvariantCulture.NumberFormat));
			colourHeartPiece ("A_4", float.Parse (vals [found, 4], CultureInfo.InvariantCulture.NumberFormat));

			colourHeartPiece ("B_1", float.Parse (vals [found, 5], CultureInfo.InvariantCulture.NumberFormat));
			colourHeartPiece ("B_2", float.Parse (vals [found, 6], CultureInfo.InvariantCulture.NumberFormat));
			colourHeartPiece ("B_3", float.Parse (vals [found, 7], CultureInfo.InvariantCulture.NumberFormat));
			colourHeartPiece ("B_4", float.Parse (vals [found, 8], CultureInfo.InvariantCulture.NumberFormat));
			
			colourHeartPiece ("C_1", float.Parse (vals [found, 9], CultureInfo.InvariantCulture.NumberFormat));
			colourHeartPiece ("C_2", float.Parse (vals [found, 10], CultureInfo.InvariantCulture.NumberFormat));
			colourHeartPiece ("C_3", float.Parse (vals [found, 11], CultureInfo.InvariantCulture.NumberFormat));
			colourHeartPiece ("C_4", float.Parse (vals [found, 12], CultureInfo.InvariantCulture.NumberFormat));
			
			colourHeartPiece ("D_1", float.Parse (vals [found, 13], CultureInfo.InvariantCulture.NumberFormat));
			colourHeartPiece ("D_2", float.Parse (vals [found, 14], CultureInfo.InvariantCulture.NumberFormat));
			colourHeartPiece ("D_3", float.Parse (vals [found, 15], CultureInfo.InvariantCulture.NumberFormat));
			
			colourHeartPiece ("E_1", float.Parse (vals [found, 16], CultureInfo.InvariantCulture.NumberFormat));
			colourHeartPiece ("E_2", float.Parse (vals [found, 17], CultureInfo.InvariantCulture.NumberFormat));
			colourHeartPiece ("E_3", float.Parse (vals [found, 18], CultureInfo.InvariantCulture.NumberFormat));
		} 
		else
			Debug.Log ("Gene with name " + geneName + " not found.");
	}

	/***
	 *
	 *	Colours heartPiece according to the value of exprVal.
	 *	19 shades of each colour + black.
	 *
	 ***/
	public void colourHeartPiece(string heartPiece, float exprVal){
		float r = 0, b = 0, g = 0;
		float rgb = 255;

		// Red (high expression)
		if (exprVal >= 11)				{	r = 255; g = 0; b = 0; }
		if (exprVal < 11 && exprVal >= 10){	r = 255; g = 28; b = 28; }
		if (exprVal < 10 && exprVal >= 9){	r = 255; g = 56; b = 56; }
		if (exprVal < 9 && exprVal >= 8){	r = 255; g = 85; b = 85; }
		if (exprVal < 8 && exprVal >= 7){	r = 255; g = 113; b = 113; }
		if (exprVal < 7 && exprVal >= 6){	r = 255; g = 141; b = 141; }
		if (exprVal < 6 && exprVal >= 5){	r = 255; g = 170; b = 170; }
		if (exprVal < 5 && exprVal >= 4){	r = 255; g = 198; b = 198; }
		if (exprVal < 4 && exprVal >= 3){	r = 255; g = 226; b = 226; }

		// Blue (low expression)
		if (exprVal < 3 && exprVal >= 2.75){	r = 212; g = 212; b = 255; }
		if (exprVal < 2.75 && exprVal >= 2.5){	r = 170; g = 170; b = 255; }
		if (exprVal < 2.5 && exprVal > 2)	{	r = 127; g = 127; b = 255; }
		if (exprVal <= 2 && exprVal >= 1.5)	{	r = 85; g = 85; b = 255; }
		if (exprVal < 1.5 && exprVal >= 1)	{	r = 42; g = 42; b = 255; }
		if (exprVal < 1)					{	r = 0; g = 0; b = 255; }

		GameObject.Find(heartPiece).GetComponent<Renderer>().material.color = new Color(r/rgb, g/rgb, b/rgb);
	}


	public void resetColour(){
		// Clear input field
		//GameObject.Find ("InputFieldWew").GetComponent<InputField> = "";

		// Resets all heart pieces to white
		GameObject.Find("A_1").GetComponent<Renderer>().material.color = Color.white;
		GameObject.Find("A_2").GetComponent<Renderer>().material.color = Color.white;
		GameObject.Find("A_3").GetComponent<Renderer>().material.color = Color.white;
		GameObject.Find("A_4").GetComponent<Renderer>().material.color = Color.white;
		
		GameObject.Find("B_1").GetComponent<Renderer>().material.color = Color.white;
		GameObject.Find("B_2").GetComponent<Renderer>().material.color = Color.white;
		GameObject.Find("B_3").GetComponent<Renderer>().material.color = Color.white;
		GameObject.Find("B_4").GetComponent<Renderer>().material.color = Color.white;
		
		GameObject.Find("C_1").GetComponent<Renderer>().material.color = Color.white;
		GameObject.Find("C_2").GetComponent<Renderer>().material.color = Color.white;
		GameObject.Find("C_3").GetComponent<Renderer>().material.color = Color.white;
		GameObject.Find("C_4").GetComponent<Renderer>().material.color = Color.white;
		
		GameObject.Find("D_1").GetComponent<Renderer>().material.color = Color.white;
		GameObject.Find("D_2").GetComponent<Renderer>().material.color = Color.white;
		GameObject.Find("D_3").GetComponent<Renderer>().material.color = Color.white;
		
		GameObject.Find("E_1").GetComponent<Renderer>().material.color = Color.white;
		GameObject.Find("E_2").GetComponent<Renderer>().material.color = Color.white;
		GameObject.Find("E_3").GetComponent<Renderer>().material.color = Color.white;
	}

	// Changes slices to distinct colours to show layers
	public void rainbowSlices(){
		GameObject.Find("A_1").GetComponent<Renderer>().material.color = Color.red;
		GameObject.Find("A_2").GetComponent<Renderer>().material.color = Color.red;
		GameObject.Find("A_3").GetComponent<Renderer>().material.color = Color.red;
		GameObject.Find("A_4").GetComponent<Renderer>().material.color = Color.red;
		
		GameObject.Find("B_1").GetComponent<Renderer>().material.color = Color.yellow;
		GameObject.Find("B_2").GetComponent<Renderer>().material.color = Color.yellow;
		GameObject.Find("B_3").GetComponent<Renderer>().material.color = Color.yellow;
		GameObject.Find("B_4").GetComponent<Renderer>().material.color = Color.yellow;
		
		GameObject.Find("C_1").GetComponent<Renderer>().material.color = Color.green;
		GameObject.Find("C_2").GetComponent<Renderer>().material.color = Color.green;
		GameObject.Find("C_3").GetComponent<Renderer>().material.color = Color.green;
		GameObject.Find("C_4").GetComponent<Renderer>().material.color = Color.green;
		
		GameObject.Find("D_1").GetComponent<Renderer>().material.color = Color.blue;
		GameObject.Find("D_2").GetComponent<Renderer>().material.color = Color.blue;
		GameObject.Find("D_3").GetComponent<Renderer>().material.color = Color.blue;
		
		GameObject.Find("E_1").GetComponent<Renderer>().material.color = Color.magenta;
		GameObject.Find("E_2").GetComponent<Renderer>().material.color = Color.magenta;
		GameObject.Find("E_3").GetComponent<Renderer>().material.color = Color.magenta;
	}

	// Changes slices to distinct colours to show layers
	public void rainbowPieces(){
		GameObject.Find ("A_1").GetComponent<Renderer> ().material.color = new Color (1.0F, 0.0F, 0.0F);
		GameObject.Find("A_2").GetComponent<Renderer>().material.color = new Color (1.0F, 0.5F, 0.0F);
		GameObject.Find("A_3").GetComponent<Renderer>().material.color = new Color (1.0F, 1.0F, 0.0F);
		GameObject.Find("A_4").GetComponent<Renderer>().material.color = new Color (0.5F, 1.0F, 0.0F);
		
		GameObject.Find("B_1").GetComponent<Renderer>().material.color = new Color (0.0F, 1.0F, 0.0F);
		GameObject.Find("B_2").GetComponent<Renderer>().material.color = new Color (0.0F, 1.0F, 0.5F);
		GameObject.Find("B_3").GetComponent<Renderer>().material.color = new Color (0.0F, 1.0F, 1.0F);
		GameObject.Find("B_4").GetComponent<Renderer>().material.color = new Color(0.0F, 0.5F, 1.0F);
		
		GameObject.Find("C_1").GetComponent<Renderer>().material.color = new Color (0.0F, 0.0F, 1.0F);
		GameObject.Find("C_2").GetComponent<Renderer>().material.color = new Color (0.5F, 0.0F, 1.0F);
		GameObject.Find("C_3").GetComponent<Renderer>().material.color = new Color (1.0F, 0.0F, 1.0F);
		GameObject.Find("C_4").GetComponent<Renderer>().material.color = new Color (1.0F, 0.0F, 0.5F);
		
		GameObject.Find("D_1").GetComponent<Renderer>().material.color = new Color (255 / 255, 0 / 255, 0 / 255);
		GameObject.Find("D_2").GetComponent<Renderer>().material.color = new Color (1.0F, 0.5F, 0.0F);
		GameObject.Find("D_3").GetComponent<Renderer>().material.color = new Color (1.0F, 1.0F, 0.0F);
		
		GameObject.Find("E_1").GetComponent<Renderer>().material.color = new Color (0.0F, 1.0F, 0.0F);
		GameObject.Find("E_2").GetComponent<Renderer>().material.color = new Color (0.0F, 1.0F, 0.5F);
		GameObject.Find("E_3").GetComponent<Renderer>().material.color = new Color (0.0F, 1.0F, 1.0F);
	}
}
