using System.Collections;
using UnityEngine;

public class SoulPresenter : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private ParticleSystem _appearParticles;

    public void Appear()
    {
        _appearParticles.Play();
        _particles.Play();
    }

	public void SetColor(Color color)
    {
        var module = _particles.colorOverLifetime;
        var gradient = new Gradient()
        {
            colorKeys = new GradientColorKey[]
            {
                new GradientColorKey(Color.white, 0),
                new GradientColorKey(color, 0.4f),
                new GradientColorKey(color, 1)
            },
            alphaKeys = new GradientAlphaKey[]
            {
                new GradientAlphaKey(1, 0),
                new GradientAlphaKey(1, 0.75f),
                new GradientAlphaKey(0, 1)
            }
        };

        module.color = gradient;
    }

    public void Disappear() => StartCoroutine(Disappearing());

    private IEnumerator Disappearing()
    {
		_particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
	}
}