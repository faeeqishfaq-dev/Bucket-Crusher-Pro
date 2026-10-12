using UnityEngine;

namespace Voodoo.Utils.Ease
{
	public static class CurveEasing
	{
		public static AnimationCurve GenerateAnimationCurve(Ease easing)
		{
			return null;
		}

		private static AnimationCurve CreateInCurve(Keyframe[] inKeyframes)
		{
			return null;
		}

		private static AnimationCurve CreateOutCurve(Keyframe[] inKeyframes)
		{
			return null;
		}

		private static AnimationCurve CreateInOutCurve(Keyframe[] inKeyframes)
		{
			return null;
		}

		private static Keyframe[] GetInQuarticKeyframes()
		{
			return null;
		}

		private static Keyframe[] GetInQuinticKeyframes()
		{
			return null;
		}

		private static Keyframe[] GetInExponentialKeyframes()
		{
			return null;
		}

		private static Keyframe[] GetInElasticKeyframes()
		{
			return null;
		}

		private static Keyframe[] GetInBounceKeyframes()
		{
			return null;
		}

		private static Keyframe[] GetSpringKeyframes()
		{
			return null;
		}

		private static Keyframe[] GetBezierKeyFrames(params Vector2[] points)
		{
			return null;
		}

		private static void MoveAndScaleKeyframes(Keyframe[] keyframes, Vector2 move, float scale)
		{
		}

		private static void RotateKeyframes(Keyframe[] keyframes)
		{
		}

		private static void UpdateSegment(ref Keyframe left, ref Keyframe right, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
		{
		}

		private static float GetTangent(float x, float y)
		{
			return 0f;
		}

		private static float GetTangent(Vector2 v)
		{
			return 0f;
		}

		private static Vector2 P(float x, float y)
		{
			return default(Vector2);
		}
	}
}
