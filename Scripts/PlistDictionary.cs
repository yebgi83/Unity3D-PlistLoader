using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

/* Author : Lee kang-yong (yebgi83@gmail.com) */
public class PlistDictionary {
	private Dictionary<string, PlistNode> dictionaryNode;
	
	public PlistNode this[string key]
	{
		get {
			return GetNode(key);
		}
	}
	
	public PlistDictionary(XmlNode dictionaryNode) {
		this.dictionaryNode = new Dictionary<string, PlistNode>();
		
		for (int nodeIndex = 0; nodeIndex < dictionaryNode.ChildNodes.Count; nodeIndex += 2) {
			PlistNode plistNode = new PlistNode
			(
				dictionaryNode.ChildNodes[nodeIndex],
				dictionaryNode.ChildNodes[nodeIndex + 1]
			);
			
			this.dictionaryNode.Add (plistNode.Key, plistNode);
		}
	}
	
	public IEnumerator GetEnumerator() {
		return dictionaryNode.Values.GetEnumerator();
	}

	public PlistNode GetNode(string key) {
		if (this.dictionaryNode.ContainsKey(key) == true) {
			return this.dictionaryNode[key];
		} 
		else {
			return null;
		}
	}
}
