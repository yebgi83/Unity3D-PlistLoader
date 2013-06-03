using UnityEngine;

using System;
using System.Collections;
using System.Xml;

public class PlistDocument {
	private PlistDictionary rootDictionary;
	
	public PlistDictionary Root
	{
		get	{
			return GetRoot();
		}
	}
	
	public PlistDocument(string plistText) {
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml (plistText);
		
		if (IsAvailable(xmlDocument) == false) {
			return;
		}
		
		this.rootDictionary = new PlistDictionary(xmlDocument.DocumentElement.FirstChild);
	}
	
	private PlistDictionary GetRoot() {
		return this.rootDictionary;
	}
	
	private bool IsAvailable(XmlDocument xmlDocument) {
		if (xmlDocument.DocumentElement.Name != "plist") {
			return false;
		}
		else {
			return true;
		}
	}
}
