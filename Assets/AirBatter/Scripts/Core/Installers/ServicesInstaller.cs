using UnityEngine;

[DefaultExecutionOrder(-100)]
public class ServicesInstaller : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _installersInOrder;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (_installersInOrder == null) return;

        foreach (var mb in _installersInOrder)
        {
            if ( mb is not IInstaller installer)
            {
                if (mb != null)
                    Debug.LogWarning($"'{mb.name}' Does not implement IInstaller, skipping it", mb);
                continue;
            }

            installer.Install(ServiceLocator.Instance);
           
        }

    }

    private void OnDestroy()
    {
        foreach (IInstaller installer  in _installersInOrder)
        {
            installer.Uninstall(ServiceLocator.Instance);
        }
    }

}
