using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextProcessor : MonoBehaviour {

	public static string ConvertFromNewLine(string text) {
		return text.Replace("\n", "#$");
	}
	public static string ConvertToNewLine(string text) {
		return text.Replace("#$", "\n");
	}

	public void Test(){
		string converted = ConvertFromNewLine(GetComponent<TMP_InputField>().text);
		Debug.LogWarning(converted);
		Debug.LogWarning(ConvertToNewLine(converted));
	}
}
