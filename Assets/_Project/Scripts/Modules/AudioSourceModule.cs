using UnityEngine;
using Zenject;

public class AudioSourceModule : MonoInstaller
{
    [SerializeField] private AudioSource _audioSource;
    public override void InstallBindings()
    {
        ProjectContext.Instance.Container.Bind<AudioSource>().FromInstance(_audioSource).AsSingle();
    }
}