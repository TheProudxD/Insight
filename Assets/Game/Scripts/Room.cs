using Enemies;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects;
    private IEnumerable<Enemy> _enemies;

    private void Awake()
    {
        for (var i=0; i< transform.childCount; i++)
        {
            _objects.Add(transform.GetChild(i).gameObject);
        }

        _enemies = GetOnlyEnemies(_objects);
        _objects = GetAllExceptEnemies(_objects).ToList();
        _objects.ForEach(x => x.SetActive(false));
    }
    private IEnumerable<Enemy> GetOnlyEnemies(IEnumerable<GameObject> objects) => objects.Where(g => !g.IsUnityNull() && g.TryGetComponent(out Enemy enemy)).Select(x => x.GetComponent<Enemy>());
    private IEnumerable<GameObject> GetAllExceptEnemies(IEnumerable<GameObject> objects) => objects.Where(g => !g.IsUnityNull() && !g.TryGetComponent(out Enemy enemy)).Select(x => x);
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            foreach (var enemy in _enemies)
            {
                ChangeActivation(enemy,true); 
            }
            foreach (var @object in _objects)
            {
                ChangeActivation(@object, true); 
            }
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            foreach (var enemy in _enemies)
            {
                ChangeActivation(enemy,false);
            }
            foreach (var @object in _objects)
            {
                ChangeActivation(@object, false);
            }
        }
    }

    protected void ChangeActivation(Enemy enemy, bool activation)
    {
        if (!enemy.IsUnityNull())
            enemy.gameObject.SetActive(activation);
    }

    protected void ChangeActivation(GameObject component, bool activation)
    {
        if (!component.IsUnityNull())
            component.SetActive(activation);
    }
}
