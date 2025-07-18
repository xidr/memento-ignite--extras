using Reflex.Core;
using UnityEngine;

namespace XTools {
    public abstract class ProjectInstallerBase : MonoBehaviour, IInstaller {
        public virtual void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(new AudioManager(), typeof(AudioManager));
            builder.AddSingleton(new SceneLoader(), typeof(SceneLoader));
        }
    }
}