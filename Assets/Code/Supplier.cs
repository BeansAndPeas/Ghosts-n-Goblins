using System;

public class Supplier<T> {
    private Nullable<T> value;
    private readonly Func<T> getValue;

    public Supplier(Func<T> value) {
        this.value = null;
        this.getValue = value;
    }

    public T Value {
        get {
            if (value == null) {
                value = getValue();
            }
            return (T)value;
        }
    }
}