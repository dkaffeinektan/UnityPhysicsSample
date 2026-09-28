using UnityEngine;

/// <summary>
/// Holds the curves of <see cref="AnimateKinematicBodyCurve"/>: AnimationCurve has no unmanaged equivalent, so the baker
/// stores them on this ScriptableObject and the component references it.
/// </summary>
class AnimateKinematicBodyCurveHost : ScriptableObject
{
    public AnimationCurve TranslationCurve;
    public AnimationCurve OrientationCurve;
}
