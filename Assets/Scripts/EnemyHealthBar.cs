using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

    /// <summary>
    /// The color of the bar.
    /// </summary>
    public Image enemyBar;

    /// <summary>
    /// The Name of the enemy.
    /// </summary>
    public Text enemyName;
	
   
	void Start () {
		
	}
	
	
	void Update () {
		
	}

    /// <summary>
    /// Setting The name of the Bar.
    /// </summary>
    /// <param name="Name"> Name of the bar</param>
    public void BarGenerateName(string Name) {
        enemyName.text = Name;
    }

    public void BarPosition(Vector3 EnemyPosition) {
        this.transform.position = new Vector3(EnemyPosition.x, EnemyPosition.y + 0.8f, EnemyPosition.z);

    } 
}
