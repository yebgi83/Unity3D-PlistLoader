using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

/* Author : Lee kang-yong (yebgi83@gmail.com) */
public class PlistNode {
	static private Dictionary<string, Func<XmlNode, object>> valueTypes;

	static PlistNode() {
		valueTypes = new Dictionary<string, Func<XmlNode, object>>();
		valueTypes.Add ("string", GetString);
		valueTypes.Add ("real", GetReal);
		valueTypes.Add ("integer", GetInteger);
		valueTypes.Add ("date", GetDate);
		valueTypes.Add ("true", GetTrue);
		valueTypes.Add ("false", GetFalse);
		valueTypes.Add ("data", GetData);
		valueTypes.Add ("array", GetArray);
		valueTypes.Add ("dict", GetDictionary);
	}
	
	static private object GetString(XmlNode valueNode) {
		return valueNode.InnerText;
	}
	
	static private object GetReal(XmlNode valueNode) {
		return Convert.ToSingle (valueNode.InnerText);
	}
	
	static private object GetInteger(XmlNode valueNode) {
		return Convert.ToInt32 (valueNode.InnerText);
	}

	static private object GetDate(XmlNode valueNode) {
		return Convert.ToDateTime (valueNode.InnerText);
	}
	
	static private object GetTrue(XmlNode valueNode) {
		return true;
	}
	
	static private object GetFalse(XmlNode valueNode) {
		return false;
	}
	
	static private object GetData(XmlNode valueNode) {
		return Convert.FromBase64String(valueNode.InnerText);
	}
	
	static private object GetArray(XmlNode valueNode) {
		List<PlistNode> plistNodeList = new List<PlistNode>();
		
		for (int nodeIndex = 0; nodeIndex < valueNode.ChildNodes.Count; nodeIndex += 2) {
			PlistNode plistNode = new PlistNode
			(
				valueNode.ChildNodes[nodeIndex],
				valueNode.ChildNodes[nodeIndex + 1]
			);
			
			plistNodeList.Add (plistNode);
		}
		
		return plistNodeList.ToArray();
	}
	
	static private object GetDictionary(XmlNode valueNode) {
		return new PlistDictionary(valueNode);
	}
	
	private XmlNode keyNode;
	private XmlNode valueNode;
	
	public string Key
	{
		get {
			return keyNode.InnerText;
		}
	}
	
	public object Value
	{
		get {
			return PlistNode.valueTypes[this.valueNode.Name](this.valueNode);
		}
	}
	
	public PlistNode(XmlNode keyNode, XmlNode valueNode) {
		if (IsAvailable(keyNode, valueNode) == false) {
			return;
		}
		
		this.keyNode = keyNode;
		this.valueNode = valueNode;
	}
	
	private bool IsAvailable(XmlNode keyNode, XmlNode valueNode) {
		if (keyNode.Name != "key") {
			return false;
		}
		else {
			if (PlistNode.valueTypes.ContainsKey(valueNode.Name) == true) {
				return true;
			} 
			else {
				return false;
			}
		}
	}
}