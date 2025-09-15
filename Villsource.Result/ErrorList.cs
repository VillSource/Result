using System.Collections;

namespace Villsource.Result;

public class ErrorList : IError, IList<IError>
{
    private readonly IList<IError> _errors = new List<IError>();
    public IEnumerator<IError> GetEnumerator()
    {
        return _errors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(IError item)
    {
        _errors.Add(item);
    }

    public void Clear()
    {
        _errors.Clear();
    }

    public bool Contains(IError item)
    {
        return _errors.Contains(item);
    }

    public void CopyTo(IError[] array, int arrayIndex)
    {
        _errors.CopyTo(array, arrayIndex);
    }

    public bool Remove(IError item)
    {
        return _errors.Remove(item);
    }

    public int Count => _errors.Count;
    public bool IsReadOnly => _errors.IsReadOnly;
    public int IndexOf(IError item)
    {
        return _errors.IndexOf(item);
    }

    public void Insert(int index, IError item)
    {
        _errors.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        _errors.RemoveAt(index);
    }

    public IError this[int index]
    {
        get => _errors[index];
        set => _errors[index] = value;
    }
}
