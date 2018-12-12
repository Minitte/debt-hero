using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a class for skill icons.
/// </summary>
[RequireComponent(typeof(Animator))]
public class SkillIcon : MonoBehaviour {

    /// <summary>
    /// The skill object that this icon is representing.
    /// </summary>
    public Skill skill;

    /// <summary>
    /// Reference to the animator.
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// Reference to the cooldown slider component.
    /// </summary>
    private Slider _cooldown;

    /// <summary>
    /// Flag for if the skill is currently on cooldown.
    /// </summary>
    private bool _onCooldown;

    /// <summary>
    /// The end time of the skill cooldown.
    /// </summary>
    private float _cooldownEnd;

	// Use this for initialization
	private void Start () {
        _animator = GetComponent<Animator>();
        _cooldown = transform.GetChild(0).GetComponent<Slider>();
	}

    // Update is called once per frame
    private void Update() {
        if (_onCooldown) {
            if (_cooldownEnd > Time.time) {
                _cooldown.value = (_cooldownEnd - Time.time) / skill.cooldown;
            } else {
                _cooldown.value = 0f;
                _onCooldown = false;
            }
        }
    }

    /// <summary>
    /// Called when this skill is activated by the input manager.
    /// Purely visual effect, does not actually cast the skill.
    /// </summary>
    public void Press() {
        _animator.SetTrigger("Pressed");
    }

    /// <summary>
    /// Starts the cooldown
    /// </summary>
    /// <param name="endTime"></param>
    public void StartCooldown(float endTime) {
        _cooldownEnd = endTime;
        _cooldown.value = 1f;
        _onCooldown = true;
    }
}
