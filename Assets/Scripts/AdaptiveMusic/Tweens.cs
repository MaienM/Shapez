using UnityEngine;
using System.Collections;

public class Tweens : MonoBehaviour {

	/// <summary>
	/// Easing equation function for a quintic (t^5) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// Borrowed from http://wpf-animation.googlecode.com/svn/trunk/src/WPF/Animation/PennerDoubleAnimation.cs
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float InOutQuint( float t, float b, float c, float d ) {
		if ( ( t /= (d / 2f) ) < 1f )
			return (c-b) / 2f * t * t * t * t * t + b;
		else
			return (c-b) / 2f * ( ( t -= 2f ) * t * t * t * t + 2f ) + b;
	}
}
