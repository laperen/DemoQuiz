using System;
using System.Collections.Generic;
using UnityEngine;

namespace DemoQuiz {
	public class StringStore : MonoBehaviour {
		public List<StringEventLink> links;

		public void OnClick(int index) {
			links[index].InvokeEvent();
		}
		public void AddLink(string val, Action<string> act) {
			if (null == links) {
				links = new List<StringEventLink>();
			}
			StringEventLink sel = new StringEventLink {
				value = val,
				evt = act
			};
			links.Add(sel);
		}
	}
}