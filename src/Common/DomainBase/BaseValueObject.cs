namespace Common.DomainBase;

public record BaseValueObject<T>
{
    public T Value { get; }

    protected BaseValueObject(T value)
    {
        Value = value;
    }

    public static implicit operator T(BaseValueObject<T> valueObject) => valueObject.Value;
    public static explicit operator BaseValueObject<T>(T valueObject) => new(valueObject);
}