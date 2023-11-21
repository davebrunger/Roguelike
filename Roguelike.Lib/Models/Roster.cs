using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Roguelike.Lib.Models;

public class Roster<T> : IReadOnlyDictionary<int, T> where T : IEntity
{
    private readonly ImmutableDictionary<int, T> entities;

    private readonly int nextId;

    public IEnumerable<int> Keys => entities.Keys;

    public IEnumerable<T> Values => entities.Values;

    public int Count => entities.Count;

    private Roster(ImmutableDictionary<int, T> entities, int nextId)
    {
        this.entities = entities;
        this.nextId = nextId;   
    }

    public Roster() : this(ImmutableDictionary<int, T>.Empty, 0)
    {
    }

    public Roster<T> Add(Func<int, T> initEntity)
    {
        var newEntity = initEntity(nextId);
        if (newEntity.Id != nextId)
        {
            throw new ArgumentException("Entity ID must match provided ID", nameof(initEntity));
        }
        return new Roster<T>(entities.Add(nextId, initEntity(nextId)), nextId + 1);
    }

    public Roster<T> Remove(int id)
    {
        return new Roster<T>(entities.Remove(id), id);
    }

    public Roster<T> Update(T entity)
    {
        if (!entities.ContainsKey(entity.Id))
        {
            throw new ArgumentException("Entity must exist in roster", nameof(entity));
        }
        return new Roster<T>(entities.SetItem(entity.Id, entity), nextId);
    }

    public bool ContainsKey(int key) => entities.ContainsKey(key);

    public bool TryGetValue(int key, [MaybeNullWhen(false)] out T value) => entities.TryGetValue(key, out value);

    public IEnumerator<KeyValuePair<int, T>> GetEnumerator() => entities.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public T this[int id]
    {
        get => entities[id];
    }
}
