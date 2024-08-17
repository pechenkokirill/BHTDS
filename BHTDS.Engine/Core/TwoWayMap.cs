namespace BHTDS.Engine.Core;



public class TwoWayMap<TKey, TValue>
{
#nullable disable

    private readonly Dictionary<TKey, TValue> keyValueMap = [];
    private readonly Dictionary<TValue, TKey> valueKeyMap = [];

#nullable enable

    public IReadOnlyDictionary<TKey, TValue> Map => this.keyValueMap;

    public void Add(TKey key, TValue value)
    {
        this.keyValueMap.Add(key, value);
        this.valueKeyMap.Add(value, key);
    }

    public TValue? GetOrDefalut(TKey key) => this.keyValueMap.GetValueOrDefault(key);

    public void Remove(TKey key)
    {
        if (this.keyValueMap.Remove(key, out var value))
        {
            this.valueKeyMap.Remove(value);
        }
    }

    public void Remove(TValue value)
    {
        if (this.valueKeyMap.Remove(value, out var key))
        {
            this.keyValueMap.Remove(key);
        }
    }
}
