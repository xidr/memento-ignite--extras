using System.ComponentModel;
using UnityEngine;

namespace XTools {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ServiceLocator))]
    internal abstract class ServiceLocatorBootstrapper : MonoBehaviour {
        ServiceLocator _container;
        internal ServiceLocator container => _container.OrNull() ?? (_container = GetComponent<ServiceLocator>());

        bool _hasBeenBootstrapped;

        void Awake() => BootstrapOnDemand();

        public void BootstrapOnDemand() {
            if (_hasBeenBootstrapped) return;
            _hasBeenBootstrapped = true;
            Bootstrap();
        }

        protected abstract void Bootstrap();
    }

    [AddComponentMenu("Patterns/IoS_ServiceLocator.GA/ServiceLocator Global")]
    internal class ServiceLocatorGlobalBootstrapper : ServiceLocatorBootstrapper {
        [SerializeField] bool _dontDestroyOnLoad = true;

        protected override void Bootstrap() {
            container.ConfigureAsGlobal(_dontDestroyOnLoad);
        }
    }

    [AddComponentMenu("Patterns/IoS_ServiceLocator.GA/ServiceLocator Scene")]
    internal class ServiceLocatorSceneBootstrapper : ServiceLocatorBootstrapper {
        protected override void Bootstrap() {
            container.ConfigureForScene();
        }
    }
}