using System;
using System.Collections.Generic;
using UnityEngine;

public class SystemsManager : MonoBehaviour
{
    private static SystemsManager inst;

    [SerializeField]
    private SystemBase[] systems;

    private Dictionary<Type, SystemBase> systemsMap = new Dictionary<Type, SystemBase>();

    private void Awake()
    {
        inst = this;

        for (int i = 0; i < systems.Length; i++)
        {
            systems[i] = Instantiate(systems[i]);

            systemsMap.Add(systems[i].GetType(), systems[i]);
        }

        foreach (var item in systems)
        {
            item.Init();
        }
    }

    private void OnEnable()
    {
        foreach (var item in systems)
        {
            item.MyOnEnable();
        }
    }

    private void OnDisable()
    {
        foreach (var item in systems)
        {
            item.MyOnDisable();
        }
    }

    private void OnDestroy()
    {
        foreach (var item in systems)
        {
            item.MyOnDestroy();
        }
    }

    public static T GetSystem<T>() where T : class
    {
        if (inst.systemsMap.TryGetValue(typeof(T), out var system))
        {
            return system as T;
        }

        foreach (var item in inst.systemsMap.Values)
        {
            if (item is T)
            {
                return item as T;
            }
        }

        return null;
    }
}