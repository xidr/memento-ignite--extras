using Reflex.Core;
using UnityEngine;

namespace XTools {
    public abstract class ProjectInstallerBase : MonoBehaviour, IInstaller {
        public virtual void InstallBindings(ContainerBuilder builder)
        {
            
        }
    }
}