//using System;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public interface IResource
//{
//    float Total { get; }
//    float Current { get; }
//    void Spend(float amount);
//    void Regenerate(float deltaTime);
//    event Action<float> OnResourceChanged;
//}

//public abstract class BaseResource : IResource
//{
//    public float Total { get; protected set; }
//    public float Current { get; protected set; }
//    public event Action<float> OnResourceChanged;

//    protected BaseResource(float total)
//    {
//        Total = total;
//        Current = total;
//    }

//    public virtual void Spend(float amount)
//    {
//        Current = Mathf.Max(Current - amount, 0);
//        OnResourceChanged?.Invoke(Current / Total);
//    }

//    public virtual void Regenerate(float deltaTime) { }

//    protected void ChangeCurrent(float newValue)
//    {
//        Current = Mathf.Clamp(newValue, 0, Total);
//        OnResourceChanged?.Invoke(Current / Total);
//    }
//}

//public class Mana : BaseResource
//{
//    private readonly float _regenRate;

//    public Mana(float total, float regenRate) : base(total)
//    {
//        _regenRate = regenRate;
//    }

//    public override void Regenerate(float deltaTime)
//    {
//        ChangeCurrent(Current + _regenRate * deltaTime);
//    }
//}

//public class Energy : BaseResource
//{
//    private readonly float _regenRate;

//    public Energy(float total, float regenRate) : base(total)
//    {
//        _regenRate = regenRate;
//    }

//    public override void Regenerate(float deltaTime)
//    {
//        ChangeCurrent(Current + _regenRate * deltaTime);
//    }
//}

//public class HealthManaHybrid : IResource
//{
//    private readonly BaseResource _mana;
//    private readonly BaseResource _health;

//    public float Total => _mana.Total + _health.Total;
//    public float Current => _mana.Current + _health.Current;
//    public event Action<float> OnResourceChanged;

//    public HealthManaHybrid(BaseResource mana, BaseResource health)
//    {
//        _mana = mana;
//        _health = health;
//        _mana.OnResourceChanged += OnChange;
//        _health.OnResourceChanged += OnChange;
//    }

//    public void Spend(float amount)
//    {
//        if (_mana.Current >= amount)
//        {
//            _mana.Spend(amount);
//        }
//        else
//        {
//            float remaining = amount - _mana.Current;
//            _mana.Spend(_mana.Current);
//            _health.Spend(remaining);
//        }
//    }

//    public void Regenerate(float deltaTime)
//    {
//        _mana.Regenerate(deltaTime);
//        _health.Regenerate(deltaTime);
//    }

//    private void OnChange(float _) => OnResourceChanged?.Invoke(Current / Total);
//}

//public class ResourceController
//{
//    private readonly Dictionary<Type, IResource> _resources = new();

//    public void AddResource<T>(T resource) where T : IResource
//    {
//        _resources[typeof(T)] = resource;
//    }

//    public T GetResource<T>() where T : IResource
//    {
//        return _resources.TryGetValue(typeof(T), out var resource) ? (T)resource : default;
//    }

//    public void RegenerateAll(float deltaTime)
//    {
//        foreach (var resource in _resources.Values)
//        {
//            resource.Regenerate(deltaTime);
//        }
//    }
//}

//public class Character : MonoBehaviour
//{
//    private ResourceController _resourceController;

//    private void Awake()
//    {
//        _resourceController = new ResourceController();

//        var mana = new Mana(100, 5);
//        var energy = new Energy(50, 10);
//        var hybrid = new HealthManaHybrid(mana, new BaseResource(150));

//        _resourceController.AddResource(mana);
//        _resourceController.AddResource(energy);
//        _resourceController.AddResource(hybrid);
//    }

//    private void Update()
//    {
//        _resourceController.RegenerateAll(Time.deltaTime);
//    }

//    public void UseMana(float amount)
//    {
//        _resourceController.GetResource<Mana>()?.Spend(amount);
//    }

//    public void UseEnergy(float amount)
//    {
//        _resourceController.GetResource<Energy>()?.Spend(amount);
//    }

//    public void UseHybrid(float amount)
//    {
//        _resourceController.GetResource<HealthManaHybrid>()?.Spend(amount);
//    }
//}
