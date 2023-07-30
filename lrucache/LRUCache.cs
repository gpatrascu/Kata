namespace lrucache;

public class LRUCache
{
    private readonly int _capacity;
    private readonly Dictionary<int, int> _dictionary;

    readonly Queue<int> touched;
    
    public LRUCache(int capacity) {
        _capacity = capacity;
        _dictionary = new Dictionary<int, int>();
        touched = new Queue<int>();
    }
    
    public int Get(int key)
    {
        var val = _dictionary.TryGetValue(key, out var value) ? value : -1;

        if (val == -1)
        {
            return -1;
        }
        
        touched.Enqueue(key);
        RemoveUnusedElements();

        
        return val;
    }

    private void RemoveUnusedElements()
    {
        if (touched.Count > _capacity)
        {
            var dequeue = touched.Dequeue();
            if (!touched.Contains(dequeue))
            {
                _dictionary.Remove(dequeue);
            }
        }
    }

    public void Put(int key, int value) {
        touched.Enqueue(key);
        RemoveUnusedElements();

        _dictionary.Remove(key);
        _dictionary.Add(key, value);
    }
}