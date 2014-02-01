using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectGameObject {

	// Current list of selected object
	private static List<ISelectable> selected
		= new List<ISelectable>();	

	public static void Dispatch(GameObject target)
	{

		// Iterate over Selectable components
		Component[] selectableComponents 
			= target.GetComponents(typeof(ISelectable));
		
		for (int i = 0; i<selectableComponents.Length; i++){
			// If the class inherits selectable inteface
			if (selectableComponents[i] is ISelectable){
				ISelectable selectableObject = selectableComponents[i] as ISelectable;
				
				ISelectable alreadySelected = null;
				// call OnSelect method
				// First check if it's there alread
				// We don't want to select it twice, do we?
				if ( selected.Contains(selectableObject) == false) {
					selectableObject.OnSelect();
					// Adding it to the list of selected object
					selected.Add(selectableObject);
					alreadySelected = selectableObject;
				} else {
					alreadySelected = selectableObject;
				}
				// In future, we may allow groups to be selected
				// But for now, let's just reset em all
				for (int a = 0; a<selected.Count; a++){
					ISelectable cSelected = selected[a];
					if ( alreadySelected != cSelected){
						cSelected.OnDeselect();
						selected.Remove(cSelected);
					}
				}
			}
		}
	}
}
