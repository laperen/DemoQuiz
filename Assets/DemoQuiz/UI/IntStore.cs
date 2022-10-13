using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoQuiz {
	public class IntStore : MonoBehaviour {
		public List<IntEventLink> links;

		public void OnClick(int index) {
			links[index].InvokeEvent();
		}
		public void AddLink(int val, Action<int> act) {
			if (null == links) {
				links = new List<IntEventLink>();
			}
			IntEventLink sel = new IntEventLink {
				value = val,
				evt = act
			};
			links.Add(sel);
		}
	}
}